using System.Text.Json.Serialization;
namespace CoffeeShopManagementSystem.Entities;
//Represents an order placed at the register.
public class Order
{
    // ID of the order that was placed.
    public string OrderId { get; set; } = string.Empty;

    //ID of the employee who registered the order.
    public string EmployeeId { get; set; } = string.Empty;

    // Date and time the order was placed.
    public DateTime Timestamp { get; set; }

    //List of all items in the order.
    public List<OrderItem> Items { get; set; } = new();

    //Payment method that are used are Cash Card or Vipps.
    public string PaymentMethod { get; set; } = string.Empty;

    //This returns true when order is paid.
    public bool IsCompleted { get; set; }

    // Calculates total price for all items in the order.
    //[JsonIgnore] means this is NOT saved to JSON, it is calculated from items.
    [JsonIgnore] public decimal TotalPrice => Items.Sum(i => i.Subtotal);

    //Constructor with no parameters for JSON deserialization.
    public Order()
    {
    }

    //Creates a new order and generates an order ID.
    public Order(string employeeId)
    {
        EmployeeId = employeeId;
        Timestamp = DateTime.Now;
        OrderId = GenerateOrderId();
    }

    //Creates an ID from date and time, example = Order: 20260417-122355
    private static string GenerateOrderId()
    {
        return $"Order: {DateTime.Now:yyyyMMdd-HHmmss}";
    }

    //Adds a coffee to the order.
    //If the same coffee is already there, it increases the quantity.
    public void AddItem(Coffee coffee, int quantity = 1)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than 0.");
        }

        var existing = Items.FirstOrDefault(i => i.Coffee.Id == coffee.Id);
        if (existing is null)
        {
            Items.Add(new OrderItem(coffee, quantity));
        }
        else
        {
            existing.Quantity += quantity;
        }
    }

    //Reduces quantity from an existing order line by coffee ID.
    //If quantity becomes 0 or less, the whole item is removed.
    //Returns true if item was found, false if coffee was not found.
    public bool ReduceItemQuantity(int coffeeId, int quantity = 1)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than 0.");
        }

        var item = Items.FirstOrDefault(i => i.Coffee.Id == coffeeId);
        if (item is null)
        {
            return false;
        }

        item.Quantity -= quantity;
        if (item.Quantity <= 0)
        {
            Items.Remove(item);
        }

        return true;
    }

    //Removes coffee from order line by coffee ID.
    //Returns true if removed, false if coffee is not found.
    public bool RemoveItem(int coffeeId)
    {
        var item = Items.FirstOrDefault(i => i.Coffee.Id == coffeeId);
        if (item is null)
        {
            return false;
        }

        Items.Remove(item);
        return true;
    }

}