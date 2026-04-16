using System.ComponentModel.Design;

namespace CoffeeShopManagementSystem.Entities
{
    // klassen representerer en ansatt type: Barista.
    // Arver fra Employee-klassen.
    public class Barista : Employee
    {
        // Returnerer rolle-navnet som vises i menyen.
        public override string Role => "Barista";
        
        // Oppretter ny barista med id og navn.
        // Kaller konstruktøren via "base".
        public Barista(string id, string name) : base(id, name) {}
        
        // Viser ansatt info i headeren.
        public override void DisplayInfo()
        {
            Console.WriteLine($"Logged in as: {Role}");
            Console.WriteLine($"Name: {Name}");
        }

        // Barista skal kun ha tilgang til : New order, Coffee MenuCommand, switch user og exit.
        public override List<int> GetMenuOptions()
        {
            return new List<int> { 0, 1, 2, 3, 4 };
        }
    }
}

