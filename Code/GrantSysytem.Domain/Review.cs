namespace GrantSysytem.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public float Score { get; set; }
        public string Comment { get; set; }
        public DateTime SubmissionDate { get; set; }
        
        public int ApplicationId { get; set; } // Связь с GrantApplication
        public int ExpertId { get; set; }     // Связь с Expert

        public void Submit()
        {
            Console.WriteLine("Рецензия успешно отправлена");
        }
    }
}