namespace GrantSystem.Interfaces
{
    public interface IUserRepository<TUser> where TUser : BaseUser
    {
        TUser findById(int id);
        TUser findByEmail(string email);
        TUser save(BaseUser user);
        void delete(BaseUser user);
        int getReviews(string id);
        double getAvgScore(string id);
        int getExpertRanking(string id);
        List<TUser> GetAll();
    }
}