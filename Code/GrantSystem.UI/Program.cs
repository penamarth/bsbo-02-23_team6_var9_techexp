
using GrantSystem.Facade;
using GrantSystem.Interfaces;
using GrantSystem.Repositories;
using GrantSysytem.Domain;

namespace GrantSystem.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IAppRepository appRepository = new AppRepository();

            var facade = new GrantSystemFacade(
                appRepository
            );

            Console.WriteLine("======== Создание заявки ========");

            GrantApplication newApplication = facade.CreateApplication(1, new GrantApplication
            {
                Title = "Новая заявка на грант",
                Description = "Grant for scientific research project."
            });
            Console.WriteLine($"Создана заявка на грант: Id={newApplication.Id}, Title={newApplication.Title}, Status={newApplication.Status}");

            Console.WriteLine("======== Верификация заявки ========");

            newApplication.Status = "ReadyForReview";
            GrantApplication updatedApplication = facade.UpdateGrantApplication(1, newApplication);
            Console.WriteLine($"Обновлена заявка на грант: Id={updatedApplication.Id}, Title={updatedApplication.Title}, Status={updatedApplication.Status}");
        }
    }
}
