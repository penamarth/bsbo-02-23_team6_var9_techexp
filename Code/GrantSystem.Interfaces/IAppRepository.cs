using GrantSysytem.Domain;
using System.Collections.Generic;

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
    }
}
