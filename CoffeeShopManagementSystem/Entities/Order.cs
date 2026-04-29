using System.Text.Json.Serialization;

namespace CoffeeShopManagementSystem.Entities;

// Represents an order placed at the register.
public class Order
{
    public string OrderId { get; set; } = string.Empty;
    public string EmployeeId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public string PaymentMethod { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }

    [JsonIgnore]
    public decimal TotalPrice => Items.Sum(i => i.Subtotal);

    public Order() { }

    public Order(string employeeId)
    {
        EmployeeId = employeeId;
        Timestamp = DateTime.Now;
        OrderId = GenerateOrderId();
    }

    private static string GenerateOrderId()
    {
        return $"{DateTime.Now:yyyyMMdd-HHmmss}";
    }

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

        if (quantity > item.Quantity)
        {
            return false;
        }

        item.Quantity -= quantity;

        if (item.Quantity == 0)
        {
            Items.Remove(item);
        }

        return true;
    }

    public int GetItemQuantity(int coffeeId)
    {
        var item = Items.FirstOrDefault(i => i.Coffee.Id == coffeeId);
        return item?.Quantity ?? 0;
    }

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