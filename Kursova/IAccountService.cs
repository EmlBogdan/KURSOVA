public interface IAccountService
{
    User Login(string username, string password);
    string Register(string username, string password);
    void UpdateAccount(int userId, string newUsername, string newPassword);
    User GetUserById(int userId);
}