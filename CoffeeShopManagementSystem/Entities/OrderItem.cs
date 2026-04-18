namespace CoffeeShopManagementSystem.Entities;

// Represents one item in an order (a coffee and its quantity).
public class OrderItem
{
    private Coffee _coffee = null!;
    private int _quantity;

    // The coffee that was ordered.
    public Coffee Coffee
    {
        get => _coffee;
        set
        {
            _coffee = value ?? throw new ArgumentNullException(nameof(value), "Coffee cannot be null.");
        }
    }

    // How many of this coffee was ordered.
    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Quantity must be greater than 0.");

            _quantity = value;
        }
    }

    // Calculates the total price for this order item.
    public decimal Subtotal => Coffee.Price * Quantity;

    // Constructor with no parameters for JSON deserialization.
    public OrderItem()
    {
    }

    // Creates a new OrderItem with a coffee and a quantity.
    public OrderItem(Coffee coffee, int quantity)
    {
        Coffee = coffee;
        Quantity = quantity;
    }
}