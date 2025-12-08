using GrantSystem.Interfaces;
using GrantSysytem.Domain;
using System.Collections.Generic;

namespace GrantSystem.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public List<Review> findByApplicant(int id)
        {
            throw new System.NotImplementedException();
        }
        public List<Review> findByApplication(int applicationId)
        {
            return new List<Review>();
        }
        public Review findById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Review save(Review review)
        {
            throw new System.NotImplementedException();
        }
    }
}
