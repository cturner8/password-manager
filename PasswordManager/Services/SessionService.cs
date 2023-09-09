using API.Dto.User;
using API.Services;
using Encryption.Services;
using Microsoft.Extensions.Logging;
using PasswordManager.State;
using System.Text;

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
        // TODO: fix saving key and iv as strings
        await SecureStorage.Default.SetAsync("user_key", Encoding.ASCII.GetString(dto.Key));
        await SecureStorage.Default.SetAsync("user_iv", Encoding.ASCII.GetString(dto.IV));

        _logger.LogInformation("Session saved");
    }

    public async Task Restore(AuthStateContainer authStateContainer)
    {
        try
        {
            if (authStateContainer.LoggedInUser != null) return;

            _logger.LogInformation("Attempting to restore the user session");

            var userId = await SecureStorage.Default.GetAsync("user_id");
            var userKey = await SecureStorage.Default.GetAsync("user_key");
            var userIV = await SecureStorage.Default.GetAsync("user_iv");

            if (userId == null || userKey == null || userIV == null)
            {
                _logger.LogInformation("No user session found");
                return;
            }

            _logger.LogInformation("Restoring the user session");

            // TODO: fix parsing of saved string content
            var key = Encoding.ASCII.GetBytes(userKey);
            var iv = Encoding.ASCII.GetBytes(userIV);

            _encryptionService.Initialise(key, iv);
            authStateContainer.LoggedInUser = _userService.GetUser(Guid.Parse(userId));

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
