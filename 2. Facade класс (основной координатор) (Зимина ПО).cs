public class GrantApplicationFacade
{
    private readonly IGrantApplicationRepository _grantApplicationRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly INotificationService _notificationService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IExpertiseAdapter _expertiseAdapter;
    private readonly IDatabase _database;

    public GrantApplicationFacade(
        IGrantApplicationRepository grantApplicationRepository,
        IApplicantRepository applicantRepository,
        INotificationService notificationService,
        IAuthorizationService authorizationService,
        IExpertiseAdapter expertiseAdapter,
        IDatabase database)
    {
        _grantApplicationRepository = grantApplicationRepository;
        _applicantRepository = applicantRepository;
        _notificationService = notificationService;
        _authorizationService = authorizationService;
        _expertiseAdapter = expertiseAdapter;
        _database = database;
    }

    public GrantApplication CreateApplication(uint applicantId)
    {
        if (!_authorizationService.ValidateAccess(applicantId))
            throw new UnauthorizedAccessException();

        var applicant = _applicantRepository.FindById(applicantId);
        var application = new GrantApplication 
        { 
            ApplicantId = applicantId,
            Status = ApplicationStatus.Draft
        };

        return _grantApplicationRepository.Create(application);
    }

    public void AttachDocuments(uint applicationId, File[] documents)
    {
        _grantApplicationRepository.AddDocuments(applicationId, documents);
        
        var result = _expertiseAdapter.SubmitForExpertise(applicationId);
        
        _notificationService.Notify(applicationId, "Заявка принята и направлена на экспертизу");
    }
}
