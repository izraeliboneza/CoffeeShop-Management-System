using CoffeeShopManagementSystem.Services;



namespace CoffeeShopManagementSystem.Tests;

//Tests for payment processors.
//This type of tests dont call for as many asserts as in OrderTest.cs. 
//ProcessPayment returns bools and therefore only necessary to test bools.
public class PaymentProcessorTests
{
    //TEST 1: Cash payment should calculate correct change.
    [Fact]
    public void CashPaymentProcessor_WithMoreCashThanNeeded_ReturnsCorrectChange()
    {
        //Arrange - create cash processor and payment details.
        var processor = new CashPaymentProcessor();
        decimal orderAmount = 75.0m;
        decimal cashGiven = 200.0m;

        //Act - process the payment.
        var change = processor.CalculateChange(orderAmount, cashGiven);

        //Assert - should succeed with correct change.
        Assert.Equal(125.0m, change); //200-75=125
    }
    
    // TEST 2: CardPayemntProcessor should return true for valid card.
    [Fact]
    public void CardPaymentProcessor_WithValidCard_ReturnsTrue()
    {
        // Arrange - create card processor and payment details.
        var processor = new CardPaymentProcessor();
        decimal orderAmount = 165.0m;
        
        // Act - process card payemnt.
        var result = processor.ProcessPayment(orderAmount);
        
        // Assert - should return true for successful card payment.
        Assert.True(result);
    }
    
    //TEST 3: ProcessCashPayment with insufficient cash should return false.
    [Fact]
    public void CashPaymentProcessor_WithInsufficientCash_ReturnsFalse()
    {
        // Arrange - create processor where customer doesnt have enough money.
        var processor = new CashPaymentProcessor();
        decimal orderAmount = 110.0m;
        decimal cashGiven = 3.0m; //Cash given is only 3kr but the order is 110kr.
        
        // Act - try to process the order with not enough cash.
        var result = processor.ProcessCashPayment(orderAmount, cashGiven);
        
        //Assert - Should return false failed payment.
        Assert.False(result);
        
    }
}
