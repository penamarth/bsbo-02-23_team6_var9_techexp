using System;

namespace GrantSysytem.Domain.Handlers
{
    public class ExpertReviewHandler : ApplicationHandlerBase
    {
        private readonly double _minScore = 7.0;

        public override GrantApplication Handle(GrantApplication application)
        {
            var avgScore = application.getAverageScore();
            if (avgScore < _minScore)
            {
                application.Status = "Rejected";
                return application;
            }
            return base.Handle(application);
        }
    }
}