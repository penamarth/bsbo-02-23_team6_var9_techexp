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
            IPaymentService paymentService = new PaymentService(appRepository, notifyService);

            var facade = new GrantSystemFacade(
                expertRepository,
                appRepository,
                notifyService,
                statsService,
                paymentService
            );

            var expert = new Expert()
            {
                Id = 1,
                Name = "Николай",
                Role = "Эксперт"
            };

            var applicant = new Applicant()
            {
                Id = 2,
                Name = "Иван",
                Role = "Заявитель"
            };

            // ======== УПРАВЛЕНИЕ ЗАЯВКОЙ (заявитель) ========
            Console.WriteLine("======== УПРАВЛЕНИЕ ЗАЯВКОЙ ========");

            Console.WriteLine("\n======== Создание заявки (OpenCreateApplicationForm) ========");
            GrantApplication newApplication = facade.CreateApplication(applicant.Id, new GrantApplication
            {
                Title = "Новая заявка на грант",
                Description = "Grant for scientific research project.",
                grant = new Grant()
            });
            Console.WriteLine($"Создана заявка на грант: Id={newApplication.Id}, Title={newApplication.Title}, Status={newApplication.Status}");

            Console.WriteLine("\n======== Верификация заявки (saveApplication) ========");
            newApplication.Status = "ReadyForReview";
            GrantApplication updatedApplication = facade.UpdateGrantApplication(1, newApplication);
            Console.WriteLine($"Обновлена заявка на грант: Id={updatedApplication.Id}, Title={updatedApplication.Title}, Status={updatedApplication.Status}");

            Console.WriteLine("\n======== Отправка на экспертизу (SubmitApplication) ========");
            facade.SubmitApplication(newApplication.ApplicantId);
            Console.WriteLine($"Заявка Id={updatedApplication.Id} отправлена на экспертизу");

            Console.WriteLine("\n======== Доработка заявки (editApplication) ========");
            newApplication.Status = "Edit";
            newApplication.Title = "Обновленный заголовок заявки";
            GrantApplication editApplication = facade.UpdateGrantApplication(1, newApplication);
            Console.WriteLine($"Доработка заявки на грант: Id={editApplication.Id}, Title={editApplication.Title}, Status={editApplication.Status}");
            facade.SubmitApplication(newApplication.ApplicantId);

            // ======== УПРАВЛЕНИЕ ЭКСПЕРТИЗОЙ (эксперт) ========
            Console.WriteLine("\n======== УПРАВЛЕНИЕ ЭКСПЕРТИЗОЙ ========");

            Console.WriteLine("\n======== 1. Авторизация эксперта (Login) ========");
            var loggedExpert = facade.Login("expert@example.com", "password");
            Console.WriteLine($"Эксперт авторизован: Id={loggedExpert.Id}, Name={loggedExpert.Name}, Role={loggedExpert.Role}");

            Console.WriteLine("\n======== 2. Получение заявок для экспертизы (GetApplicationsForExpert) ========");
            var applicationsForExpert = facade.GetApplicationsForExpert(loggedExpert.Id);
            Console.WriteLine($"Найдено заявок для экспертизы: {applicationsForExpert.Count}");
            foreach (var app in applicationsForExpert)
            {
                Console.WriteLine($"  - Заявка Id={app.Id}, Title=\"{app.Title}\", Status={app.Status}");
            }

            GrantApplication applicationData = null;
            if (applicationsForExpert.Count > 0)
            {
                var appId = applicationsForExpert[0].Id;
                Console.WriteLine($"\n======== 3. Просмотр заявки и истории оценок (GetApplicationReviews) ========");
                applicationData = facade.GetApplicationReviews(appId);
                Console.WriteLine($"Просмотр заявки Id={applicationData.Id}: {applicationData.Title}");
                Console.WriteLine($"История оценок: {applicationData.reviews.Count} шт.");
                foreach (var review in applicationData.reviews)
                {
                    Console.WriteLine($"  - Оценка: {review.Score}, Комментарий: \"{review.Comment}\"");
                }
            }

            if (applicationData != null)
            {
                Console.WriteLine("\n======== 4. Отправка новой оценки (SubmitReview) ========");
                var applicationWithReview = facade.SubmitReview(
                    loggedExpert.Id,
                    10,
                    "Хорошая идея для гранта!",
                    applicationData.Id
                );
                Console.WriteLine("Экспертиза успешно отправлена.");
            }

            if (applicationData != null)
            {
                Console.WriteLine("\n======== Завершение экспертизы (FinalizeReview) ========");
                var finalizedApplication = facade.FinalizeReview(applicationData.Id);
                Console.WriteLine($"Финальный статус заявки: {finalizedApplication.Status}");
            }

            // ======== ВЫДАЧА ГРАНТА (инвестор) ========
            Console.WriteLine("\n======== ВЫДАЧА ГРАНТА ========");

            if (applicationData != null)
            {
                Console.WriteLine("\n======== Одобрение гранта (ApproveApplication) ========");
                facade.ApproveApplication(applicationData.Id);
                Console.WriteLine("Грант одобрен.");
            }

            // ======== СТАТИСТИКА ========
            Console.WriteLine("\n======== СТАТИСТИКА ГРАНТА ========");

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