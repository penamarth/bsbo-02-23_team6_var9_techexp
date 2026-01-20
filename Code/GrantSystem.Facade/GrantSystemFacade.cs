using GrantSystem.Interfaces;
using GrantSysytem.Domain;
using System;
using System.Collections.Generic;

namespace GrantSystem.Facade
{
    public class GrantSystemFacade
    {
        private readonly IUserRepository<Expert> _expertRepository;
        private readonly IUserRepository<Applicant> _applicantRepository;
        private readonly IAppRepository _appRepository;
        private readonly INotifyService _notifyService;
        private readonly IPaymentService _paymentService;
        private readonly IStatsService _statsService;
        private User _currentUser;

        public GrantSystemFacade(
            IUserRepository<Expert> expertRepository,
            IUserRepository<Applicant> applicantRepository,
            IAppRepository appRepository,
            INotifyService notifyService,
            IPaymentService paymentService,
            IStatsService statsService)
        {
            _expertRepository = expertRepository;
            _applicantRepository = applicantRepository;
            _appRepository = appRepository;
            _notifyService = notifyService;
            _paymentService = paymentService;
            _statsService = statsService;
        }

        public User Login(string email, string password)
        {
            var user = _expertRepository.findByEmail(email) as User 
                ?? _applicantRepository.findByEmail(email) as User;
            _currentUser = user;
            return user;
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public GrantApplication CreateApplication(GrantApplication applicationData)
        {
            var newApplication = new GrantApplication
            {
                Id = GenerateNewId(),
                ApplicantId = applicationData.ApplicantId,
                Title = applicationData.Title,
                Description = applicationData.Description,
                Status = "CREATED",
                SubmissionDate = DateTime.Now
            };
            
            _appRepository.save(newApplication);
            return newApplication;
        }

        public void SubmitApplication(int appId)
        {
            var application = _appRepository.findById(appId);
            application.Status = "UNDER_REVIEW";
            _appRepository.update(application);
            
            var experts = _expertRepository.GetAll();
            foreach (var expert in experts)
            {
                _notifyService.SendNotification(expert, "Новая заявка для экспертизы");
            }
        }

        public List<GrantApplication> GetApplicationsForExpert()
        {
            return _appRepository.GetApplicationsForExpert();
        }

        public Review SubmitReview(int appId, int score, string comment)
        {
            var application = _appRepository.findById(appId);
            var expert = _currentUser as Expert;
            
            var review = new Review
            {
                Id = GenerateNewId(),
                ApplicationId = appId,
                ExpertId = expert.Id,
                Score = score,
                Comment = comment,
                SubmissionDate = DateTime.Now
            };
            
            _appRepository.AddReview(review);
            application.Status = "REVIEWED";
            _appRepository.update(application);
            
            _notifyService.SendNotification(
                _applicantRepository.findById(application.ApplicantId), 
                "Получена новая экспертная оценка"
            );
            
            return review;
        }

        public List<Review> GetApplicationReviews(int appId)
        {
            return _appRepository.GetReviewsForApplication(appId);
        }

        public Grant ApproveApplication(int appId, decimal amount)
        {
            var application = _appRepository.findById(appId);
            
            if (application.Status != "APPROVED" || amount <= 0)
            {
                throw new InvalidOperationException(
                    $"Заявка не одобрена (статус: {application.Status}) или неверная сумма: {amount}"
                );
            }
            
            var grant = new Grant
            {
                Id = GenerateNewId(),
                ApplicationId = appId,
                Amount = amount,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
                Status = "APPROVED",
                InvestorId = _currentUser?.Id.ToString() ?? "SYSTEM"
            };
            
            _appRepository.SaveGrant(grant);
            
            application.Status = "GRANT_ISSUED";
            _appRepository.update(application);
            
            var applicant = _applicantRepository.findById(application.ApplicantId);
            _notifyService.SendNotification(applicant, "Ваш грант одобрен");
            
            try
            {
                bool paymentResult = _paymentService.ProcessPayment(grant);
                
                if (paymentResult)
                {
                    grant.Status = "DISBURSED";
                    _appRepository.SaveGrant(grant);
                    return grant;
                }
            }
            catch (PaymentFailedException ex)
            {
                throw;
            }
            
            return grant;
        }

        public void RejectApplication(int appId, string reason)
        {
            var application = _appRepository.findById(appId);
            application.Status = "REJECTED";
            _appRepository.update(application);
            
            var applicant = _applicantRepository.findById(application.ApplicantId);
            _notifyService.SendNotification(applicant, $"Заявка отклонена. Причина: {reason}");
        }

        public ApplicationStats GetApplicationsStats()
        {
            return _appRepository.GetApplicationStats();
        }

        public GrantStats GetGrantStats(string investorId)
        {
            return _appRepository.GetGrantStats(investorId);
        }

        public ExpertStats GetExpertStats(string expertId)
        {
            return _statsService.getExpertStats(expertId);
        }

        private int GenerateNewId()
        {
            return new Random().Next(1000, 9999);
        }
    }
}