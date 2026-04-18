using System.Text.Json.Serialization;

namespace CoffeeShopManagementSystem.Entities;

// Represents an order placed at the register.
public class Order
{
    private string _orderId = string.Empty;
    private string _employeeId = string.Empty;
    private string _paymentMethod = string.Empty;

    // ID of the order that was placed.
    public string OrderId
    {
        get => _orderId;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Order ID cannot be empty.");

            _orderId = value;
        }
    }

    // ID of the employee who registered the order.
    public string EmployeeId
    {
        get => _employeeId;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Employee ID cannot be empty.");

            _employeeId = value;
        }
    }

    // Date and time the order was placed.
    public DateTime Timestamp { get; set; }

    // List of all items in the order.
    public List<OrderItem> Items { get; set; } = new();

    // Payment method used for the order.
    public string PaymentMethod
    {
        get => _paymentMethod;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Payment method cannot be empty.");

            _paymentMethod = value;
        }
    }

    // Returns true when the order is completed and paid.
    public bool IsCompleted { get; set; }

    // Calculates total price for all items in the order.
    // [JsonIgnore] means this value is calculated and not stored in JSON.
    [JsonIgnore]
    public decimal TotalPrice => Items.Sum(item => item.Subtotal);

    // Constructor with no parameters for JSON deserialization.
    public Order()
    {
    }

    // Creates a new order and generates an order ID.
    public Order(string employeeId)
    {
        EmployeeId = employeeId;
        Timestamp = DateTime.Now;
        OrderId = GenerateOrderId();
        IsCompleted = false;
    }

    // Creates an ID from date and time, for example: 20260417-122355
    private static string GenerateOrderId()
    {
        return DateTime.Now.ToString("yyyyMMdd-HHmmss");
    }

    // Adds a coffee to the order.
    // If the coffee already exists in the order, the quantity is increased.
    public void AddItem(Coffee coffee, int quantity = 1)
    {
        if (coffee is null)
            throw new ArgumentNullException(nameof(coffee));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0.");

        OrderItem? existingItem = Items.FirstOrDefault(item => item.Coffee.Id == coffee.Id);

        if (existingItem is null)
        {
            Items.Add(new OrderItem(coffee, quantity));
        }
        else
        {
            existingItem.Quantity += quantity;
        }
    }

    // Removes an order item by coffee ID.
    // Returns true if removed, false if coffee is not found.
    public bool RemoveItem(int coffeeId)
    {
        OrderItem? item = Items.FirstOrDefault(orderItem => orderItem.Coffee.Id == coffeeId);

        if (item is null)
            return false;

        Items.Remove(item);
        return true;
    }

    // Marks the order as completed and sets the payment method.
    public void CompleteOrder(string paymentMethod)
    {
        PaymentMethod = paymentMethod;
        IsCompleted = true;
    }
}