using System;
using GrantSystem.Interfaces;
using GrantSysytem.Domain;

namespace GrantSystem.Services
{
    public class StatsService : IStatsService
    {
        private IAppRepository _appRepo;
        private IUserRepository<Expert> _userRepo;

        public StatsService(IAppRepository appRepo, IUserRepository<Expert> userRepo)
        {
            _appRepo = appRepo;
            _userRepo = userRepo;
        }
        
        public ApplicationStats getApplicationStats()
        {
            Console.WriteLine("=== Вызов StatsService.getApplicationStats() ===");
            
            long total = _appRepo.countAll();
            long approved = _appRepo.countByStatus("APPROVED");
            double totalAmount = _appRepo.sumAmount();
            double avgScore = _appRepo.calculateAverageScore();

            return new ApplicationStats(total, approved, totalAmount, avgScore);
        }

        public ExpertStats getExpertStats(string expertId)
        {
            throw new NotImplementedException();
        }

        public GrantStats getGrantStats(string investorId)
        {
            throw new NotImplementedException();
        }
    }
}
