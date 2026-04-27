using CoffeeShopManagementSystem.Entities;

namespace CoffeeShopManagementSystem.Services;

// Stores all employees and their hourly wages
public class EmployeeService
{
    private readonly Dictionary<string, Employee> _employees;
    private readonly Dictionary<string, decimal> _hourlyWages;

    public EmployeeService()
    {
        _employees = new Dictionary<string, Employee>
        {
            {"B001", new Barista("B001", "Elsa Medel-Svensson")},
            {"B002", new Barista("B002", "Per Kaffegård")},
            {"B003", new Barista("B003", "Kari Bønnesen")},
            {"B004", new Barista("B004", "Frank-Marius Kaffeberg")},
            {"S001", new Supervisor("S001", "Petrus Kowalski")},
        };

        _hourlyWages = new Dictionary<string, decimal>
        {
            {"B001", 225m},
            {"B002", 225m},
            {"B003", 225m},
            {"B004", 225m},
            {"S001", 419m},
        };
    }

    public Employee? GetEmployee(string employeeId)
    {
        _employees.TryGetValue(employeeId, out Employee? employee);
        return employee;
    }

    public decimal GetHourlyWage(string employeeId)
    {
        return _hourlyWages.TryGetValue(employeeId, out decimal wage) ? wage : 0m;
    }
}