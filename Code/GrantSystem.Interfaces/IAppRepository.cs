namespace GrantSystem.Interfaces
{
    public interface IAppRepository
    {
        GrantApplication findById(int id);
        List<GrantApplication> findByApplicant(int id);
        List<GrantApplication> findByStatus(string status);
        GrantApplication save(GrantApplication app);
        GrantApplication update(GrantApplication app);
        void delete(BaseUser user);
        long countAll();
        long countByStatus(string status);
        double sumAmount();
        double calculateAverageScore();
        int getInvestorGrants(string id);
        double getInvestorTotal(string id);
        double avgAmountByInvestor(string id);
        int getUniqueApplicants(string id);
        
        void AddReview(Review review);
        List<Review> GetReviewsForApplication(int appId);
        List<Review> GetReviewsByExpertId(int expertId);
        
        void SaveGrant(Grant grant);
        Grant GetGrantById(int grantId);
        List<Grant> GetGrantsByInvestor(string investorId);
        void UpdateGrant(Grant grant);
        
        ApplicationStats GetApplicationStats();
        GrantStats GetGrantStats(string investorId);
        List<GrantApplication> GetApplicationsForExpert();
        List<GrantApplication> GetAllApplications();
    }
}