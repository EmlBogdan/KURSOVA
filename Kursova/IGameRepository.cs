public interface IGameRepository
{
    void SaveGame(Game game);
    List<Game> GetGamesByUserId(int userId);
}
