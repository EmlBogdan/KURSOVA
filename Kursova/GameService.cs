using System.Collections.Generic;
using System.Linq;

////Класс який виконую роль сервісу для взаємодії з процессом гри
public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public void SaveGameResult(User player1, User player2, string result, int matchRating)
    {
        var game = new Game
        {
            Id = _gameRepository.GetGamesByUserId(player1.Id).Count + 1,
            Player1Id = player1.Id,
            Player2Id = player2.Id,
            Result = result,
            MatchRating = matchRating
        };

        _gameRepository.SaveGame(game);
    }

    public void UpdatePlayerRatings(User player1, User player2, string result, int matchRating)
    {
        if (result == "win")
        {
            player1.Rating += matchRating;
            player2.Rating = Math.Max(player2.Rating - matchRating, 0);
        }
        else if (result == "lose")
        {
            player2.Rating += matchRating;
            player1.Rating = Math.Max(player1.Rating - matchRating, 0);
        }
        else // draw
        {
            player1.Rating += matchRating / 2;
            player2.Rating += matchRating / 2;
        }
    }

    public List<Game> GetMatchHistory(int userId)
    {
        return _gameRepository.GetGamesByUserId(userId);
    }
}
