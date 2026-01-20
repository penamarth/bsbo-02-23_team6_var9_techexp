using GrantSystem.Interfaces;
using GrantSystem.Repositories;
using GrantSysytem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrantSystem.Facade
{
    public class GrantSystemFacade
    {
        private readonly IUserRepository<Expert> _userRepository;
        private readonly IAppRepository _appRepository;
        private readonly IStatsService _statsService;
        private readonly INotifyService _notifyService;
        private Expert _currentExpert;

        public GrantSystemFacade(
            IUserRepository<Expert> userRepository,
            IAppRepository appRepository,
            INotifyService notifyService,
            IStatsService statsService
        )
        {
            _userRepository = userRepository;
            _appRepository = appRepository;
            _statsService = statsService;
            _notifyService = notifyService;
        }

        public Expert Login(string email, string password)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.Login() ===");

            var expert = _userRepository.findByEmail(email);

            _currentExpert = expert;

            return expert;
        }

        public void Logout()
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.Logout() ===");

            _currentExpert = null;
        }

        public GrantApplication CreateApplication(int applicantId, GrantApplication applicationData)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.CreateApplication() ===");

            IUserRepository<Applicant> userApplicantRepository = new UserRepository<Applicant>();
            var user = userApplicantRepository.findById(applicantId); // получаем пользователя-заявителя по id

            var newApplication = new GrantApplication
            {
                Id = 1,
                ApplicantId = user.Id,
                Title = applicationData.Title,
                Description = applicationData.Description,
                Status = "Created",
                SubmissionDate = DateTime.Now
            };

            _appRepository.save(newApplication);

            return newApplication;
        }

        public GrantApplication UpdateGrantApplication(int applicantId, GrantApplication updateApplicationData)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.UpdateGrantApplication() ===");

            var updatedApplication = _appRepository.update(updateApplicationData);

            return updatedApplication;
        }

        public void SubmitApplication(int applicantId)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.SubmitApplication() ===");

            GrantApplication grantApplication = _appRepository.findById(applicantId);

            grantApplication.Status = "UNDER_REVIEW";

            GrantApplication updatedApplication = _appRepository.update(grantApplication);

            _notifyService.sendNotification(grantApplication.ApplicantId, "Заявка успешно принята к рассмотрению");
            _notifyService.sendNotification(new List<int>() { 1, 2, 3}, "Новая заявка для экпертизы");
        }

        public GrantApplication GetGrantApplication(int applicantId)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.GetGrantApplication() ===");

            GrantApplication grantApplication = _appRepository.findById(applicantId);

            // Мокаем данные гранта
            grantApplication.Id = 1;
            grantApplication.ApplicantId = 1;
            grantApplication.Title = "Новая заявка на грант";
            grantApplication.Description = "Grant for scientific research project.";
            grantApplication.Status = "onReview";
            grantApplication.reviews = new List<Review>();

            return grantApplication;
        }

        public void ApproveApplication(int applicationId)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.ApproveApplication() ===");

            GrantApplication grantApplication = _appRepository.findById(applicationId);
            
            grantApplication.grant = new Grant()
            {
                Amount = 100000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                Status = "Approved"
            };

            GrantApplication updatedApplication = _appRepository.update(grantApplication);

            _notifyService.sendNotification(grantApplication.ApplicantId, "Заявка ободрена");
        }

        public GrantApplication SubmitReview(int expertId, int score, string comment, int applicationId)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.SubmitReview() ===");

            GrantApplication grantApplication = _appRepository.findById(applicationId);

            // Убедимся, что список инициализирован
            if (grantApplication.reviews == null)
                grantApplication.reviews = new List<Review>();

            int newId = grantApplication.reviews.Count > 0 
                ? grantApplication.reviews.Max(r => r.Id) + 1 
                : 1;

            // Добавляем новую рецензию
            grantApplication.reviews.Add(new Review
            {
                Id = newId,
                ApplicationId = applicationId,
                ExpertId = expertId,
                Score = score,
                Comment = comment,
                SubmissionDate = DateTime.Now
            });

            GrantApplication updatedApplication = _appRepository.update(grantApplication);

            _notifyService.sendNotification(grantApplication.ApplicantId, "Получена новая экспертная оценка");

            return updatedApplication;
        }

        public List<GrantApplication> GetApplicationsForExpert(int expertId)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.GetApplicationsForExpert() ===");

            return _appRepository.findByStatus("UNDER_REVIEW");
        }
                
        public GrantApplication GetApplicationReviews(int applicationId)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.GetApplicationReviews() ===");
            return _appRepository.findById(applicationId); // reviews уже внутри
        }

        public ApplicationStats getApplicationStats()
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.getApplicationStats() ===");

            return _statsService.getApplicationStats();
        }

        public ExpertStats getExpertStats(string expertId)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.getExpertStats() ===");

            return _statsService.getExpertStats(expertId);
        }

        public GrantStats getGrantStats(string investorId)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.getGrantStats() ===");
            
            return _statsService.getGrantStats(investorId);
        }
    }
}