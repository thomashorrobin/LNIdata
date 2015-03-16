using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungNatsDBv1.Interfaces
{
    interface IVoterLog : IHtmlDivable
    {
        DateTime InteractionDate { get; }
        bool RelatesToVoter(int VoterId);
    }
}
