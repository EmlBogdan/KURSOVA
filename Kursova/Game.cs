//Класс який зберігає дані про гру
public class Game
{
    public int Id { get; set; }
    public int Player1Id { get; set; }
    public int Player2Id { get; set; }
    public string Result { get; set; } // "win", "lose", "draw"
    public int MatchRating { get; set; } // Рейтинг игры
}