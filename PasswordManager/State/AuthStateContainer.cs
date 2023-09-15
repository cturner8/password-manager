using API.Dto.User;

namespace PasswordManager.State;

public class AuthStateContainer
{
    private GetUserDto User = null;

    public GetUserDto LoggedInUser
    {
        get => User;
        set
        {
            User = value;
            NotifyStateChanged();
        }
    }

    public event Action OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
