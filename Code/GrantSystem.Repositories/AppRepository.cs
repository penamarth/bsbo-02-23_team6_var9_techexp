using GrantSystem.Interfaces;
using GrantSysytem.Domain;
using System;
using System.Collections.Generic;

namespace GrantSystem.Repositories
{
    public class AppRepository : IAppRepository
    {
        public void delete(BaseUser user)
        {
            throw new NotImplementedException();
        }

        public List<GrantApplication> findByApplicant(int id)
        {
            throw new NotImplementedException();
        }

        public GrantApplication findById(int id)
        {
            throw new NotImplementedException();
        }

        public GrantApplication save(GrantApplication app)
        {
            throw new NotImplementedException();
        }

        public GrantApplication update(GrantApplication app)
        {
            throw new NotImplementedException();
        }
    }
}
