namespace CoffeeShopManagementSystem.Entities
{
    //Barista employee inherits from abstract Employee class.
    public class Barista : Employee
    {
        //Returns the role name shown in the menu + header.
        public override string Role => "Barista";
        
        //Creates a new Barista with ID and name.
        //Calls the employee constructor from "base"
        public Barista(string id, string name) : base(id, name) {}
        
        //Displays employee info in the header
        //Barista also shows name.
        public override void DisplayInfo()
        {
        }

        //Barista only has access to: New Order, Coffee Menu, Switch User and Exit.
        public override List<int> GetMenuOptions()
        {
            return "Barista";
        }
    }
}