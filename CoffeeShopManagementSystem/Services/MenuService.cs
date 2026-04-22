using CoffeeShopManagementSystem.Entities;
using CoffeeShopManagementSystem.Interfaces;
using CoffeeShopManagementSystem.Utils;

namespace CoffeeShopManagementSystem.Services;

//This class handles all menu displays and user interactions.
//Program.cs call on MenuService when the user selects a menu option.
public class MenuService
{
    private readonly OrderService _orderService;
    private readonly ReportService _reportService;

    //Coffee menu with all available products and prices.
    private readonly List<Coffee> _coffeeMenu = new()
    {
        new Coffee(1, "Espresso",                   38m),
        new Coffee(2, "Americano",                  47m),
        new Coffee(3, "Cappuccino",                 54m),
        new Coffee(4, "Caffe Latte",                62m),
        new Coffee(5, "Mokka (Moccalatte)",         75m),
        new Coffee(6, "Traktekaffe / Svart kaffe",  42m),
    };

    public MenuService()
    {
        JsonFileService fileService = new JsonFileService();
        _orderService = new OrderService(fileService);
        _reportService = new ReportService(_orderService);
    }

    public void ShowCoffeeMenu(Employee employee)
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("      COFFEE SHOP MANAGEMENT SYSTEM");
        Console.WriteLine("========================================");
        Console.WriteLine("Coffee Menu");
        Console.WriteLine("----------------------------------------");

        foreach (Coffee coffee in _coffeeMenu)
        {
            Console.WriteLine($"{coffee.Id}. {coffee.Name} {coffee.Price} kr");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }

