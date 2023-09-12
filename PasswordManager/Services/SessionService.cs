using API.Dto.User;
using API.Services;
using Encryption.Services;
using Microsoft.Extensions.Logging;
using PasswordManager.State;

namespace PasswordManager.Services;

public class SessionService
{
    private readonly ILogger<SessionService> _logger;
    private readonly EncryptionService _encryptionService;
    private readonly UserService _userService;


    public SessionService(
        ILogger<SessionService> logger,
        EncryptionService encryptionService,
        UserService userService
        )
    {
        _logger = logger;
        _encryptionService = encryptionService;
        _userService = userService;
    }

    public async Task Save(AuthStateContainer authStateContainer, UserResultDto dto)
    {
        _logger.LogInformation("Saving user session");

        authStateContainer.LoggedInUser = dto.User;

        await SecureStorage.Default.SetAsync("user_id", dto.User.Id.ToString());

        var keyString = Convert.ToBase64String(dto.Key);
        var ivString = Convert.ToBase64String(dto.IV);

        await SecureStorage.Default.SetAsync("user_key", keyString);
        await SecureStorage.Default.SetAsync("user_iv", ivString);

        _logger.LogInformation("Session saved");
    }

    public async Task Restore(AuthStateContainer authStateContainer)
    {
        try
        {
            if (authStateContainer.LoggedInUser != null) return;

            _logger.LogInformation("Attempting to restore the user session");

            var userIdString = await SecureStorage.Default.GetAsync("user_id");
            var userKeyString = await SecureStorage.Default.GetAsync("user_key");
            var userIVString = await SecureStorage.Default.GetAsync("user_iv");

            if (userIdString == null || userKeyString == null || userKeyString == null)
            {
                _logger.LogInformation("No user session found");
                return;
            }

            _logger.LogInformation("Restoring the user session");

            var userId = Guid.Parse(userIdString);
            var userKey = Convert.FromBase64String(userKeyString);
            var userIV = Convert.FromBase64String(userIVString);

            _encryptionService.Initialise(userKey, userIV);
            authStateContainer.LoggedInUser = _userService.GetUser(userId);

            _logger.LogInformation("Session restored");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            Clear(authStateContainer);
        }

    }

    public void Clear(AuthStateContainer authStateContainer)
    {
        _logger.LogInformation("Clearing session");

        SecureStorage.Default.Remove("user_id");
        SecureStorage.Default.Remove("user_key");
        SecureStorage.Default.Remove("user_iv");

        _encryptionService.Clear();
        authStateContainer.LoggedInUser = null;

        _logger.LogInformation("Session cleared");
    }
}
