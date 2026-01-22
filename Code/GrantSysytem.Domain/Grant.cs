using System;

namespace GrantSysytem.Domain
{
    public class Grant
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
