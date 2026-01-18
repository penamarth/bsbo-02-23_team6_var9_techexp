using GrantSystem.Interfaces;
using GrantSysytem.Domain;

namespace GrantSystem.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserRepository<BaseUser> userRepository = new UserRepositry();

        }
    }
}