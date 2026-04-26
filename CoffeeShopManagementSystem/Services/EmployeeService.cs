using CoffeeShopManagementSystem.Entities;
namespace CoffeeShopManagementSystem.Services;

//This class store all the employees in the system.
//This would usually come from a databse in a real system.
public class EmployeeService
{
    //A dictionary that lets you look up employee directly by ID.
    // Key is the employee ID, value is the employee object.
    private Dictionary<string, Employee> _employees;
    
    //List of Employees(hardcoded).
    public  EmployeeService()
    {
        _employees = new Dictionary<string, Employee>
        {
            {"B001", new Barista("B001", "Elsa Medel-Svensson")},
            {"B002", new Barista("B002", "Per Kaffegård")},
            {"B003", new Barista("B003", "Kari Bønnesen")},
            {"B004", new Barista("B004", "Frank-Marius Kaffeberg")},
            {"S001", new Supervisor("S001", "Petrus Kowalski")},
        };
    }
    
    //Returns the employee with the given ID and null if not found.
    public Employee? GetEmployee(string employeeId)
    {
        _employees.TryGetValue(employeeId, out Employee? employee);
        return employee;
    }
    
}