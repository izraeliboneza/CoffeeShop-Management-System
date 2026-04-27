using CoffeeShopManagementSystem.Entities;
using CoffeeShopManagementSystem.Services;
using CoffeeShopManagementSystem.Interfaces;
using Xunit;
using System.Collections.Generic;
using System.ComponentModel;

namespace CoffeeShopManagementSystem.Tests
{
    //Tests for the OrderService class.
    //OrderService handles creating and managing orders.
    public class OrderServiceTests
    {
        //Test 1: Creating order with correct items,
        [Fact]
        public void CreateOrder_WithOrderItems_StoreItemsCorrectly()
        {
            // Arrange - Set up the service and an item to order.
            var coffee1 = new Coffee{Id = 1, Name = "Espresso", Price = 45};
            var coffee2 = new Coffee{Id = 2, Name = "Latte", Price = 50};
            
            var orderItem1 = new OrderItem {Coffee = coffee1, Quantity = 2};
            var orderItem2 = new OrderItem {Coffee = coffee2, Quantity = 1};
            
            var items = new List<OrderItem>{orderItem1, orderItem2};
            
            
            //Act - create the order.
            var order = new Order("B001");
            order.Items.Add(orderItem1);
            order.Items.Add(orderItem2);
            
            // Assert- check that the order was created.
            Assert.NotNull(order.Items);
            Assert.Equal(2, order.Items.Count);
            Assert.Equal("Espresso", order.Items[0].Coffee.Name);
            Assert.Equal(2, order.Items[0].Quantity);
            Assert.Equal("Latte", order.Items[1].Coffee.Name);
            Assert.Equal(140, order.TotalPrice);
        }
        
        
        //Test 2 + Test 3: AddItem should add coffee to the order.
        //and increase number of same coffee(test3).
        [Fact]
        public void AddItem_AddsNewCoffeeAndIncreaseQuantityForSameCoffee()
        {
            var order = new Order("B002");
            var coffee = new Coffee{Id = 1, Name = "Cappuccino", Price = 55};
            
            //Act - add the coffee to the order using AddItem method.
            order.AddItem(coffee, 2);
            
            // Assert - check if the items were added.
            Assert.Single(order.Items); //Should have one item.
            Assert.Equal("Cappuccino", order.Items[0].Coffee.Name);
            Assert.Equal(2, order.Items[0].Quantity);
            Assert.Equal(110, order.TotalPrice); //3*55=110
            
            order.AddItem(coffee, 1); //TEst 3.
            
            //Assert - should still have one coffee but the quantity increases.
            Assert.Single(order.Items);
            Assert.Equal(3,  order.Items[0].Quantity);
            Assert.Equal(165, order.TotalPrice); //3*55=165
        }
        
        
        // Test 4: Remove item should remove coffee from order.
        [Fact]
        public void RemoveItem_RemovesItemFromOrder()
        {
            // Arrange - Create order with items
            var order = new Order("B003");
            var coffee1 = new Coffee{Id = 1, Name = "Mocha", Price = 60};
            var coffee2 = new Coffee{Id = 2, Name = "Americano", Price = 40};
            
            order.AddItem(coffee1, 2);
            order.AddItem(coffee2, 1);
            
            //Assert - order should have 2 items before removing
            Assert.Equal(2, order.Items.Count);
            Assert.Equal(160, order.TotalPrice); ///2*60+40=160
            
            //Act - remove one coffee
            var result = order.RemoveItem(1);
            
            //Assert - item should be removed.
            Assert.True(result);
            Assert.Single(order.Items); //Should have only one item left.
            Assert.Equal("Americano", order.Items[0].Coffee.Name); //Only Americano left in the order.
            Assert.Equal(40, order.TotalPrice); //Only Americano price left.
        }
    }
}