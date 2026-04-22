namespace CoffeeShopManagementSystem.Interfaces;

// Defines what a payment processor must support.
// Implemented by Cash, Card, and Vipps payment processors.
// This allows OrderService to remain independent of specific payment types.
public interface IPaymentProcessor
{
    // Name of the payment method used for the order.
    string PaymentMethod { get; }

    // Attempts to process payment for the given amount.
    // Returns true if the payment succeeds, otherwise false.
    bool ProcessPayment(decimal amount);
}