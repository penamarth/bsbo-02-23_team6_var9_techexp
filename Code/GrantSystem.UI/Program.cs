using GrantSystem.Facade;
using GrantSystem.Interfaces;
using GrantSystem.Repositories;
using GrantSystem.Services;
using GrantSystem.Domain;
using System;

namespace GrantSystem.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IAppRepository appRepository = new AppRepository();
            IUserRepository<Expert> expertRepository = new UserRepository<Expert>();
            IUserRepository<Applicant> applicantRepository = new UserRepository<Applicant>();
            IStatsService statsService = new StatsService(appRepository, expertRepository);
            INotifyService notifyService = new NotifyService();
            IPaymentService paymentService = new PaymentService(new MockPaymentGatewayAPI(), appRepository, notifyService);

            var facade = new GrantSystemFacade(
                expertRepository,
                applicantRepository,
                appRepository,
                notifyService,
                paymentService,
                statsService
            );

            var expert = new Expert()
            {
                Id = 1,
                Name = "Николай",
                Email = "expert@example.com",
                Role = "Эксперт"
            };

            var applicant = new Applicant()
            {
                Id = 2,
                Name = "Иван",
                Email = "ivan@example.com",
                Role = "Заявитель"
            };

            expertRepository.save(expert);
            applicantRepository.save(applicant);

            var loggedExpert = facade.Login("expert@example.com", "password");

            GrantApplication newApplication = facade.CreateApplication(new GrantApplication
            {
                ApplicantId = applicant.Id,
                Title = "Новая заявка на грант",
                Description = "Grant for scientific research project.",
                Status = "CREATED"
            });

            facade.SubmitApplication(newApplication.Id);

            var applicationsForExpert = facade.GetApplicationsForExpert();

            var application = appRepository.findById(newApplication.Id);
            application.Status = "APPROVED";
            appRepository.update(application);

            var review = facade.SubmitReview(newApplication.Id, 10, "Хорошая идея для гранта");

            try
            {
                var grant = facade.ApproveApplication(newApplication.Id, 50000);
            }
            catch (PaymentFailedException ex)
            {
                Console.WriteLine($"Ошибка платежа: {ex.Message}");
            }

            var applicationStats = facade.GetApplicationsStats();
            var expertStats = facade.GetExpertStats("1");
            var grantStats = facade.GetGrantStats("SYSTEM");

            facade.Logout();
        }
    }

    public class MockPaymentGatewayAPI : IPaymentGatewayAPI
    {
        public PaymentResult ProcessPayment(decimal amount, string recipientAccount)
        {
            var random = new Random();
            bool success = random.Next(0, 2) == 1;
            
            return new PaymentResult
            {
                IsSuccessful = success,
                TransactionId = success ? Guid.NewGuid().ToString() : null,
                ErrorMessage = success ? null : "Ошибка платежного шлюза"
            };
        }
    }
}