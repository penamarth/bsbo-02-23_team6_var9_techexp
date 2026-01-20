namespace GrantSystem.Repositories
{
    public class UserRepository<T> : IUserRepository<T> where T : BaseUser, new()
    {
        private List<T> _users = new List<T>();

        public T findByEmail(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);
            if (user != null) return user;
            
            return new T
            {
                Id = 1,
                Name = "Эксперт",
                Email = email,
                Role = "Expert",
                PasswordHash = "password"
            };
        }

        public T findById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null) return user;
            
            return new T
            {
                Id = id,
                Name = "Иван Иванов",
                Email = "test@example.com",
                Role = "Applicant",
                PasswordHash = "hashed_password"
            };
        }

        public T save(BaseUser user)
        {
            var existing = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existing != null)
            {
                _users.Remove(existing);
            }
            
            _users.Add(user as T);
            return user as T;
        }

        public void delete(BaseUser user)
        {
            throw new NotImplementedException();
        }

        public int getReviews(string id)
        {
            return 28;
        }

        public double getAvgScore(string id)
        {
            return 7.8;
        }

        public int getExpertRanking(string id)
        {
            return 5;
        }

        public List<T> GetAll()
        {
            return _users;
        }
    }
}