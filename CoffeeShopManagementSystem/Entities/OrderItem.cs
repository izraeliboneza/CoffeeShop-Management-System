namespace  CoffeeShopManagementSystem.Entities;

// This class represents one item in an order.
//A coffee and how many of it were ordered.
public class OrderItem
{
    // Name of coffee that was ordered.
    public Coffee Coffee { get; set; } = new();
    
    //How many of this coffee was ordered.
    public int Quantity { get; set; }
    
    //Calculates the total price for this order.
    public decimal Subtotal => Coffee.Price * Quantity;
    
    //Constructor with no paramater for JSON deserialization.
    public OrderItem() {}

    //Creates a new OrderItem with a coffee and a quantity.
    public OrderItem(Coffee coffee, int quantity)
    {
        Coffee =  coffee;
        Quantity = quantity;
    }
}