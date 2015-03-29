using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungNatsDBv1.DataModels
{
    public partial class PamphletDelivery : Interfaces.IKnownIndividualLog, Interfaces.IVoterLog, Interfaces.IAddressLog
    {
        public DateTime InteractionDate
        {
            get { return DateTimeDelivered; }
        }

        public string GetHtml(string[] elementsToExclude)
        {
            string html = "<div><p><b>" + this.KnownIndividual.ToString() + "</b> delivered a <b>" + this.PamphletRun.PamphletRunShortTitle + "</b> pamphlet to <b>" + this.Address.Address1 + "</b> on <i>" + this.DateTimeDelivered.ToLongDateString() + "</i></p></div>";
            return html;
        }


        public bool RelatesToVoter(int VoterId)
        {
            foreach (Voter voter in this.Address.Voters)
            {
                if (voter.VoterId == VoterId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
