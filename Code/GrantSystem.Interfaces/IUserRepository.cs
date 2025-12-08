using GrantSysytem.Domain;

namespace GrantSystem.Interfaces
{
    public interface IUserRepository<TUser>
    {
        TUser findById(int id);
        TUser findByEmail(string email);
        TUser save(BaseUser user);
        void delete(BaseUser user);
    }
}
