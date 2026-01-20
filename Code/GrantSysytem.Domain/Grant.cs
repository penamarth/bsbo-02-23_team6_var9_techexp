namespace GrantSysytem.Domain
{
    public class Grant
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public int ApplicationId { get; set; }
        public string InvestorId { get; set; }
        public string RecipientAccount { get; set; } = "DEFAULT_ACCOUNT";

        public Grant()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddYears(1);
        }
    }
}