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
            Console.WriteLine("=== Вызов UserRepository.findByEmail() ===");

            return new T
            {
                Id = 1,
                Name = "Эксперт",
                Role = "Expert",
                PasswordHash = "password"
            };
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

        public int getReviews(string id)
        {
            Console.WriteLine("=== Вызов UserRepository.getReviews() ===");

            return 28;
        }

        public double getAvgScore(string id)
        {
            Console.WriteLine("=== Вызов UserRepository.getAvgScore() ===");

            return 7.8;
        }

        public int getExpertRanking(string id)
        {
            Console.WriteLine("=== Вызов UserRepository.getExpertRanking() ===");

            return 5;
        }
    }
}
