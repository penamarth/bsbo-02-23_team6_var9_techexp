namespace GrantSysytem.Domain
{
    public class ApplicationStats
    {
        public long TotalApplications { get; set; }
        public long ApprovedApplications { get; set; }
        public double TotalFundingAmount { get; set; }
        public double AverageScore { get; set; }

        public ApplicationStats(long total, long approved, double totalAmount, double avgScore)
        {
            TotalApplications = total;
            ApprovedApplications = approved;
            TotalFundingAmount = totalAmount;
            AverageScore = avgScore;
        }
    }
}
