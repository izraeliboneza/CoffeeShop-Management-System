using CoffeeShopManagementSystem.Entities;

namespace CoffeeShopManagementSystem.Interfaces;

// This interface defines what a payemnt processor must do.
//CashPaymentProcessor, VippsPaymentProcessor and CardPaymentProcessor all implement it.
//Because OrderService uses IPaymentProcessor and not the processors directly, its possible to
// add a new method later, for example MobilePay (that some regions in the world uses), 
//without having to change anything in Orderservice.
public interface IPaymentProcessor
{
    //Name of the payment method used is stored on the Order after a successful payemnt.
    string PaymentMethod { get; }
    
    // Trys to process a payment for the given amount in NOK.
    // Return true if the payment succeeds and false if it fails.
    public bool ProcessPayment(decimal amount);
}