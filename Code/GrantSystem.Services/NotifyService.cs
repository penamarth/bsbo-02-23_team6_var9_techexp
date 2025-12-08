namespace GrantSystem.Services
{
    public class NotifyService : INotifyService
    {
        public void sendNotification(string message)
        {
            Console.WriteLine("Уведомление: " + message);
        }
    }
}