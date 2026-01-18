using GrantSysytem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrantSystem.Interfaces
{
    public interface IStatsService
    {
        ApplicationStats getApplicationStats();
        GrantStats getGrantStats(string investorId);
        ExpertStats getExpertStats(string expertId);
    }
}
