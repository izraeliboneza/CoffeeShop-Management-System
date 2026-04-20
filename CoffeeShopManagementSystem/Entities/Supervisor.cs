namespace CoffeeShopManagementSystem.Entities
{
    // Supervisor employee inherits from abstract Employee class.
    public class Supervisor : Employee
    {
        // Creates a new Supervisor with ID and name.
        public Supervisor(string id, string name) : base(id, name)
        {
        }

        // Returns the role name shown in the menu and header.
        public override string GetRoleName()
        {
            return "Supervisor";
        }
    }
}