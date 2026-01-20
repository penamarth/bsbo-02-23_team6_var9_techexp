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
            return _appRepo.GetApplicationStats();
        }

        public ExpertStats getExpertStats(string expertId)
        {
            int reviews = _userRepo.getReviews(expertId);
            double avgScore = _userRepo.getAvgScore(expertId);
            int ranking = _userRepo.getExpertRanking(expertId);

            return new ExpertStats(expertId, reviews, avgScore, ranking);
        }

        public GrantStats getGrantStats(string investorId)
        {
            return _appRepo.GetGrantStats(investorId);
        }
    }
}