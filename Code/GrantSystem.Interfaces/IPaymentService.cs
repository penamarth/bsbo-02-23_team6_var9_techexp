using GrantSysytem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrantSystem.Interfaces
{
    public interface IPaymentService
    {
        bool processPayment(GrantApplication grant);
    }
}
