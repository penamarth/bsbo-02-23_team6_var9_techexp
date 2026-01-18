namespace GrantSysytem.Domain
{
    public class ExpertStats
    {
        public string ExpertId { get; set; }
        public int ReviewsCompleted { get; set; }
        public double AverageReviewScore {  get; set; }
        public int Ranking { get; set; }

        public ExpertStats(string expertId, int reviews, double avgScore, int ranking)
        {
            ExpertId = expertId;
            ReviewsCompleted = reviews;
            AverageReviewScore = avgScore;
            Ranking = ranking;
        }
    }
}
