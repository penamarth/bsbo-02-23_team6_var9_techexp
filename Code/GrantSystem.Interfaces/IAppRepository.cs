using GrantSysytem.Domain;
using System.Collections.Generic;

namespace GrantSystem.Interfaces
{
    public interface IAppRepository
    {
        GrantApplication findById(int id);
        List<GrantApplication> findByApplicant(int id);
        GrantApplication save(GrantApplication app);
        GrantApplication update(GrantApplication app);
        void delete(BaseUser user);
    }
}
