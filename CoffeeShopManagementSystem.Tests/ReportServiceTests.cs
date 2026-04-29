using CoffeeShopManagementSystem.Services;
using CoffeeShopManagementSystem.Entities;
using CoffeeShopManagementSystem.Interfaces;
using Xunit;
using System;
using System.Collections.Generic;

namespace CoffeeShopManagementSystem.Tests
{
    //Fake file service for testing - stores orders in memory instead of files.
    public class FakeFileService : IFileService
    {
        private List<Order> _orders = new List<Order>();

        public void Save(Order order)
        {
            _orders.Add(order);
        }

        public List<Order> LoadOrders()
        {
            return _orders;
        }
    }


    //Tests for the ReportService class.
    //The class generates sales reports and statistics.
    public class ReportServiceTests
    {
        // TEST 1: GetSalesByDate should calculate correct revenue and count.
        [Fact]
        public void GetSalesByDate_WithCompleteOrders_ReturnsCorrectRevenueAndCount()
        {
            // Arrange - create OrderService and ReportService
            var fakeFileService = new FakeFileService();
            var orderService = new OrderService(fakeFileService);
            var reportService = new ReportService(orderService);

            var espresso = new Coffee { Id = 1, Name = "Espresso", Price = 38m };
            var americano = new Coffee { Id = 2, Name = "Americano", Price = 47m };

            // Create and complete first order
            orderService.StartNewOrder("B001");
            orderService.AddToOrder(espresso, 2);
            orderService.CompleteOrder(new CardPaymentProcessor());


            // Create and complete second order  
            orderService.StartNewOrder("B002");
            orderService.AddToOrder(americano, 3);
            orderService.CompleteOrder(new CardPaymentProcessor());


            var allOrders = orderService.GetAllOrders();
            var orderDate = allOrders[0].Timestamp.Date; //using the actual date from the order.

            //Act - get sales numbers.
            var result = reportService.GetSalesByDate(orderDate);


            //Assert - should correct revenue and count.
            Assert.Equal(217, result.Revenue); //76+141=217
            Assert.Equal(2, result.OrderCount);
        }


        // TEST 2: GetSalesByDate should only count completed orders.
        [Fact]
        public void GetSalesByDate_WithIncompleteOrders_ReturnsOnlyCompletedOrders()

        {
            //Arrange - use fake file service
            var fakeFileService = new FakeFileService();
            var orderService = new OrderService(fakeFileService);
            var reportService = new ReportService(orderService);

            var cappuccino = new Coffee { Id = 3, Name = "Cappuccino", Price = 54m };

            orderService.StartNewOrder("B001");
            orderService.AddToOrder(cappuccino, 2); // 108kr (2 * 54)
            orderService.CompleteOrder(new CardPaymentProcessor());

            //creating an incomplete order.
            orderService.StartNewOrder("B002");
            orderService.AddToOrder(cappuccino, 5); // 270kr but not completed
            orderService.CancelOrder(); // Cancels the incomplete order

            var allOrders = orderService.GetAllOrders();
            var orderDate = allOrders[0].Timestamp.Date;

            //Act - get sales numbers.
            var result = reportService.GetSalesByDate(orderDate);


            //Assert - should only get the completed order.
            Assert.Equal(108, result.Revenue); // Only the completed order
            Assert.Equal(1, result.OrderCount); // Only 1 completed order}
        }
        
        
        // TEST 3: This is an edge case where GetSalesByDate with no orders should return zero.
        [Fact]
        public void GetSalesByDate_WithNoOrders_ReturnsZero()
        {
            // Arrange - use fake file service with no orders
            var fakeFileService = new FakeFileService();
            var orderService = new OrderService(fakeFileService);
            var reportService = new ReportService(orderService);

            // Act - get sales from today.
            var result = reportService.GetSalesByDate(DateTime.Today);

            // Assert - sales should be zero.
            Assert.Equal(0, result.Revenue);
            Assert.Equal(0, result.OrderCount);
        }
    }
}