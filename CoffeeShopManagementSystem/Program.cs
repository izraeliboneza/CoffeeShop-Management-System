using CoffeeShopManagementSystem.Entities;
using CoffeeShopManagementSystem.Services;
using CoffeeShopManagementSystem.Utils;

namespace CoffeeShopManagementSystem;

public class Program
{
    public static void Main()
    {
        // Create the services used by the application.
        EmployeeService employeeService = new EmployeeService();
        MenuService menuService = new MenuService();
        LoginService loginService = new LoginService(employeeService);
        WorkSessionService workSessionService = new WorkSessionService();

        bool isRunning = true;

        // Main loop for the application start menu.
        while (isRunning)
        {
            ShowStartMenu();
            int startChoice = InputValidator.GetMenuChoice(0, 1);

            switch (startChoice)
            {
                case 1:
                {
                    while (true)
                    {
                        // Try to log in an employee.
                        Employee? loggedInEmployee = HandleLogin(loginService);

                        // Return to start menu if user chose to return from login.
                        if (loggedInEmployee is null)
                        {
                            break;
                        }

                        // Start time tracking for this login session.
                        workSessionService.Start(loggedInEmployee);

                        // Show the correct menu based on employee role.
                        RunRoleBasedMenu(
                            loggedInEmployee,
                            menuService,
                            employeeService,
                            workSessionService
                        );
                    }

                    break;
                }

                case 0:
                {
                    Console.WriteLine();
                    Console.Write("Are you sure you want to exit? (y/n): ");
                    string input = Console.ReadLine()?.Trim().ToLower() ?? "";

                    if (input == "y")
                    {
                        isRunning = false;
                    }

                    break;
                }
            }
        }

        Console.Clear();
        Console.WriteLine("The program has now been closed...");
    }

