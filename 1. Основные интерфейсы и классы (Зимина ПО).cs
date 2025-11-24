// Core Entities
public class GrantApplication
{
    public uint Id { get; set; }
    public uint ApplicantId { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class Applicant
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

// Repository Interfaces
public interface IGrantApplicationRepository
{
    GrantApplication FindById(uint applicationId);
    GrantApplication Create(GrantApplication application);
    void AddDocuments(uint applicationId, File[] documents);
}

public interface IApplicantRepository
{
    Applicant FindById(uint applicantId);
}

// Service Interfaces
public interface INotificationService
{
    void Notify(uint userId, string message);
}

public interface IAuthorizationService
{
    bool ValidateAccess(uint userId);
}

public interface IExpertiseAdapter
{
    ExpertiseResult SubmitForExpertise(uint applicationId);
}

// External Systems
public interface IExpertiseServiceAPI
{
    ExpertiseResult SubmitApplication(uint applicationId);
}

public interface IDatabase
{
    void ExecuteQuery(string query);
    object GetData(string query);
}
