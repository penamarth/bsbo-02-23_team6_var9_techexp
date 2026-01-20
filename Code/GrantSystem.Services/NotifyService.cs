namespace GrantSystem.Services
{
    public class NotifyService : INotifyService
    {
        public void sendNotification(int userId, string message)
        {
            Console.WriteLine($"Уведомление для пользователя {userId}: {message}");
        }

        public void sendNotification(List<int> userIds, string message)
        {
            foreach (var userId in userIds)
            {
                Console.WriteLine($"Уведомление для пользователя {userId}: {message}");
            }
        }

        public void SendNotification(User user, string message)
        {
            Console.WriteLine($"Уведомление для {user.Name} ({user.Email}): {message}");
        }
    }
}