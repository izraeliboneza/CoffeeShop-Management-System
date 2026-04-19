namespace CoffeeShopManagementSystem.Entities
{
    // Base class for all employees in the system.
    public abstract class Employee
    {
        private string _id;
        private string _name;

        // Employee ID, for example "B001" or "S001".
        public string Id
        {
            get => _id;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Employee ID cannot be empty.");

                _id = value;
            }
        }

        // Full name of the employee.
        public string Name
        {
            get => _name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");

                _name = value;
            }
        }

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