namespace CoffeeShopManagementSystem.Utils
{
    public static class InputValidator
    {
        // Validates menu input within a given range
        public static int GetMenuChoice(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                // Check for empty input
                if (string.IsNullOrWhiteSpace(input))
                {
                    PrintError($"Please enter a number between {min} and {max}.");
                    continue;
                }

                // Check if input is a valid integer
                if (!int.TryParse(input, out int choice))
                {
                    PrintError($"Invalid input. Please enter a number between {min} and {max}.");
                    continue;
                }

                // Prevent inputs like "00"
                if (input.Length > 1 && choice == 0)
                {
                    PrintError($"Invalid input. Please enter a number between {min} and {max}.");
                    continue;
                }

                // Check range
                if (choice < min || choice > max)
                {
                    PrintError($"Choice must be between {min} and {max}.");
                    continue;
                }

                return choice;
            }
        }

        // Prints error message with red "Error"
        private static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error");
            Console.ResetColor();
            Console.WriteLine($" - {message}");
            Console.WriteLine($"Choose option: ");
        }
    }
}