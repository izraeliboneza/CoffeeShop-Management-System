using CoffeeShopManagementSystem.Entities;

namespace CoffeeShopManagementSystem.Interfaces;

// Defines what a file service must support.
// JsonFileService implements this interface and handles actual persistence.
// This allows OrderService to remain independent of storage type.
public interface IFileService
{
    // Saves a completed order to storage.
    void Save(Order order);

    // Loads all saved orders.
    // Returns an empty list if no orders are stored.
    List<Order> LoadOrders();
}