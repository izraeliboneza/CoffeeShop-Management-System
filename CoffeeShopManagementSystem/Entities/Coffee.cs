namespace CoffeeShopManagementSystem.Entities;

// Represents a coffee item in the menu.
public class Coffee
{
    private int _id;
    private string _name = string.Empty;
    private decimal _price;

    // ID to identify the coffee in the menu (1, 2, 3...).
    public int Id
    {
        get => _id;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Coffee ID must be greater than 0.");

            _id = value;
        }
    }

    // Name of the coffee, for example "Espresso" or "Cappuccino".
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Coffee name cannot be empty.");

            _name = value;
        }
    }

    // Price in NOK.
    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative.");

            _price = value;
        }
    }

    // Constructor with no parameters for JSON deserialization.
    public Coffee()
    {
    }

    // Create a new coffee with all required information.
    public Coffee(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Id}. {Name} - {Price} NOK";
    }
}