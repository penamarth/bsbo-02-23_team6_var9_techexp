namespace GrantSysytem.Domain
{
    public class PaymentFailedException : Exception
    {
        public PaymentFailedException() : base("Ошибка при обработке платежа") { }
        public PaymentFailedException(string message) : base(message) { }
        public PaymentFailedException(string message, Exception inner) : base(message, inner) { }
    }
}