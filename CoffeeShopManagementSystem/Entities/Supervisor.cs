using System.Reflection.Metadata;

namespace CoffeeShopManagementSystem.Entities
{
    //Supervisor employee inherits Id, Name and ToString() from Employee.
    public class Supervisor : Employee
    {
        //Return the role sown in the menu + header.
        public override string Role => "Supervisor";

        //Creates a new Supervisor with ID and name.
        public Supervisor(string id, string name) : base(id, name) { }

        //Supervisor shows ID and role in the header.
        public override void DisplayInfo()
        {
            Console.WriteLine($"Logged in as: {Role}");
        }

        //Supervisor has accsess to all menu options:
        //NEw Order, Order History, Sales Overview, Coffee Menu, Switch USer and Exit.
        public override List<int> GetMenuOptions()
        {
            return new List<int>{1, 2, 3, 4, 5, 6};
        }
    }
}