    public void ShowNewOrderMenu(Employee employee)
    {
        _orderService.StartNewOrder(employee.Id);

        bool inOrderMenu = true;

        while (inOrderMenu)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("      COFFEE SHOP MANAGEMENT SYSTEM");
            Console.WriteLine("========================================");
            Console.WriteLine("Register New Order");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("1. Add coffee to order");
            Console.WriteLine("2. View current order");
            Console.WriteLine("3. Remove item from current order");
            Console.WriteLine("4. Complete and pay order");
            Console.WriteLine("5. Cancel current order");
            Console.WriteLine();
            Console.WriteLine("0. Back");
            Console.WriteLine("----------------------------------------");
            Console.Write("Choose an option: ");

            int choice = InputValidator.GetMenuChoice(0, 5);

            switch (choice)
            {
                case 1:
                    AddCoffeeToOrder();
                    break;
                case 2:
                    ViewCurrentOrder();
                    break;
                case 3:
                    RemoveItemFromOrder();
                    break;
                case 4:
                    if (CompleteAndPayOrder())
                    {
                        inOrderMenu = false;
                    }
                    break;
                case 5:
                    _orderService.CancelOrder();
                    inOrderMenu = false;
                    break;
                case 0:
                    _orderService.CancelOrder();
                    inOrderMenu = false;
                    break;
            }
        }
    }

    private void AddCoffeeToOrder()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("Add Coffee to Order");
        Console.WriteLine("========================================");

        foreach (Coffee coffee in _coffeeMenu)
        {
            Console.WriteLine($"{coffee.Id}. {coffee.Name} {coffee.Price} kr");
        }

        Console.WriteLine("0. Back");
        Console.WriteLine("----------------------------------------");
        Console.Write("Choose coffee: ");

        int choice = InputValidator.GetMenuChoice(0, _coffeeMenu.Count);
        if (choice == 0) return;

        Coffee selected = _coffeeMenu[choice - 1];

        Console.Write("Quantity: ");
        int quantity = InputValidator.GetPositiveInt();

        _orderService.AddToOrder(selected, quantity);

        Console.WriteLine($"Added {quantity}x {selected.Name} to order.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void ViewCurrentOrder()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("Current Order");
        Console.WriteLine("========================================");

        Order? order = _orderService.GetCurrentOrder();

        if (order is null || !order.Items.Any())
        {
            Console.WriteLine("No items in current order.");
        }
        else
        {
            foreach (OrderItem item in order.Items)
            {
                Console.WriteLine($"{item.Coffee.Name}, {item.Quantity}   {item.Subtotal} kr");
            }
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Total: {order.TotalPrice} kr");
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }

    private void RemoveItemFromOrder()
    {
        Order? order = _orderService.GetCurrentOrder();

        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("Remove Item");
        Console.WriteLine("========================================");

        if (order is null || !order.Items.Any())
        {
            Console.WriteLine("No items to remove.");
            Console.ReadKey();
            return;
        }

        foreach (OrderItem item in order.Items)
        {
            Console.WriteLine($"{item.Coffee.Id}.{item.Coffee.Name} x{item.Quantity}");
        }

        Console.WriteLine();
        Console.WriteLine("0. Back");
        Console.WriteLine("----------------------------------------");
        Console.Write("Enter coffee number to remove: ");

        int choice = InputValidator.GetMenuChoice(0, _coffeeMenu.Count);
        if (choice == 0) return;

        bool removed = _orderService.RemoveFromOrder(choice);
        Console.WriteLine(removed ? "\nItem removed." : "\nItem not found in order.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private bool CompleteAndPayOrder()
    {
        Order? order = _orderService.GetCurrentOrder();

        if (order is null || !order.Items.Any())
        {
            Console.WriteLine("The order is empty!.");
            Console.ReadKey();
            return false;
        }

        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("Complete Order");
        Console.WriteLine("========================================");
        Console.WriteLine();

        foreach (OrderItem item in order.Items)
        {
            Console.WriteLine($"{item.Coffee.Name} x{item.Quantity}   {item.Subtotal} kr");
        }

        Console.WriteLine();
        Console.WriteLine($"Total: {order.TotalPrice} kr");
        Console.WriteLine();
        Console.WriteLine("Select payment method:");
        Console.WriteLine("1. Cash");
        Console.WriteLine("2. Card");
        Console.WriteLine("3. Vipps");
        Console.WriteLine();
        Console.WriteLine("0. Back");
        Console.WriteLine("----------------------------------------");
        Console.Write("Choose: ");

        int choice = InputValidator.GetMenuChoice(0, 3);
        if (choice == 0) return false;

        IPaymentProcessor processor = choice switch
        {
            1 => new CashPaymentProcessor(),
            2 => new CardPaymentProcessor(),
            3 => new VippsPaymentProcessor(),
            _ => new CashPaymentProcessor()
        };

        bool success = _orderService.CompleteOrder(processor);

        if (success)
        {
            Console.WriteLine($"\nPayment via {processor.PaymentMethod} accepted.");
            Console.WriteLine("Order saved.");
        }
        else
        {
            Console.WriteLine("\nPayment failed. Please try again.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        return success;
    }

    public void ShowOrderHistoryMenu(Employee employee)
    {
        bool inMenu = true;

        while (inMenu)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("      COFFEE SHOP MANAGEMENT SYSTEM");
            Console.WriteLine("========================================");
            Console.WriteLine("Order History:");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("1. View all orders");
            Console.WriteLine("2. Search by order ID");
            Console.WriteLine("3. Filter by date");
            Console.WriteLine();
            Console.WriteLine("0. Back");
            Console.WriteLine("----------------------------------------");
            Console.Write("Choose an option: ");

            int choice = InputValidator.GetMenuChoice(0, 3);

            switch (choice)
            {
                case 1:
                    ViewAllOrders();
                    break;
                case 2:
                    SearchByOrderId();
                    break;
                case 3:
                    FilterByDate();
                    break;
                case 0:
                    inMenu = false;
                    break;
            }
        }
    }

    private void ViewAllOrders()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("All Orders");
        Console.WriteLine("========================================");

        List<Order> orders = _orderService.GetAllOrders();

        if (!orders.Any())
        {
            Console.WriteLine("No orders found.");
        }
        else
        {
            foreach (Order order in orders)
            {
                Console.WriteLine($"{order.OrderId} {order.Timestamp:dd.MM.yyyy HH:mm}   {order.TotalPrice} kr   {order.PaymentMethod}");
            }
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }

    private void SearchByOrderId()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("Search by Order ID");
        Console.WriteLine("========================================");
        Console.Write("Enter order ID (0 to cancel): ");

        string input = Console.ReadLine()?.Trim() ?? string.Empty;
        if (input == "0") return;

        Order? order = _orderService.GetOrderById(input);

        Console.WriteLine();

        if (order is null)
        {
            Console.WriteLine($"No order found with ID: {input}");
        }
        else
        {
            Console.WriteLine($"Order ID:  {order.OrderId}");
            Console.WriteLine($"Date/Time: {order.Timestamp:dd.MM.yyyy HH:mm}");
            Console.WriteLine($"Employee:  {order.EmployeeId}");
            Console.WriteLine($"Payment:   {order.PaymentMethod}");
            Console.WriteLine();

            foreach (OrderItem item in order.Items)
            {
                Console.WriteLine($"{item.Coffee.Name} x{item.Quantity}   {item.Subtotal} kr");
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Total: {order.TotalPrice} kr");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }

    private void FilterByDate()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("Filter by Date");
        Console.WriteLine("========================================");
        Console.Write("Enter date (dd.mm.yyyy): ");

        DateTime date = InputValidator.GetDate();
        List<Order> orders = _orderService.GetOrdersByDate(date);

        Console.WriteLine();

        if (!orders.Any())
        {
            Console.WriteLine($"No orders found for {date:dd.MM.yyyy}.");
        }
        else
        {
            foreach (Order order in orders)
            {
                Console.WriteLine($"{order.OrderId} {order.Timestamp:HH:mm}   {order.TotalPrice} kr   {order.PaymentMethod}");
            }
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }

    public void ShowSalesOverviewMenu(Employee employee)
    {
        bool inMenu = true;

        while (inMenu)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("      COFFEE SHOP MANAGEMENT SYSTEM");
            Console.WriteLine("========================================");
            Console.WriteLine("Sales Overview:");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine("1. View today's sales");
            Console.WriteLine("2. View sales by date");
            Console.WriteLine("3. View payment summary");
            Console.WriteLine("4. View best selling products");
            Console.WriteLine();
            Console.WriteLine("0. Back");
            Console.WriteLine("----------------------------------------");
            Console.Write("Choose an option: ");

            int choice = InputValidator.GetMenuChoice(0, 4);

            switch (choice)
            {
                case 1:
                    ViewTodaysSales();
                    break;
                case 2:
                    ViewSalesByDate();
                    break;
                case 3:
                    ViewPaymentSummary();
                    break;
                case 4:
                    ViewBestSellingProducts();
                    break;
                case 0:
                    inMenu = false;
                    break;
            }
        }
    }

    private void ViewTodaysSales()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("Today's Sales");
        Console.WriteLine("========================================");

        var (revenue, count) = _reportService.GetSalesByDate(DateTime.Today);

        Console.WriteLine($"Date: {DateTime.Today:dd.MM.yyyy}");
        Console.WriteLine();
        Console.WriteLine($"Total revenue:    {revenue} NOK");
        Console.WriteLine($"Total orders:     {count}");

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }

    private void ViewSalesByDate()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("Sales by Date:");
        Console.WriteLine("========================================");
        Console.Write("Enter date (dd.mm.yyyy): ");

        DateTime date = InputValidator.GetDate();
        var (revenue, count) = _reportService.GetSalesByDate(date);

        Console.WriteLine();
        Console.WriteLine($"Date: {date:dd.MM.yyyy}");
        Console.WriteLine();
        Console.WriteLine($"Total revenue:    {revenue} NOK");
        Console.WriteLine($"Total orders:     {count}");

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }

    private void ViewPaymentSummary()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("Payment Summary:");
        Console.WriteLine("========================================");

        Dictionary<string, decimal> summary = _reportService.GetPaymentSummary(DateTime.Today);
        decimal total = summary.Values.Sum();

        string[] methods = { "Cash", "Card", "Vipps" };

        foreach (string method in methods)
        {
            summary.TryGetValue(method, out decimal amount);
            Console.WriteLine($"{method}: {amount} NOK");
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Total: {total} NOK");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }

    private void ViewBestSellingProducts()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("Best Selling Products:");
        Console.WriteLine("========================================");

        List<(string Name, int UnitsSold)> products = _reportService.GetBestSellingProducts();

        if (!products.Any())
        {
            Console.WriteLine("No sales data available yet.");
        }
        else
        {
            int rank = 1;
            foreach (var (name, units) in products)
            {
                Console.WriteLine($"{rank++}. {name} {units} sold");
            }
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("Press any key to go back...");
        Console.ReadKey();
    }
}