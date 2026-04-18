/*
==================================================================================
Program.cs - Main entry point for the application "COFFEE SHOP MANAGEMENT SYSTEM".

This class controls the overall flow of the system.
The application starts with a login screen where the user
must enter a valid employee ID (Barista or Supervisor).

After successful login, the user is shown a role-based menu:
- Barista: Can register orders and view the coffee menu
- Supervisor: Has the same functionality as Barista,
plus access to order history and sales overview

The program uses a loop-based console navigation system,
where each menu returns control back to the main flow.

Key responsibilities in this file:
- Handle login and validation of employee ID (Bxxx / Sxxx)
- Route users to correct menus based on role
- Display menus and handle user input
- Control program lifecycle (start, run, exit)

Error messages use colored output where "Error" is shown in red
for better user feedback in the console.

Note:
Business logic is handled in Services,
while this file focuses only on flow and user interaction.
=================================================================================
*/

// Importing required namespaces from the project:
// - Entities: Contains core classes such as Employee, Barista and Supervisor
// - Services: Handles business logic like login, menu handling and system functionality
// - Utils: Contains helper classes such as input validation
using CoffeeShopManagementSystem.Entities;
using CoffeeShopManagementSystem.Services;
using CoffeeShopManagementSystem.Utils;

namespace CoffeeShopManagementSystem;

public class Program
{
    public static void Main()
    {
        // Create the services used by the application
        EmployeeService employeeService = new EmployeeService();
        MenuService menuService = new MenuService();
        LoginService loginService = new LoginService(employeeService);

        bool isRunning = true;

        // Main loop for the application start menu
        while (isRunning)
        {
            ShowStartMenu();
            int startChoice = InputValidator.GetMenuChoice(0, 1);

            switch (startChoice)
            {
                case 1:
                {
                    // Try to log in an employee
                    Employee? loggedInEmployee = HandleLogin(loginService);

                    // Return to start menu if user chose to go back
                    if (loggedInEmployee is null)
                    {
                        break;
                    }

                    // Show the correct menu based on employee role
                    RunRoleBasedMenu(loggedInEmployee, menuService);
                    break;
                }

                case 0:
                    // Exit the application
                    isRunning = false;
                    break;
            }
        }

        Console.Clear();
        Console.WriteLine("Goodbye!");
    }

    private static void ShowStartMenu()
    {
        // Display the first menu shown when the program starts
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
        // Keep asking for employee ID until login succeeds or user goes back
        while (true)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("      COFFEE SHOP MANAGEMENT SYSTEM");
            Console.WriteLine("========================================");
            Console.WriteLine("Enter employee ID:");
            Console.WriteLine();
            Console.WriteLine("(Enter 0 to go back)");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Example:");
            Console.WriteLine("B001 = Barista");
            Console.WriteLine("S001 = Supervisor");
            Console.WriteLine();
            Console.Write("Employee ID: ");

            // Read and normalize the employee ID input
            string input = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

            // Return to previous menu
            if (input == "0")
            {
                return null;
            }

            // Validate employee ID format before checking if it exists
            if (!IsValidEmployeeId(input))
            {
                // Print the word "Error" in red, then reset color so the rest of the message is normal
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error");
                Console.ResetColor();
                Console.WriteLine(" - Invalid ID format. Use Bxxx or Sxxx (for example B001 or S001).");
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }

            // Attempt to log in using the entered employee ID
            Employee? employee = loginService.LogIn(input);

            // Return employee if login succeeds
            if (employee is not null)
            {
                return employee;
            }

            // Print the word "Error" in red, then reset color so the rest of the message is normal
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error");
            Console.ResetColor();
            Console.WriteLine(" - Employee ID not found.");
            Console.WriteLine("Press any key to try again...");
            Console.ReadKey();
        }
    }

    // Validate employee ID format (Bxxx or Sxxx)
    private static bool IsValidEmployeeId(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        // Employee ID must be exactly 4 characters long, for example B001 or S001
        if (input.Length != 4)
        {
            return false;
        }

        // First character must be B or S
        char prefix = input[0];
        if (prefix != 'B' && prefix != 'S')
        {
            return false;
        }

        // Remaining characters must all be digits
        string numericPart = input.Substring(1);
        return numericPart.All(char.IsDigit);
    }

    private static void RunRoleBasedMenu(Employee employee, MenuService menuService)
    {
        bool isLoggedIn = true;

        // Keep showing menus until user chooses to switch user or exit
        while (isLoggedIn)
        {
            if (employee is Barista)
            {
                isLoggedIn = RunBaristaMenu(employee, menuService);
            }
            else if (employee is Supervisor)
            {
                isLoggedIn = RunSupervisorMenu(employee, menuService);
            }
            else
            {
                // Safety fallback if employee role is unknown
                isLoggedIn = false;
            }
        }
    }

    private static bool RunBaristaMenu(Employee employee, MenuService menuService)
    {
        Console.Clear();
        ShowHeader(employee);

        // Show menu options for barista
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
                // Open the new order menu
                menuService.ShowNewOrderMenu(employee);
                return true;

            case 2:
                // Show the coffee menu
                menuService.ShowCoffeeMenu(employee);
                return true;

            case 3:
                // Log out and return to login
                return false;

            case 0:
                // Exit the entire application
                Environment.Exit(0);
                return false;

            default:
                return true;
        }
    }

    private static bool RunSupervisorMenu(Employee employee, MenuService menuService)
    {
        Console.Clear();
        ShowHeader(employee);

        // Show menu options for supervisor
        Console.WriteLine("1. New order");
        Console.WriteLine("2. Order history");
        Console.WriteLine("3. Sales overview");
        Console.WriteLine("4. Coffee menu");
        Console.WriteLine("5. Switch user");
        Console.WriteLine();
        Console.WriteLine("0. Exit");
        Console.WriteLine("----------------------------------------");
        Console.Write("Choose an option: ");

        int choice = InputValidator.GetMenuChoice(0, 5);

        switch (choice)
        {
            case 1:
                // Open the new order menu
                menuService.ShowNewOrderMenu(employee);
                return true;

            case 2:
                // Show order history
                menuService.ShowOrderHistoryMenu(employee);
                return true;

            case 3:
                // Show sales overview
                menuService.ShowSalesOverviewMenu(employee);
                return true;

            case 4:
                // Show coffee menu
                menuService.ShowCoffeeMenu(employee);
                return true;

            case 5:
                // Log out and return to login
                return false;

            case 0:
                // Exit the entire application
                Environment.Exit(0);
                return false;

            default:
                return true;
        }
    }

    private static void ShowHeader(Employee employee)
    {
        // Display the common header used in role menus
        Console.WriteLine("========================================");
        Console.WriteLine("      COFFEE SHOP MANAGEMENT SYSTEM");
        Console.WriteLine("========================================");
        Console.WriteLine($"Logged in as: {employee.Id} - {employee.GetRoleName()}");
        Console.WriteLine($"Name: {employee.Name}");
        Console.WriteLine($"Date: {DateTime.Now:dd.MM.yyyy}  |  Time: {DateTime.Now:HH:mm}");
        Console.WriteLine();
    }
}