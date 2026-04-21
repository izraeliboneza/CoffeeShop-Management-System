using CoffeeShopManagementSystem.Interfaces;

namespace CoffeeShopManagementSystem.Services;

//This class handles paymentmethods.

//Handles cashpayments.
public class CashPaymentProcessor : IPaymentProcessor
{
    public string PaymentMethod => "Cash";

    // Simulates a cash payment going through.
    public bool ProcessPayment(decimal amount)
    {
        return true;
    }
}

//Handles Vipps payment.
public class CardPaymentProcessor : IPaymentProcessor
{
    public string PaymentMethod => "Card";
    
    //Simulates a card payment going through.
    public bool ProcessPayment(decimal amount)
    {
        return true;
    }
}

// Handles card payment.
public class VippsPaymentProcessor : IPaymentProcessor
{
    public string PaymentMethod => "Vipps";
    
    // Simulates a vipps payment going through.
    public bool ProcessPayment(decimal amount)
    {
        return true;
    }
}

