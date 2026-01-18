using System;
using GrantSystem.Interfaces;

namespace GrantSystem.Services
{
    public class NotifyService : INotifyService
    {
        public void sendNotification(int userId, string message)
        {
            Console.WriteLine("=== Вызов NotifyService.SendNotification() ===");

            Console.WriteLine($"Отправлено уведомление пользователю с id={userId}. Уведомление: " + message);
        }
    }
}
