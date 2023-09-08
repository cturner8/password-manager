using Database.Models;

namespace PasswordManager.State;

public class AuthStateContainer
{
    private User User = null;

    public User LoggedInUser
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
