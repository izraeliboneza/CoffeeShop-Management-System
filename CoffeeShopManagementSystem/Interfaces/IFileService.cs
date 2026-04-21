using CoffeeShopManagementSystem.Entities;

namespace CoffeeShopManagementSystem.Interfaces;


// This interface defines what a file service must be able to do.
//JsonFileService implements this interface and handles the actual saving and loading.
//Because OrderService uses IfileService and not JsonFileService directly,
//you could replace JSON storage with a database later without having to
//change anything in OrderService.
public interface IFileService
{
    // Saves completed order to the backing store.
    public void Save(Order order);

    //Loads the saved orders from the backing store. 
    //Return an empty list if there is no orders saved.
    public List<Order> LoadOrders();
}