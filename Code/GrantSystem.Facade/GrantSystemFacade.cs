using GrantSystem.Interfaces;
using GrantSystem.Repositories;
using GrantSysytem.Domain;
using System;
using System.Collections.Generic;

namespace GrantSystem.Facade
{
    public class GrantSystemFacade
    {
        private readonly IAppRepository _appRepository;
        private readonly IStatsService _statsService;
        private readonly INotifyService _notifyService;

        public GrantSystemFacade(
            IAppRepository appRepository,
            INotifyService notifyService,
            IStatsService statsService
        )
        {
            _appRepository = appRepository;
            _statsService = statsService;
            _notifyService = notifyService;
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

            grantApplication.Status = "onReview";

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

            grantApplication.reviews = new List<Review>()
            {
                new Review()
                {
                    Id  = 1,
                    SubmissionDate = DateTime.Now,
                    Comment = comment,
                    Score = score,
                    ExpertId = expertId
                }
            };

            GrantApplication updatedApplication = _appRepository.update(grantApplication);

            return grantApplication;
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