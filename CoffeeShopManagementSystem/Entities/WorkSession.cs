namespace CoffeeShopManagementSystem.Entities;

public class WorkSession
{
    public string EmployeeId { get; set; } = "";
    public string EmployeeName { get; set; } = "";
    public DateTime LoginTime { get; set; }
    public DateTime LogoutTime { get; set; }
    public int Minutes { get; set; }
    public decimal HourlyWage { get; set; }
}