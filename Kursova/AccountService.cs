using System.Linq;

//Класс який виконую роль сервісу для взаємодії з акаунтами користувачів
public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public User Login(string username, string password)
    {
        return _accountRepository.GetUser(username, password);
    }

    public string Register(string username, string password)
    {
        var existingUser = _accountRepository.GetAllUsers().FirstOrDefault(u => u.Username == username);
        if (existingUser != null)
        {
            return "User already exists.";
        }

        var user = new User
        {
            Id = _accountRepository.GetAllUsers().Count + 1,
            Username = username,
            Password = password,
            Rating = 0
        };
        _accountRepository.AddUser(user);
        return "Registration successful!";
    }

    public void UpdateAccount(int userId, string newUsername, string newPassword)
    {
        var user = _accountRepository.GetUserById(userId);
        if (user != null)
        {
            user.Username = newUsername;
            user.Password = newPassword;
            _accountRepository.UpdateUser(user);
        }
    }

    public User GetUserById(int userId)
    {
        return _accountRepository.GetUserById(userId);
    }
}
