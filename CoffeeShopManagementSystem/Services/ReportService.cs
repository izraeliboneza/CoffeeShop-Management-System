using CoffeeShopManagementSystem.Entities;

namespace CoffeeShopManagementSystem.Services;

//Generates sales reports for the supervisor menu.

//All the data comes from OrderService.
public class ReportService
{
    private readonly OrderService _orderService;

    //OrderService is passed in so ReportService can access saved orders.
    public ReportService(OrderService orderService)
    {
        _orderService = orderService;
    }

    //Returns total revenue and number of orders for a given date.
    public (decimal Revenue, int OrderCount) GetSalesByDate(DateTime date)
    {
        List<Order> completed = _orderService
            .GetOrdersByDate(date)
            .Where(o => o.IsCompleted)
            .ToList();

        decimal revenue = completed.Sum(o => o.TotalPrice);
        int count = completed.Count;

        return (revenue, count);
    }
    
    //Returns how much was paid with each payment method for a given date.
    public Dictionary<string, decimal> GetPaymentSummary(DateTime date)
    {
        return _orderService
            .GetOrdersByDate(date)
            .Where(o => o.IsCompleted)
            .GroupBy(o => o.PaymentMethod)
            .ToDictionary(g => g.Key, g => g.Sum(o => o.TotalPrice));
    }

    //Returns a list of products ranked by how many units were sold.
    public List<(string Name, int UnitsSold)> GetBestSellingProducts()
    {
        return _orderService
            .GetAllOrders()
            .Where(o => o.IsCompleted)
            .SelectMany(o => o.Items)
            .GroupBy(i => i.Coffee.Name)
            .Select(g => (g.Key, g.Sum(i => i.Quantity)))
            .OrderByDescending(x => x.Item2)
            .ToList();
    }
}