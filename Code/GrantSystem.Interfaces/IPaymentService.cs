namespace GrantSystem.Interfaces
{
    public interface IPaymentService
    {
        bool ProcessPayment(Grant grant);
    }
}