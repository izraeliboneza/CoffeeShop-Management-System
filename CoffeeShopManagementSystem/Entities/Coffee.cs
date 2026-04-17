namespace CoffeeShopManagementSystem.Entities;

//Coffee item in the menu. Each coffee has a fixed size.
public class Coffee
{
    //ID to identify the coffee in the menu (1,2,3...).
    public int Id { get; set; }
    
    //Name of the coffee, for example "Espresso" or "Cappuccino."
    //Name is non-nullable string. Coffee should allways have a name.
    public string Name { get; set; } =  string.Empty;
    
    //Price in NOK.
    public decimal Price { get; set; }

    //Constructor with no parameters for JSON deserialization.
    public Coffee()
    {
    }

    //Create a new coffee with all required information.
    public Coffee(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}