public class GameRepository : IGameRepository
{
    private readonly DBContext _dbContext;

    public GameRepository(DBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SaveGame(Game game)
    {
        _dbContext.Games.Add(game);
    }

    public List<Game> GetGamesByUserId(int userId)
    {
        return _dbContext.Games.Where(g => g.Player1Id == userId || g.Player2Id == userId).ToList();
    }
}