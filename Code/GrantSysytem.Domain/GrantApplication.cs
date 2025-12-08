using System;

namespace GrantSysytem.Domain
{
    public class GrantApplication
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }

        public void Submit()
        {
            Console.Write("GrantApplication отправлена на экпертизу");
        }
    }
}
