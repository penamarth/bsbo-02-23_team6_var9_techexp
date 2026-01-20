using GrantSysytem.Domain;

namespace GrantSystem.Interfaces
{
    public interface IStatsService
    {
        ApplicationStats getApplicationStats();
        GrantStats getGrantStats(string investorId);
        ExpertStats getExpertStats(string expertId);
    }
}