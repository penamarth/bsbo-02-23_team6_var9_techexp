using GrantSysytem.Domain;
using System.Collections.Generic;

namespace GrantSystem.Interfaces
{
    public interface IAppRepository
    {
        GrantApplication findById(int id);
        List<GrantApplication> findByApplicant(int id);
         // НОВЫЙ МЕТОД для диаграммы последовательности
        List<GrantApplication> findByStatus(string status);
        
        GrantApplication save(GrantApplication app);
        GrantApplication update(GrantApplication app);
        void delete(BaseUser user);
    }
}
