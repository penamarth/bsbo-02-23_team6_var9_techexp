namespace GrantSystem.Interfaces
{
    public interface INotifyService
    {
        void sendNotification(int userId, string message);
    }
}
