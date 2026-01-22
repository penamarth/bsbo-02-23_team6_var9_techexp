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
        private readonly IUserRepository<Applicant> _applicantRepository;
        private readonly IAppRepository _appRepository;
        private readonly IStatsService _statsService;
        private readonly INotifyService _notifyService;
        private Expert _currentExpert;

        public GrantSystemFacade(
            IUserRepository<Expert> userRepository,
            IUserRepository<Applicant> applicantRepository,
            IAppRepository appRepository,
            INotifyService notifyService,
            IStatsService statsService
        )
        {
            _userRepository = userRepository;
            _applicantRepository = applicantRepository;
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
            var user = userApplicantRepository.findById(applicantId);

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

            grantApplication.Id = 1;
            grantApplication.ApplicantId = 1;
            grantApplication.Title = "Новая заявка на грант";
            grantApplication.Description = "Grant for scientific research project.";
            grantApplication.Status = "onReview";
            grantApplication.reviews = new List<Review>();

            return grantApplication;
        }

        public Grant ApproveApplication(int appId, decimal amount)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.ApproveApplication() ===");

            var application = _appRepository.findById(appId);
            
            if (application.Status != "APPROVED" || amount <= 0)
                throw new InvalidOperationException("Заявка не одобрена или неверная сумма");
            
            var grant = new Grant()
            {
                Id = new Random().Next(1000, 9999),
                ApplicationId = appId,
                Amount = (float)amount,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
                Status = "APPROVED",
                InvestorId = "SYSTEM",
                RecipientAccount = "DEFAULT_ACCOUNT"
            };
            
            application.Status = "GRANT_ISSUED";
            _appRepository.update(application);
            
            var applicant = _applicantRepository.findById(application.ApplicantId);
            _notifyService.sendNotification(applicant.Id, "Ваш грант одобрен");
            
            Console.WriteLine($"Грант создан: ID={grant.Id}, Сумма={grant.Amount}");
            
            return grant;
        }

        public GrantApplication SubmitReview(int expertId, int score, string comment, int applicationId)
        {
            Console.WriteLine("=== Вызов GrantSystemFacade.SubmitReview() ===");

            GrantApplication grantApplication = _appRepository.findById(applicationId);

            if (grantApplication.reviews == null)
                grantApplication.reviews = new List<Review>();

            int newId = grantApplication.reviews.Count > 0 
                ? grantApplication.reviews.Max(r => r.Id) + 1 
                : 1;

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
            return _appRepository.findById(applicationId);
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

        public GrantApplication FinalizeReview(int applicationId)
        {
            var application = _appRepository.findById(applicationId);

            var expertHandler = new ExpertReviewHandler();
            var investorHandler = new InvestorApprovalHandler();

            expertHandler.SetNext(investorHandler);

            var result = expertHandler.Handle(application);

            _appRepository.update(result);

            _notifyService.sendNotification(
                result.ApplicantId,
                $"Решение по заявке: {result.Status}"
            );

            return result;
        }
    }
}