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
                    PrintPrompt("Choose option: ");
                    continue;
                }

                // Check if input is a valid integer
                if (!int.TryParse(input, out int choice))
                {
                    PrintError($"Invalid input. Please enter a number between {min} and {max}.");
                    PrintPrompt("Choose option: ");
                    continue;
                }

                // Prevent inputs like "00"
                if (input.Length > 1 && choice == 0)
                {
                    PrintError($"Invalid input. Please enter a number between {min} and {max}.");
                    PrintPrompt("Choose option: ");
                    continue;
                }

                // Check range
                if (choice < min || choice > max)
                {
                    PrintError($"Choice must be between {min} and {max}.");
                    PrintPrompt("Choose option: ");
                    continue;
                }

                return choice;
            }
        }

        // Validates that the user enters a positive integer
        public static int GetPositiveInt()
        {
            while (true)
            {
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                // Check for empty input
                if (string.IsNullOrWhiteSpace(input))
                {
                    PrintError("Please enter a positive number.");
                    PrintPrompt("Enter quantity: ");
                    continue;
                }

                // Check if input is a valid integer
                if (!int.TryParse(input, out int value))
                {
                    PrintError("Invalid input. Please enter a positive number.");
                    PrintPrompt("Enter quantity: ");
                    continue;
                }

                // Check that number is greater than 0
                if (value <= 0)
                {
                    PrintError("Number must be greater than 0.");
                    PrintPrompt("Enter quantity: ");
                    continue;
                }

                return value;
            }
        }

        // Validates that the user enters a valid date in format dd.MM.yyyy
        public static DateTime GetDate()
        {
            while (true)
            {
                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                // Check for empty input
                if (string.IsNullOrWhiteSpace(input))
                {
                    PrintError("Please enter a date in format dd.MM.yyyy.");
                    PrintPrompt("Enter date (dd.MM.yyyy): ");
                    continue;
                }

                // Check if input matches the required date format
                if (!DateTime.TryParseExact(
                        input,
                        "dd.MM.yyyy",
                        null,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime date))
                {
                    PrintError("Invalid date. Please use format dd.MM.yyyy.");
                    PrintPrompt("Enter date (dd.MM.yyyy): ");
                    continue;
                }

                return date;
            }
        }

        // Prints error message with red "Error"
        private static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error");
            Console.ResetColor();
            Console.WriteLine($" - {message}");
        }

        // Prints input prompt
        private static void PrintPrompt(string message)
        {
            Console.Write(message);
        }
    }
}