namespace GrantSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentGatewayAPI _paymentGatewayAPI;
        private readonly IAppRepository _appRepository;
        private readonly INotifyService _notifyService;

        public PaymentService(IPaymentGatewayAPI paymentGatewayAPI, IAppRepository appRepository, INotifyService notifyService)
        {
            _paymentGatewayAPI = paymentGatewayAPI;
            _appRepository = appRepository;
            _notifyService = notifyService;
        }

        public bool ProcessPayment(Grant grant)
        {
            try
            {
                var result = _paymentGatewayAPI.ProcessPayment(grant.Amount, grant.RecipientAccount);
                
                if (result.IsSuccessful)
                {
                    grant.Status = "DISBURSED";
                    _appRepository.UpdateGrant(grant);
                    
                    var investor = new Investor { Id = int.Parse(grant.InvestorId) };
                    _notifyService.SendNotification(investor, "Грант выплачен соискателю");
                    
                    return true;
                }
                else
                {
                    grant.Status = "PAYMENT_FAILED";
                    _appRepository.UpdateGrant(grant);
                    
                    var investor = new Investor { Id = int.Parse(grant.InvestorId) };
                    _notifyService.SendNotification(investor, "Ошибка при выплате гранта");
                    
                    throw new PaymentFailedException("Ошибка при обработке платежа");
                }
            }
            catch (Exception ex)
            {
                grant.Status = "PAYMENT_FAILED";
                _appRepository.UpdateGrant(grant);
                
                var investor = new Investor { Id = int.Parse(grant.InvestorId) };
                _notifyService.SendNotification(investor, "Ошибка при выплате гранта");
                
                throw new PaymentFailedException("Ошибка при обработке платежа", ex);
            }
        }
    }
}