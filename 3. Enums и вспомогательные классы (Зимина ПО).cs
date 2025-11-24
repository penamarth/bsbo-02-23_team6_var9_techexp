public enum ApplicationStatus
{
    Draft,
    Submitted,
    UnderExpertise,
    Approved,
    Rejected
}

public class ExpertiseResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public decimal Score { get; set; }
}

public class File
{
    public string Name { get; set; }
    public byte[] Content { get; set; }
    public string ContentType { get; set; }
}
