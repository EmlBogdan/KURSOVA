using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        // Ініціалізація контексту бази даних, репозиторію та сервісів
        var dbContext = new DBContext();
        var accountRepo = new AccountRepository(dbContext);
        var gameRepo = new GameRepository(dbContext);
        var accountService = new AccountService(accountRepo);
        var gameService = new GameService(gameRepo);

        // Основне меню програми
        while (true)
        {
            Console.WriteLine("Welcome to Tic-Tac-Toe!");
            Console.WriteLine("1. Register\n2. Login\n3. Exit Program");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                // Реєстрація користувача
                Console.Write("Enter username: ");
                var username = Console.ReadLine();
                Console.Write("Enter password: ");
                var password = Console.ReadLine();

                string registrationMessage = accountService.Register(username, password);
                Console.WriteLine(registrationMessage);
            }
            else if (choice == "2")
            {
                // Авторизація користувача
                Console.Write("Enter username: ");
                var username = Console.ReadLine();
                Console.Write("Enter password: ");
                var password = Console.ReadLine();

                var currentUser = accountService.Login(username, password);

                if (currentUser != null)
                {
                    Console.WriteLine($"Welcome back, {currentUser.Username}!");
                    UserMenu(currentUser, accountService, gameService);
                }
                else
                {
                    Console.WriteLine("Invalid credentials. Try again.");
                }
            }
            else if (choice == "3")
            {
                // Вихід із програми
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Try again.");
            }
        }
    }

    // Меню користувача після авторизації
    public static void UserMenu(User currentUser, AccountService accountService, GameService gameService)
    {
        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Play Game\n2. Update Account\n3. View Game History\n4. Exit Account");
            var option = Console.ReadLine();

            if (option == "1")
            {
                // Грати в гру
                PlayGame(currentUser, accountService, gameService);
            }
            else if (option == "2")
            {
                // Оновлення даних користувача
                Console.Write("Enter new username: ");
                var newUsername = Console.ReadLine();
                Console.Write("Enter new password: ");
                var newPassword = Console.ReadLine();

                accountService.UpdateAccount(currentUser.Id, newUsername, newPassword);
                Console.WriteLine("Account updated successfully.");
            }
            else if (option == "3")
            {
                // Перегляд історії матчів
                var history = gameService.GetMatchHistory(currentUser.Id);
                Console.WriteLine("Match History:");
                Console.WriteLine("| Game ID | Opponent       | Result  | Match Rating |");
                Console.WriteLine("|---------|----------------|---------|--------------|");

                foreach (var game in history)
                {
                    string opponent;
                    if (game.Player1Id == currentUser.Id)
                    {
                        var user = accountService.GetUserById(game.Player2Id);
                        opponent = user != null ? user.Username : "Unknown";
                    }
                    else
                    {
                        var user = accountService.GetUserById(game.Player1Id);
                        opponent = user != null ? user.Username : "Unknown";
                    }

                    string result = game.Player1Id == currentUser.Id
                        ? game.Result
                        : game.Result == "win" ? "lose" : game.Result == "lose" ? "win" : "draw";

                    Console.WriteLine($"| {game.Id,-7} | {opponent,-14} | {result,-7} | {game.MatchRating,-12} |");
                }

                // Додавання поточного рейтингу користувача
                Console.WriteLine($"Your current rating: {currentUser.Rating}");
                Console.WriteLine();
            }
            else if (option == "4")
            {
                // Вихід з облікового запису
                Console.WriteLine("Exiting account...");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option. Try again.");
            }
        }
    }

    // Логіка гри "Хрестики-нулики"
    public static void PlayGame(User currentUser1, AccountService accountService, GameService gameService)
    {
        Console.WriteLine("Now, second player must log in or register.");

        User currentUser2 = null;
        while (currentUser2 == null)
        {
            Console.WriteLine("1. Register\n2. Login");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                // Реєстрація другого гравця
                Console.Write("Enter username: ");
                var username = Console.ReadLine();
                Console.Write("Enter password: ");
                var password = Console.ReadLine();

                string registrationMessage = accountService.Register(username, password);
                Console.WriteLine(registrationMessage);

                if (registrationMessage == "Registration successful!")
                {
                    currentUser2 = accountService.Login(username, password);
                }
            }
            else if (choice == "2")
            {
                // Логін другого гравця
                Console.Write("Enter username: ");
                var username = Console.ReadLine();
                Console.Write("Enter password: ");
                var password = Console.ReadLine();

                if (username == currentUser1.Username && password == currentUser1.Password)
                {
                    Console.WriteLine("Error: The second player cannot use the first player's credentials.");
                    continue;
                }

                currentUser2 = accountService.Login(username, password);

                if (currentUser2 == null)
                {
                    Console.WriteLine("Invalid credentials. Try again.");
                }
            }
        }

        Console.WriteLine("Starting Tic-Tac-Toe...");

        var game = new TicTacToeGame();
        game.StartGame(currentUser1.Username, currentUser2.Username);
        string result = game.PlayGame();

        Random random = new Random();
        int matchRating = random.Next(15, 26);

        // Збереження результатів гри
        gameService.SaveGameResult(currentUser1, currentUser2, result, matchRating);
        gameService.UpdatePlayerRatings(currentUser1, currentUser2, result, matchRating);

        Console.WriteLine($"Game finished! Result: {result}");
    }
    
}
