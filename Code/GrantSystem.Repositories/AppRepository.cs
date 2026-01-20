namespace GrantSystem.Repositories
{
    public class AppRepository : IAppRepository
    {
        private List<GrantApplication> _applications = new List<GrantApplication>();
        private List<Review> _reviews = new List<Review>();
        private List<Grant> _grants = new List<Grant>();
        private int _nextAppId = 1;
        private int _nextReviewId = 1;
        private int _nextGrantId = 1;

        public GrantApplication findById(int id)
        {
            var app = _applications.FirstOrDefault(a => a.Id == id);
            if (app != null)
            {
                app.reviews = _reviews.Where(r => r.ApplicationId == id).ToList();
                app.grant = _grants.FirstOrDefault(g => g.ApplicationId == id);
            }
            
            return app ?? new GrantApplication
            {
                Id = id,
                Title = "Исследование",
                Description = "Проект по юмл",
                Status = "UNDER_REVIEW",
                ApplicantId = 501,
                SubmissionDate = DateTime.Now.AddDays(-3),
                reviews = _reviews.Where(r => r.ApplicationId == id).ToList() ?? new List<Review>()
            };
        }

        public List<GrantApplication> findByApplicant(int id)
        {
            throw new NotImplementedException();
        }

        public List<GrantApplication> findByStatus(string status)
        {
            return _applications.Where(a => a.Status == status).ToList();
        }

        public GrantApplication save(GrantApplication app)
        {
            if (app.Id == 0)
            {
                app.Id = _nextAppId++;
                _applications.Add(app);
            }
            
            return app;
        }

        public GrantApplication update(GrantApplication app)
        {
            var existing = _applications.FirstOrDefault(a => a.Id == app.Id);
            if (existing != null)
            {
                _applications.Remove(existing);
                _applications.Add(app);
            }
            
            return app;
        }

        public void delete(BaseUser user)
        {
            throw new NotImplementedException();
        }

        public long countAll()
        {
            return _applications.Count;
        }

        public long countByStatus(string status)
        {
            return _applications.Count(a => a.Status == status);
        }

        public double sumAmount()
        {
            return _grants.Sum(g => (double)g.Amount);
        }

        public double calculateAverageScore()
        {
            if (_reviews.Count == 0) return 0;
            return _reviews.Average(r => r.Score);
        }

        public int getInvestorGrants(string id)
        {
            return _grants.Count(g => g.InvestorId == id);
        }

        public double getInvestorTotal(string id)
        {
            return (double)_grants.Where(g => g.InvestorId == id).Sum(g => g.Amount);
        }

        public double avgAmountByInvestor(string id)
        {
            var investorGrants = _grants.Where(g => g.InvestorId == id).ToList();
            if (investorGrants.Count == 0) return 0;
            
            return (double)investorGrants.Average(g => g.Amount);
        }

        public int getUniqueApplicants(string id)
        {
            var investorGrantIds = _grants.Where(g => g.InvestorId == id)
                                         .Select(g => g.ApplicationId)
                                         .Distinct()
                                         .ToList();
            
            return _applications.Where(a => investorGrantIds.Contains(a.Id))
                               .Select(a => a.ApplicantId)
                               .Distinct()
                               .Count();
        }

        public void AddReview(Review review)
        {
            if (review.Id == 0)
            {
                review.Id = _nextReviewId++;
            }
            
            _reviews.Add(review);
        }

        public List<Review> GetReviewsForApplication(int appId)
        {
            return _reviews.Where(r => r.ApplicationId == appId).ToList();
        }

        public List<Review> GetReviewsByExpertId(int expertId)
        {
            return _reviews.Where(r => r.ExpertId == expertId).ToList();
        }

        public void SaveGrant(Grant grant)
        {
            if (grant.Id == 0)
            {
                grant.Id = _nextGrantId++;
            }
            
            _grants.Add(grant);
        }

        public Grant GetGrantById(int grantId)
        {
            return _grants.FirstOrDefault(g => g.Id == grantId);
        }

        public List<Grant> GetGrantsByInvestor(string investorId)
        {
            return _grants.Where(g => g.InvestorId == investorId).ToList();
        }

        public void UpdateGrant(Grant grant)
        {
            var existing = _grants.FirstOrDefault(g => g.Id == grant.Id);
            if (existing != null)
            {
                _grants.Remove(existing);
                _grants.Add(grant);
            }
        }

        public ApplicationStats GetApplicationStats()
        {
            long total = _applications.Count;
            long approved = _applications.Count(a => a.Status == "APPROVED");
            double totalAmount = sumAmount();
            double avgScore = calculateAverageScore();
            
            return new ApplicationStats(total, approved, totalAmount, avgScore);
        }

        public GrantStats GetGrantStats(string investorId)
        {
            int grants = getInvestorGrants(investorId);
            double total = getInvestorTotal(investorId);
            double avg = avgAmountByInvestor(investorId);
            int applicants = getUniqueApplicants(investorId);
            
            return new GrantStats(investorId, grants, total, avg, applicants);
        }

        public List<GrantApplication> GetApplicationsForExpert()
        {
            return _applications.Where(a => a.Status == "UNDER_REVIEW" || a.Status == "CREATED").ToList();
        }

        public List<GrantApplication> GetAllApplications()
        {
            return _applications;
        }
    }
}