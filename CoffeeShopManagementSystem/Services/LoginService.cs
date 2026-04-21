using CoffeeShopManagementSystem.Entities;

namespace CoffeeShopManagementSystem.Services;

//This class handles logic for the application.
//Uses EmployeeServices to check if the entered ID is valid.
public class LoginService
{
    private readonly EmployeeService _employeeService;
    
    //EmployeeService is passed in so LoginService can look up employees.
    public LoginService(EmployeeService employeeService)
        {
        _employeeService = employeeService;
        }
    
    //This method tries to find and employee with the given ID.
    //Returns the employee or null if not found.
    public Employee? LogIn(string employeeId)
    {
        return _employeeService.GetEmployee(employeeId);
    }
}

