using GrantSystem.Interfaces;
using GrantSysytem.Domain;
using System;

namespace GrantSystem.Repositories
{
    public class UserRepository : IUserRepository<BaseUser>
    {
        public void delete(BaseUser user)
        {
            throw new NotImplementedException();
        }

        public BaseUser findByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public BaseUser findById(int id)
        {
            throw new NotImplementedException();
        }

        public BaseUser save(BaseUser user)
        {
            throw new NotImplementedException();
        }
    }
}
