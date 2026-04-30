using CoffeeShopManagementSystem.Entities;
using CoffeeShopManagementSystem.Interfaces;

namespace CoffeeShopManagementSystem.Tests;

public class FakeFileService : IFileService
{
    private readonly List<Order> _orders = new();

    public void Save(Order order)
    {
        _orders.Add(order);
    }

    public List<Order> LoadOrders()
    {
        return _orders;
    }
}