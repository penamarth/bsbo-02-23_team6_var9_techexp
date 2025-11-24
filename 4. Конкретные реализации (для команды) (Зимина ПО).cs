// Реализации репозиториев
public class GrantApplicationRepository : IGrantApplicationRepository
{
    private readonly IDatabase _database;
    
    public GrantApplication FindById(uint applicationId)
    {
        // реализация поиска в БД
        return new GrantApplication();
    }
    
    public GrantApplication Create(GrantApplication application)
    {
        // реализация создания
        return application;
    }
    
    public void AddDocuments(uint applicationId, File[] documents)
    {
        // реализация добавления документов
    }
}

public class ApplicantRepository : IApplicantRepository
{
    public Applicant FindById(uint applicantId)
    {
        // реализация поиска applicant
        return new Applicant();
    }
}
