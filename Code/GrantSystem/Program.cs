using GrantSystem.Repositories;
using GrantSystem.Services;

namespace GrantSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            var userRepo = new UserRepository();
            var appRepo = new AppRepository();
            var reviewRepo = new ReviewRepository();

            var notifyService = new NotifyService();
            
            var facade = new GrantSystemFacade(
                userRepo,   // IUserRepository
                appRepo,    // IAppRepository  
                reviewRepo, // IReviewRepository
                notifyService // INotifyService
            );
        }
    }
}