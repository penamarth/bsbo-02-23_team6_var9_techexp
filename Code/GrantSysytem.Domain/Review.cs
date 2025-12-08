using System;

namespace GrantSysytem.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public string Comment { get; set; }
        public DateTime SubmissionDate { get; set; }

        public void Submit()
        {
            Console.WriteLine("Рецензия успешно отправлена");
        }
    }
}
