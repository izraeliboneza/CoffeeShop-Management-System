using CoffeeShopManagementSystem.Entities;
using CoffeeShopManagementSystem.Services;


namespace CoffeeShopManagementSystem.Tests;

public class OrderServiceTests
{
    [Fact]
    public void CompleteOrder_WithItems_SavesCompletedOrder()
    {
        var fakeFileService = new FakeFileService();
        var orderService = new OrderService(fakeFileService);

        var espresso = new Coffee { Id = 1, Name = "Espresso", Price = 45 };
        var latte = new Coffee { Id = 2, Name = "Latte", Price = 50 };

        orderService.StartNewOrder("B001");
        orderService.AddToOrder(espresso, 2);
        orderService.AddToOrder(latte, 1);

        var result = orderService.CompleteOrder(new CardPaymentProcessor());

        var orders = orderService.GetAllOrders();

        Assert.True(result);
        Assert.Single(orders);
        Assert.True(orders[0].IsCompleted);
        Assert.Equal("B001", orders[0].EmployeeId);
        Assert.Equal(2, orders[0].Items.Count);
        Assert.Equal(140, orders[0].TotalPrice);
        Assert.Equal("Card", orders[0].PaymentMethod);
    }

    [Fact]
    public void AddToOrder_WithSameCoffee_IncreasesQuantity()
    {
        var fakeFileService = new FakeFileService();
        var orderService = new OrderService(fakeFileService);

        var cappuccino = new Coffee { Id = 1, Name = "Cappuccino", Price = 55 };

        orderService.StartNewOrder("B002");
        orderService.AddToOrder(cappuccino, 2);
        orderService.AddToOrder(cappuccino, 1);
        orderService.CompleteOrder(new CardPaymentProcessor());

        var order = orderService.GetAllOrders()[0];

        Assert.Single(order.Items);
        Assert.Equal(3, order.Items[0].Quantity);
        Assert.Equal(165, order.TotalPrice);
    }

    [Fact]
    public void CancelOrder_WithActiveOrder_DoesNotSaveOrder()
    {
        var fakeFileService = new FakeFileService();
        var orderService = new OrderService(fakeFileService);

        var americano = new Coffee { Id = 2, Name = "Americano", Price = 40 };

        orderService.StartNewOrder("B003");
        orderService.AddToOrder(americano, 1);
        orderService.CancelOrder();

        Assert.Empty(orderService.GetAllOrders());
    }
}