using System;
using System.Collections.Generic;

namespace GrantSysytem.Domain
{
    public class GrantApplication
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int ApplicantId { get; set; }

        public List<Review> reviews { get; set; } = new List<Review>();
        public Grant grant { get; set; }
    }
}
