using GrantSystem.Interfaces;
using GrantSysytem.Domain;
using GrantSystem.Repositories;

namespace GrantSystem.Facade
{
    public class GrantSystemFacade
    {
        private readonly IAppRepository _appRepository;

        public GrantSystemFacade(
            IAppRepository appRepository
        )
        {
            _appRepository = appRepository;
        }

        public int CreateApplication(int applicantId, GrantApplication applicationData) 
        { 
            Console.WriteLine("=== Вызов GrantSystemFacade.CreateApplication() ===");

            IUserRepository<Applicant> userApplicantRepository = new UserRepository<Applicant>();
            var user = userApplicantRepository.findById(applicantId); // получаем пользователя-заявителя по id

            var newApplication = new GrantApplication
            {
                Id = 1,
                ApplicantId = user.Id,
                Title = applicationData.Title,
                Description = applicationData.Description,
                Status = "Created",
                SubmissionDate = DateTime.Now
            };

            _appRepository.save(newApplication);

            return newApplication.Id;
        }
    }
}
