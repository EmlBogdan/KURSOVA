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

    public void UpdatePlayerRatings(User currentUser1, User currentUser2, string result, int matchRating)
{
    if (result == $"{currentUser1.Username} wins!") 
    {
        currentUser1.Rating += matchRating;
        currentUser2.Rating -= matchRating / 2;
    }
    else if (result == $"{currentUser2.Username} wins!") 
    {
        currentUser2.Rating += matchRating;
        currentUser1.Rating -= matchRating / 2;
    }
    else if (result == "draw") 
    {
        currentUser1.Rating += matchRating / 2;
        currentUser2.Rating += matchRating / 2;
    }
    else
    {
        throw new Exception("Invalid game result!"); 
    }
    currentUser1.Rating = Math.Max(currentUser1.Rating, 0);
    currentUser2.Rating = Math.Max(currentUser2.Rating, 0);
}


    public List<Game> GetMatchHistory(int userId)
    {
        return _gameRepository.GetGamesByUserId(userId);
    }
}
