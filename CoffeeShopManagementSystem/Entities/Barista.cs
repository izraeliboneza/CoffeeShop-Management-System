namespace CoffeeShopManagementSystem.Entities
{
    // Barista employee inherits from the abstract Employee class.
    public class Barista : Employee
    {
        // Creates a new Barista with ID and name.
        public Barista(string id, string name) : base(id, name)
        {
        }

        // Returns the role name shown in the menu and header.
        public override string GetRoleName()
        {
            return "Barista";
        }
    }
}