using System.Text.Json;
using CoffeeShopManagementSystem.Entities;
using CoffeeShopManagementSystem.Interfaces;

namespace CoffeeShopManagementSystem.Services;

// This class handles saving and loading orders to and from a JSON file.
// It implements IFileService so OrderService does not depend directly on JSON logic.
public class JsonFileService : IFileService
{
    // This stores the full path to the JSON file.
    private readonly string _filePath;

    // The constructor sets the file path to the Data/orders.json file
    // located in the project root folder, not inside bin/Debug/netX.X.
    public JsonFileService(string filePath = "Data/orders.json")
    {
        // AppContext.BaseDirectory points to the folder where the program runs,
        // usually bin/Debug/netX.X.
        var baseDir = AppContext.BaseDirectory;

        // Move step by step up from:
        // netX.X -> Debug -> bin -> project root
        var projectRoot = Directory
            .GetParent(baseDir)  // netX.X
            ?.Parent             // Debug
            ?.Parent             // bin
            ?.Parent             // Project root
            ?.FullName;

        // Stop the program with a clear error if the project root cannot be found.
        if (projectRoot == null)
        {
            throw new Exception("Could not resolve project root directory.");
        }

        // Build the full path to the JSON file in the Data folder.
        _filePath = Path.Combine(projectRoot, filePath);
    }

    // Saves a completed order by:
    // 1. Loading all existing orders
    // 2. Adding the new order
    // 3. Writing the updated list back to the JSON file
    public void Save(Order order)
    {
        List<Order> orders = LoadOrders();
        orders.Add(order);

        // Get the folder path from the full file path.
        var directory = Path.GetDirectoryName(_filePath);

        // Create the Data folder if it does not already exist.
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory!);
        }

        // Convert the order list to formatted JSON text.
        string json = JsonSerializer.Serialize(
            orders,
            new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON text to the file.
        File.WriteAllText(_filePath, json);
    }

    // Loads all orders from the JSON file.
    // Returns an empty list if the file does not exist or the JSON is invalid.
    public List<Order> LoadOrders()
    {
        // Return an empty list if the file has not been created yet.
        if (!File.Exists(_filePath))
        {
            return new List<Order>();
        }

        try
        {
            // Read all JSON text from the file.
            string json = File.ReadAllText(_filePath);

            // Convert the JSON text into a List<Order>.
            // If deserialization returns null, return an empty list instead.
            return JsonSerializer.Deserialize<List<Order>>(json) ?? new List<Order>();
        }
        catch (JsonException)
        {
            // If the JSON file is corrupted or invalid,
            // return an empty list instead of crashing the program.
            return new List<Order>();
        }
    }
}