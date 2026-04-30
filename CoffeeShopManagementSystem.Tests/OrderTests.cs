using CoffeeShopManagementSystem.Entities;
using Xunit;
using System;

namespace CoffeeShopManagementSystem.Tests;

public class OrderTests
{
    // TEST 1: New order should have correct initial  values.
    [Fact]
    public void Order_WhenCreated_HasCorrectInitialValues()
    {
        
        //Arrange - prepare employee ID. The one creating the order.
        var employeeId = "B004";

        //Act - create new order.
        var order = new Order(employeeId);

        // Assert - check all initial properties.
        Assert.Equal("B004", order.EmployeeId); 
        Assert.Empty(order.Items); //Items list should be empty.
        Assert.Equal(0, order.TotalPrice); //Total should be zero since there´s nothing in the order.
    }
    
    
    // TEST 2: REducing item quantity should reduce it correctly.
    [Fact]
    public void ReduceItemQuantity_WithValidQuantity_DecreasesQuantity()
    {
        // Arrange - crate an order with an item.
        var order = new Order ("B002");
        var coffee = new Coffee{Id = 1, Name = "Latte", Price = 50};
        order.AddItem(coffee, 7);
        
        
        // Act - reduce by 3
        var result = order.ReduceItemQuantity(1, 3);
        
        // Assert - quantoty should be reduced.
        Assert.True(result); //Should return true for success.
        Assert.Single(order.Items);
        Assert.Equal(4, order.Items[0].Quantity); //7-3=4.
        Assert.Equal(200, order.TotalPrice);
    }
    
    
    //TEST 3: Reducint Item Quantity To zero should remove the item.
    [Fact]
    public void ReduceItemQuantity_ToZero_RemovesItem()
    {
        // Arrange - create new order with an item.
        var order = new Order ("B001");
        var coffee = new Coffee{Id = 2, Name = "Espresso", Price = 45};
        order.AddItem(coffee, 2); //adds 2 Espressos.
        
        
        //Act - reduce the quantity if the item by more than the true quantity in order.
        var result = order.ReduceItemQuantity(2, 2);
        
        // Assert - item should be completely removed.
        Assert.True(result); //Returns true if successful.
        Assert.Empty(order.Items); //items should be removed
        Assert.Equal(0, order.TotalPrice); //should be zero
    }
}