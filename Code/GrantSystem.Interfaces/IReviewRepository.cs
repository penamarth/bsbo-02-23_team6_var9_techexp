using GrantSysytem.Domain;
using System.Collections.Generic;

namespace GrantSystem.Interfaces
{
    public interface IReviewRepository
    {
        Review findById(int id);
        List<Review> findByApplicant(int id);
        // НОВЫЙ МЕТОД для диаграммы последовательности
        List<Review> findByApplication(int applicationId);
        Review save(Review review);
    }
}
