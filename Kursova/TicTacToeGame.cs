using System;
//Логіка гри
public class TicTacToeGame
{
    private char[,] board = new char[3, 3];
    private int turnCount = 0;
    private string player1Name;
    private string player2Name;
    //Метод початку гри
    public void StartGame(string player1Name, string player2Name)
    {
        for (int i = 0; i < 3; i++)//Заповнюємо двовимірний масив пустими строками
            for (int j = 0; j < 3; j++)
                board[i, j] = ' ';

        this.player1Name = player1Name;
        this.player2Name = player2Name;
    }

    public string PlayGame()
    {
        int row, col;
        char currentPlayer = 'X';
        string currentPlayerName = player1Name;

        while (true)//Гра триває доти, доки не буде визначено результат
        {
            Console.Clear();
            PrintBoard();

            Console.WriteLine($"{currentPlayerName}'s turn");
            Console.Write("Enter row and column (0-2) separated by a space: ");
            var input = Console.ReadLine()?.Split(' ');//Отримуємо строку у форматі "row column", видаляємо пробіл

            if (input?.Length == 2 && int.TryParse(input[0], out row) && int.TryParse(input[1], out col) && board[row, col] == ' ') // перевірка чи довжина дорівнює 2, чи перше значення число, чи друге значення число і чи не зайнята ця клітинка
            {
                board[row, col] = currentPlayer;
                turnCount++;

                if (CheckWin(currentPlayer)) { Console.Clear(); PrintBoard(); return $"{currentPlayerName} wins!"; };
                if (turnCount == 9) {Console.Clear(); PrintBoard();return "Draw!";};

                currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
                currentPlayerName = currentPlayer == 'X' ? player1Name : player2Name;
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
            }
        }
        
    }
    //Метод для виводу поточного стану гри
    private void PrintBoard()
    {
        Console.WriteLine("Current board:");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(" ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j]);
                if (j < 2) Console.Write(" | ");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("---+---+---");
        }
    }
    //Метод який повертає значення чи переміг гравець чи ні
    private bool CheckWin(char player)
    {
        for (int i = 0; i < 3; i++) //Перевірка по рядкам
            if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) return true;

        for (int i = 0; i < 3; i++) //Перевірка по стовпцям
            if (board[0, i] == player && board[1, i] == player && board[2, i] == player) return true;

        return board[0, 0] == player && board[1, 1] == player && board[2, 2] == player || // Перевірка по діагоналям
               board[0, 2] == player && board[1, 1] == player && board[2, 0] == player;
    }
}
