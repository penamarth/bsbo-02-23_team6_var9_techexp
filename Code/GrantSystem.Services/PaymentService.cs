using GrantSystem.Interfaces;
using GrantSysytem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GrantSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private IAppRepository _appRepository;
        private INotifyService _notifyService;

        public PaymentService(IAppRepository appRepository, INotifyService notifyService)
        {
            _appRepository = appRepository;
            _notifyService = notifyService;
        }

        public bool processPayment(GrantApplication grant)
        {
            Console.WriteLine("=== Вызов адаптера PaymentService.processPayment() ===");

            ExternalPaymentGatewayAPI.processPayment(1000000, grant.ApplicantId);
            _appRepository.save(grant);
            _notifyService.sendNotification(1, "Грант выплачен соискателю");

            return true;
        }
    }

    static class ExternalPaymentGatewayAPI
    {
        public static Result processPayment(int amount, int recipientId)
        {
            Console.WriteLine("Выплата гранта");

            return new Result(true);
        }
    }
}
