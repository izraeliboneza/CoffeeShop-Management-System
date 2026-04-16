namespace CoffeeShopManagementSystem.Entities
{
    // Baseklasse for alle ansatte i systemet.
    // Barista og Supervisor arver fra denne klasse.
    public abstract class Employee
    {
        // Ansatt-IDictionary<, for eks. "B001" eller "S001".
        public string Id { get; protected set; }
        
        // Fult navn på ansatte.
        public string Name { get; protected set; }
        
        // Rolle-navnet.
        // Barista og Supervisor klassene bestemmer hva dette blir.
        public abstract string Role { get; }
        
        protected Employee(string id, string name)
        {
            Id = id;
            Name = name;
        }
        
        // Viser ansatt info i hovedmenyen.
        public abstract void DisplayInfo();
        
        // Viser menyvalgene den ansatte har tilgang til.
        // Sub klassene kan både vise sin egen definert info eller ikke.
        public virtual List<int> GetMenuOptions()
        {
            return new List<int> { 0, 1 };
        }

        // DEtte returnerer kort info om Id og rolle.
        public override string ToString()
        {
            return $"Id: {Id}, Role: {Role}";
        }
    }
}