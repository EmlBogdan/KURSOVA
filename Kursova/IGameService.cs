public interface IGameService
{
    void SaveGameResult(User player1, User player2, string result, int matchRating);
    void UpdatePlayerRatings(User player1, User player2, string result, int matchRating);
    List<Game> GetMatchHistory(int userId);
}