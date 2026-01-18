using GrantSystem.Interfaces;
using GrantSysytem.Domain;
using System;

namespace GrantSystem.Repositories
{
    public class UserRepository<T> : IUserRepository<T> where T : BaseUser, new()
    {
        public void delete(BaseUser user)
        {
            throw new NotImplementedException();
        }

        public T findByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public T findById(int id)
        {
            Console.WriteLine("=== Вызов UserRepository.findById() ===");

            return new T
            {
                Id = id,
                Name = "Иван Иванов",
                Role = "Applicant",
                PasswordHash = "hashed_password"
            };
        }

        public T save(BaseUser user)
        {
            throw new NotImplementedException();
        }
    }
}
