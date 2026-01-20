namespace GrantSystem.Interfaces
{
    public interface IPaymentGatewayAPI
    {
        PaymentResult ProcessPayment(decimal amount, string recipientAccount);
    }

    public class PaymentResult
    {
        public bool IsSuccessful { get; set; }
        public string TransactionId { get; set; }
        public string ErrorMessage { get; set; }
    }
}