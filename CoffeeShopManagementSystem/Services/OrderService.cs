using CoffeeShopManagementSystem.Entities;
using CoffeeShopManagementSystem.Interfaces;

namespace CoffeeShopManagementSystem.Services;

//This class handles all order logic in the system.


//Creates, updates and completes orders.
public class OrderService
{ 
    private readonly IFileService _fileService;

    //The order in process of being set.
    private Order? _currentOrder;
    
    //IFileService is used so OrderService can save and load orders.
    public OrderService(IFileService fileService)
    {
        _fileService = fileService;
    }

    //This will return true if there is an active order in progress.
    public bool HasActiveOrder => _currentOrder is not null;

    //Starts a new order and registers which employee is placing it.
    public Order StartNewOrder(string employeeId)
    {
        _currentOrder = new Order(employeeId);
        return _currentOrder;
    }

    //Returns the order in progress or null if none exists.
    public Order? GetCurrentOrder()
    {
        return _currentOrder;
    }

    //adds a coffee to the current order.
    public void AddToOrder(Coffee coffee, int quantity = 1)
    {
        if (_currentOrder is null)
        {
            throw new InvalidOperationException("No active order. Start a new order.");
        }
        _currentOrder.AddItem(coffee, quantity);
    }

    //removes a coffee from the current order by coffee ID. Returns false if the coffee is not found in the order.
    public bool RemoveFromOrder(int coffeeId)
    {
        if (_currentOrder is null)
        {
            return false;
        }
        return _currentOrder.RemoveItem(coffeeId);
    }

    //Completes the current order, processes payment and saves it.
    //Returns false if there is no active order or the order is empty.
    public bool CompleteOrder(IPaymentProcessor processor)
    {
        if (_currentOrder is null || !_currentOrder.Items.Any())
        {
            return false;
        }

        //Process the payment. If payment fails, return false.
        if (!processor.ProcessPayment(_currentOrder.TotalPrice))
        {
            return false;
        }

        //Mark the order as completed and saves it.
        _currentOrder.PaymentMethod = processor.PaymentMethod;
        _currentOrder.IsCompleted = true;

        _fileService.Save(_currentOrder);

        // Clear the active order
        _currentOrder = null;
        return true;
    }

    //cancels the current order without saving it.
    public void CancelOrder()
    {
        _currentOrder = null;
    }

    //Returns all saved orders from List<Orders>.
    public List<Order> GetAllOrders()
    {
        return _fileService.LoadOrders();
    }

    //Returns a single order by its ID and null if not found.
    public Order? GetOrderById(string orderId)
    {
        return _fileService.LoadOrders()
            .FirstOrDefault(o => o.OrderId == orderId);
    }

    //returns all orders placed on a specific date
    public List<Order> GetOrdersByDate(DateTime date)
    {
        return _fileService.LoadOrders()
            .Where(o => o.Timestamp.Date == date.Date)
            .ToList();
    }
}