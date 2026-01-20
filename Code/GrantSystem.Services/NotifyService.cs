using System;
using System.Collections.Generic;
using GrantSystem.Interfaces;

namespace GrantSystem.Services
{
    public class NotifyService : INotifyService
    {

        public void sendNotification(int userId, string message)
        {
            Console.WriteLine("=== Вызов адаптера NotifyService.SendNotification() ===");

            ExternalEmailServiceAPI.sendEmail(userId, message);
        }

        public void sendNotification(List<int> userIds, string message)
        {
            Console.WriteLine("=== Вызов адаптера NotifyService.SendNotification() для ExpertGroups ===");

            userIds.ForEach(id => ExternalEmailServiceAPI.sendEmail(id, message));
        }
    }

    static class ExternalEmailServiceAPI
    {
        public static bool sendEmail(int userId, string body)
        {
            Console.WriteLine("Отправка уведомления на E-mail");
            Console.WriteLine($"Отправлено уведомление пользователю с id={userId}. Уведомление: " + body);

            return true;
        }
    }
}
