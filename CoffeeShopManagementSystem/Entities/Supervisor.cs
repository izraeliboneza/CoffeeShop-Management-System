using System.Reflection.Metadata;

namespace CoffeeShopManagementSystem.Entities
{
    // Supervisor arver fra Employee.
    // REpresenterer supervisor ansatt.
    public class Supervisor : Employee
    {
        // returnerer rolle-navnet.
        public override string Role => "Supervisor";

        // Oppretter en ny supervisor med ID og navn.
        public Supervisor(string id, string name) : base(id, name) { }

        // Supervisor skal vise kun ID or rolle.
        public override void DisplayInfo()
        {
            Console.WriteLine($"Logged in as: {Role}");
        }

        // Supervisor skal ha tilgang til alle menyene.
        // new order, order history, sales overviewe, coffee menu, switch UserStringHandle og exit.
        public override List<int> GetMenuOptions()
        {
            return new List<int>{1, 2, 3, 4, 5, 6};
        }
    }
}