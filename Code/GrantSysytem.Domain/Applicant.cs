namespace GrantSysytem.Domain
{
    public class Applicant : BaseUser
    {
        public string Organization { get; set; }
        public string INN { get; set; }
    }
}
