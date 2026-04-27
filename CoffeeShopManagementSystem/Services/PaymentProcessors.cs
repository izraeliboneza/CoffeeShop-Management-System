using CoffeeShopManagementSystem.Interfaces;

namespace CoffeeShopManagementSystem.Services;

// This file contains simulated payment processors for the system.

// Handles cash payments.
public class CashPaymentProcessor : IPaymentProcessor
{
    public string PaymentMethod => "Cash";

    // Simulates a simple cash payment check.
    public bool ProcessPayment(decimal amount)
    {
        return amount > 0;
    }

    // Simulates a cash payment using the amount received from the customer.
    public bool ProcessCashPayment(decimal amount, decimal cashReceived)
    {
        if (amount <= 0)
        {
            return false;
        }

        if (cashReceived < amount)
        {
            return false;
        }

        return true;
    }

    // Calculates how much change the customer should receive.
    public decimal CalculateChange(decimal amount, decimal cashReceived)
    {
        if (!ProcessCashPayment(amount, cashReceived))
        {
            return 0;
        }

        return cashReceived - amount;
    }
}

// Handles card payments.
public class CardPaymentProcessor : IPaymentProcessor
{
    public string PaymentMethod => "Card";

    // Card payment is always accepted in this simulation.
    public bool ProcessPayment(decimal amount)
    {
        return true;
    }
}

// Handles Vipps payments.
public class VippsPaymentProcessor : IPaymentProcessor
{
    public string PaymentMethod => "Vipps";

    // Vipps payment is always accepted in this simulation.
    public bool ProcessPayment(decimal amount)
    {
        return true;
    }
}