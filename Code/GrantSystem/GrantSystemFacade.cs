using GrantSystem.Interfaces;
using GrantSysytem.Domain;
using System.Collections.Generic;

namespace GrantSystem
{
    public class GrantSystemFacade
    {
        // ВСЕ ЗАВИСИМОСТИ из диаграммы классов
        private IUserRepository userRepository;
        private IAppRepository appRepository;
        private IReviewRepository reviewRepository;
        private INotifyService notifyService;
        
        // Конструктор с зависимостями
        public GrantSystemFacade(
            IUserRepository userRepo,
            IAppRepository appRepo,
            IReviewRepository reviewRepo,
            INotifyService notify)
        {
            userRepository = userRepo;
            appRepository = appRepo;
            reviewRepository = reviewRepo;
            notifyService = notify;
        }
        
        // 1. Логин
        public BaseUser login(string email, string password)
        {
            var user = userRepository.findByEmail(email);
            if (user != null)
            {
                user.Login();
            }
            return user;
        }
        
        public void logout()
        {
            // демо
        }
        
        // 2. Получить заявки для эксперта
        public List<GrantApplication> getApplicationsForExpert()
        {
            return appRepository.findByStatus("UnderReview");
        }
        
        // 3. Создать заявку
        public GrantApplication createApplication(GrantApplication app)
        {
            return appRepository.save(app);
        }
        
        public void submitApplication(int appId)
        {
            var app = appRepository.findById(appId);
            if (app != null)
            {
                app.Submit();
            }
        }
        
        public List<GrantApplication> getMyApplications()
        {
            // демо
            return new List<GrantApplication>();
        }
        
        // 4. Экспертиза - ГЛАВНЫЙ МЕТОД
        public Review submitReview(int appId, float score, string comment)
        {
            // Создаем рецензию
            var review = new Review
            {
                ApplicationId = appId,  // связь с заявкой
                Score = score,
                Comment = comment
            };
            
            review.Submit();
            
            // Сохраняем
            var savedReview = reviewRepository.save(review);
            
            // Уведомляем (из диаграммы последовательности)
            notifyService.sendNotification("Экспертиза завершена");
            
            return savedReview;
        }
        
        // 5. Получить рецензии заявки
        public List<Review> getApplicationReviews(int appId)
        {
            return reviewRepository.findByApplication(appId);
        }
        
        // 6. Утвердить/отклонить заявку (демо)
        public void approveApplication(int appId, decimal amount)
        {
            // демо
        }
        
        public void rejectApplication(int appId, string reason)
        {
            // демо
        }
        
        // 7. Статистика (демо)
        public object getApplicationStats()
        {
            return new object();
        }
        
        public object getGrantStats()
        {
            return new object();
        }
    }
}