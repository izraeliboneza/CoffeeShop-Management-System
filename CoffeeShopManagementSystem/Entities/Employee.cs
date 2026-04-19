namespace CoffeeShopManagementSystem.Entities
{
    // Base class for all employee in the system.
    //Bariste and Supervisor.
    public abstract class Employee
    {
        //Employee ID, for example "B001" or "S001".
        public string Id { get; protected set; }
        
        // Full name of the employee.
        public string Name { get; protected set; }
        
        // Role name - each subclasses decides what this returns.
        public abstract string Role { get; }
        
        // This runs when a Barista or Supervisor object is created.
        protected Employee(string id, string name)
        {
            Id = id;
            Name = name;
        }
        
        //Displays info in the main menu header.
        public abstract void DisplayInfo();
        
        // Returns whcich menu options this employee has access to.
        public virtual List<int> GetMenuOptions()
        {
            return new List<int> { 0, 1 };
        }

        //Returns a short string like "B001 - Barista".
        public override string ToString()
        {
            return $"Id: {Id}, Role: {GetRoleName()}";
        }
    }
}