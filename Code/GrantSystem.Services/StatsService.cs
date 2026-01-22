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
            Console.WriteLine("=== Вызов StatsService.getExpertStats() ===");

            int reviews = _userRepo.getReviews(expertId);
            double avgScore = _userRepo.getAvgScore(expertId);
            int ranking = _userRepo.getExpertRanking(expertId);

            return new ExpertStats(expertId, reviews, avgScore, ranking);
        }

        public GrantStats getGrantStats(string investorId)
        {
            Console.WriteLine("=== Вызов StatsService.getGrantStats() ===");

            int grants = _appRepo.getInvestorGrants(investorId);
            double total = _appRepo.getInvestorTotal(investorId);
            double avg = _appRepo.avgAmountByInvestor(investorId);
            int applicants = _appRepo.getUniqueApplicants(investorId);

            return new GrantStats(investorId, grants, total, avg, applicants);
        }
    }
}
