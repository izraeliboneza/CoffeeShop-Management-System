namespace CoffeeShopManagementSystem.Entities
{
    // Base class for all employees in the system.
    public abstract class Employee
    {
        // Employee ID, for example "B001" or "S001".
        public string Id { get; protected set; }

        // Full name of the employee.
        public string Name { get; protected set; }

        // This runs when a Barista or Supervisor object is created.
        protected Employee(string id, string name)
        {
            Id = id;
            Name = name;
        }

        // Returns the role name of the employee.
        public abstract string GetRoleName();

        // Returns a short description of the employee.
        public override string ToString()
        {
            return $"Id: {Id}, Role: {GetRoleName()}";
        }
    }
}