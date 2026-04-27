using System.Text.Json;
using CoffeeShopManagementSystem.Entities;

namespace CoffeeShopManagementSystem.Services;

public class WorkSessionService
{
    private Employee? _employee;
    private DateTime _loginTime;

    // Ensures JSON is stored in project Data folder, not in bin
    private readonly string _path = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Data", "work_sessions.json")
    );

    public void Start(Employee employee)
    {
        _employee = employee;
        _loginTime = DateTime.Now;
    }

    public void End(decimal hourlyWage)
    {
        if (_employee is null) return;

        DateTime logoutTime = DateTime.Now;

        WorkSession session = new WorkSession
        {
            EmployeeId = _employee.Id,
            EmployeeName = _employee.Name,
            LoginTime = _loginTime,
            LogoutTime = logoutTime,
            Minutes = (int)(logoutTime - _loginTime).TotalMinutes,
            HourlyWage = hourlyWage
        };

        Save(session);

        // Prevent duplicate saves
        _employee = null;
    }

    public List<WorkSession> GetAll()
    {
        if (!File.Exists(_path))
        {
            return new List<WorkSession>();
        }

        string json = File.ReadAllText(_path);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<WorkSession>();
        }

        return JsonSerializer.Deserialize<List<WorkSession>>(json) ?? new List<WorkSession>();
    }

    private void Save(WorkSession session)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_path)!);

        List<WorkSession> sessions = GetAll();
        sessions.Add(session);

        string json = JsonSerializer.Serialize(sessions, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_path, json);
    }
}