namespace GrantSystem.Interfaces
{
    public interface INotifyService
    {
        void sendNotification(int userId, string message);
        void sendNotification(List<int> userIds, string message);
        void SendNotification(User user, string message);
    }
}