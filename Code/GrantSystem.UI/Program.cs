using GrantSystem.Facade;
using GrantSystem.Interfaces;
using GrantSystem.Repositories;
using GrantSystem.Services;
using GrantSysytem.Domain;

namespace GrantSystem.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IAppRepository appRepository = new AppRepository();
            IUserRepository<Expert> expertRepository = new UserRepository<Expert>();
            IStatsService statsService = new StatsService(appRepository, expertRepository);
            INotifyService notifyService = new NotifyService();

            var facade = new GrantSystemFacade(
                appRepository,
                notifyService,
                statsService
            );

            Console.WriteLine("======== УПРАВЛЕНЕИ ЗАЯВКОЙ ========");

            Console.WriteLine("\n======== Создание заявки (CreateApplication) ========");

            GrantApplication newApplication = facade.CreateApplication(1, new GrantApplication
            {
                Title = "Новая заявка на грант",
                Description = "Grant for scientific research project.",
                grant = new Grant()
            });
            Console.WriteLine($"Создана заявка на грант: Id={newApplication.Id}, Title={newApplication.Title}, Status={newApplication.Status}");

            Console.WriteLine("\n======== Верификация заявки (UpdateGrantApplication) ========");

            newApplication.Status = "ReadyForReview";
            GrantApplication updatedApplication = facade.UpdateGrantApplication(1, newApplication);
            Console.WriteLine($"Обновлена заявка на грант: Id={updatedApplication.Id}, Title={updatedApplication.Title}, Status={updatedApplication.Status}");

            Console.WriteLine("\n======== Отправка на экспертизу (SubmitApplication) ========");

            facade.SubmitApplication(newApplication.ApplicantId);

            Console.WriteLine($"Заявка Id={updatedApplication.Id} отправлена на экспертизу");

            Console.WriteLine("\n======== Экспертиза и просмотр статуса (GetGrantApplication) ========");

            GrantApplication applicationData = facade.GetGrantApplication(newApplication.Id);
            Console.WriteLine($"Просмотр экспертизы  " +
                $"Id={applicationData.Id}, " +
                $"Title={applicationData.Title}, " +
                $"Status={applicationData.Status}, " +
                $"Review={applicationData.reviews.Count()}");

            Console.WriteLine("\n======== Одобрение гранта (ApproveApplication) ========");

            facade.ApproveApplication(applicationData.Id);

            Console.WriteLine("\n======== Запрос общей статистики ========");
            ApplicationStats applicationStats = facade.getApplicationStats();
            Console.WriteLine($"Общая статистика: Total={applicationStats.TotalApplications}, Approved={applicationStats.ApprovedApplications}, Amount={applicationStats.TotalFundingAmount}, AvgScore={applicationStats.AverageScore}");

            Console.WriteLine("\n======== Запрос персональной статистики ========");
            ExpertStats expertStats = facade.getExpertStats("123");
            Console.WriteLine($"Статистика эксперта: Id={expertStats.ExpertId}, Reviews={expertStats.ReviewsCompleted}, AvgScore={expertStats.AverageReviewScore}, Ranking={expertStats.Ranking}");

            Console.WriteLine("\n======== Запрос финансовой статистики ========");
            GrantStats grantStats = facade.getGrantStats("123");
            Console.WriteLine($"Финансовая статистика: Id={grantStats.InvestorId}, Grants={grantStats.GrantsCount}, Total={grantStats.TotalAmount}, AvgAmount={grantStats.AverageAmount}, UniqueApplicants={grantStats.UniqueApplicants}");
        }
    }
}
