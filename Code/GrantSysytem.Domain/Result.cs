using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrantSysytem.Domain
{
    public class Result
    {
        public bool Success { get; set; }

        public Result(bool success) { Success = success; }
    }
}
