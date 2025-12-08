using GrantSysytem.Domain;
using System.Collections.Generic;

namespace GrantSystem.Interfaces
{
    public interface IReviewRepository
    {
        Review findById(int id);
        List<Review> findByApplicant(int id);
        Review save(Review review);
    }
}
