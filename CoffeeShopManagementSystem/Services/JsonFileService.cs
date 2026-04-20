using System.Text.Json;
using CoffeeShopManagementSystem.Entities;
using CoffeeShopManagementSystem.Interfaces;
using CoffeeShopManagementSystem.Services;

namespace CoffeeShopManagementSystem.Services;

//This class handles saving orders to a JSON file.
//Implements IfileService so OrderService does not depend directly on JSON.
public class JsonFileService : IFileService
{
    //this is where all the orders will be stored.
    private readonly string _filePath;

    public JsonFileService(string filePath = "Orders.json")
    {
        _filePath = filePath;
    }
    
    //Save a completed order by loading all existing orders,
    //adding the new one, and writing everything back to the file.
    public void Save(Order order)
    {
        List<Order> Orders = LoadOrders();
        Orders.Add(order);
        string json = JsonSerializer.Serialize(
            Orders, new JsonSerializerOptions{WriteIndented = true});
        File.WriteAllText(_filePath, json);
    }

    //Loads all orders from the JSON file.
    //Returns an empty list if the file does not exist or is corrupt.
    public List<Order> LoadOrders()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Order>();
        }

        try
        {
            //Makes a new List<Order> out of the plain text in JSON file.
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Order>>(json) ?? new List<Order>();
        }
        catch (JsonException)
        {
            //if theres something wrong with the file,
            //start a fresh one instead of crashing.
            return new List<Order>();
        }
    }
}