    private static void ShowStartMenu()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("      COFFEE SHOP MANAGEMENT SYSTEM");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Log in");
        Console.WriteLine("0. Exit");
        Console.WriteLine("----------------------------------------");
        Console.Write("Choose an option: ");
    }

    private static Employee? HandleLogin(LoginService loginService)
    {
        // Keep asking for employee ID until login succeeds or user returns to the main menu.
        while (true)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("      COFFEE SHOP MANAGEMENT SYSTEM");
            Console.WriteLine("========================================");
            Console.WriteLine("Enter employee ID:");
            Console.WriteLine();
            Console.WriteLine("(Enter 0 to return to main menu)");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Example login IDs:");
            Console.WriteLine("- Baristas   : B001, B002, B003, B004");
            Console.WriteLine("- Supervisor : S001");
            Console.WriteLine();
            Console.Write("Employee ID: ");

            string input = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

            // Return null so the main flow goes back to the start menu.
            if (input == "0")
            {
                return null;
            }

            if (!IsValidEmployeeId(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error");
                Console.ResetColor();
                Console.WriteLine(" - Invalid ID format. Use Bxxx or Sxxx (for example B001 or S001).");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }

            Employee? employee = loginService.LogIn(input);

            if (employee is not null)
            {
                return employee;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error");
            Console.ResetColor();
            Console.WriteLine(" - Employee ID not found.");
            Console.WriteLine("Press any key to try again...");
            Console.ReadKey();
        }
    }

    private static bool IsValidEmployeeId(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        if (input.Length != 4)
        {
            return false;
        }

        char prefix = input[0];

        if (prefix != 'B' && prefix != 'S')
        {
            return false;
        }

        string numericPart = input.Substring(1);
        return numericPart.All(char.IsDigit);
    }

    private static void RunRoleBasedMenu(
        Employee employee,
        MenuService menuService,
        EmployeeService employeeService,
        WorkSessionService workSessionService)
    {
        bool isLoggedIn = true;

        while (isLoggedIn)
        {
            if (employee is Barista)
            {
                isLoggedIn = RunBaristaMenu(
                    employee,
                    menuService,
                    employeeService,
                    workSessionService
                );
            }
            else if (employee is Supervisor)
            {
                isLoggedIn = RunSupervisorMenu(
                    employee,
                    menuService,
                    employeeService,
                    workSessionService
                );
            }
            else
            {
                isLoggedIn = false;
            }
        }
    }

    private static bool RunBaristaMenu(
        Employee employee,
        MenuService menuService,
        EmployeeService employeeService,
        WorkSessionService workSessionService)
    {
        Console.Clear();
        ShowHeader(employee);

        Console.WriteLine("1. New order");
        Console.WriteLine("2. Coffee menu");
        Console.WriteLine("3. Switch user");
        Console.WriteLine();
        Console.WriteLine("0. Exit");
        Console.WriteLine("----------------------------------------");
        Console.Write("Choose an option: ");

        int choice = InputValidator.GetMenuChoice(0, 3);

        switch (choice)
        {
            case 1:
                menuService.ShowNewOrderMenu(employee);
                return true;

            case 2:
                menuService.ShowCoffeeMenu(employee);
                return true;

            case 3:
                // Save work session before switching user.
                workSessionService.End(employeeService.GetHourlyWage(employee.Id));
                return false;

            case 0:
            {
                Console.WriteLine();
                Console.Write("Are you sure you want to exit? (y/n): ");
                string input = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (input == "y")
                {
                    // Save work session before closing the program.
                    workSessionService.End(employeeService.GetHourlyWage(employee.Id));
                    Environment.Exit(0);
                }

                return true;
            }

            default:
                return true;
        }
    }

    private static bool RunSupervisorMenu(
        Employee employee,
        MenuService menuService,
        EmployeeService employeeService,
        WorkSessionService workSessionService)
    {
        Console.Clear();
        ShowHeader(employee);

        Console.WriteLine("1. New order");
        Console.WriteLine("2. Coffee menu");
        Console.WriteLine("3. Switch user");
        Console.WriteLine("--------------------");
        Console.WriteLine("4. Order history");
        Console.WriteLine("5. Sales overview");
        Console.WriteLine("6. Time & Wage Overview");
        Console.WriteLine("--------------------");
        Console.WriteLine();
        Console.WriteLine("0. Exit");
        Console.WriteLine("----------------------------------------");
        Console.Write("Choose an option: ");

        int choice = InputValidator.GetMenuChoice(0, 6);

        switch (choice)
        {
            case 1:
                menuService.ShowNewOrderMenu(employee);
                return true;

            case 2:
                menuService.ShowCoffeeMenu(employee);
                return true;

            case 3:
                // Save work session before switching user.
                workSessionService.End(employeeService.GetHourlyWage(employee.Id));
                return false;

            case 4:
                menuService.ShowOrderHistoryMenu(employee);
                return true;

            case 5:
                menuService.ShowSalesOverviewMenu(employee);
                return true;

            case 6:
                menuService.ShowTimeAndWageOverview(employee);
                return true;

            case 0:
            {
                Console.WriteLine();
                Console.Write("Are you sure you want to exit? (y/n): ");
                string input = Console.ReadLine()?.Trim().ToLower() ?? "";

                if (input == "y")
                {
                    // Save work session before closing the program.
                    workSessionService.End(employeeService.GetHourlyWage(employee.Id));
                    Environment.Exit(0);
                }

                return true;
            }

            default:
                return true;
        }
    }

    private static void ShowHeader(Employee employee)
    {
        Console.WriteLine("========================================");
        Console.WriteLine("      COFFEE SHOP MANAGEMENT SYSTEM");
        Console.WriteLine("========================================");
        Console.WriteLine($"Logged in as: {employee.Id} - {employee.GetRoleName()}");
        Console.WriteLine($"Name: {employee.Name}");
        Console.WriteLine($"Date: {DateTime.Now:dd.MM.yyyy}  |  Time: {DateTime.Now:HH:mm}");
        Console.WriteLine();
    }
}