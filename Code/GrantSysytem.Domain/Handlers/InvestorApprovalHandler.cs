using System;

namespace GrantSysytem.Domain.Handlers
{
    public class InvestorApprovalHandler : ApplicationHandlerBase
    {
        public override GrantApplication Handle(GrantApplication application)
        {
            application.Status = "Approved";
            application.grant = new Grant
            {
                Amount = 100000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(6),
                Status = "Active"
            };
            return application;
        }
    }
}