namespace GrantSysytem.Domain
{
    public class GrantStats
    {
        public string InvestorId { get; set; }
        public int GrantsCount { get; set; }
        public double TotalAmount { get; set; }
        public double AverageAmount { get; set; }
        public int UniqueApplicants {  get; set; }

        public GrantStats(string investorId, int grants, double total, double avg, int applicants)
        {
            InvestorId = investorId;
            GrantsCount = grants;
            TotalAmount = total;
            AverageAmount = avg;
            UniqueApplicants = applicants;
        }
    }
}
