public class AccountRepository : IAccountRepository
{
    private readonly DBContext _dbContext;

    public AccountRepository(DBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User GetUser(string username, string password)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }

    public User GetUserById(int userId)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == userId);
    }

    public void AddUser(User user)
    {
        _dbContext.Users.Add(user);
    }

    public void UpdateUser(User user)
    {
        var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            existingUser.Username = user.Username;
            existingUser.Password = user.Password;
            existingUser.Rating = user.Rating;
        }
    }

    public List<User> GetAllUsers()
    {
        return _dbContext.Users;
    }
}