using System.Collections.Generic;
// Інтерфейс який описує методи для взаємодії користувача з бд
public interface IAccountRepository
{
    User GetUser(string username, string password);
    User GetUserById(int userId);
    void AddUser(User user);
    void UpdateUser(User user);
    List<User> GetAllUsers();
}