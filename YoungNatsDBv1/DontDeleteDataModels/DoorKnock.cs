using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungNatsDBv1.DataModels
{
    public partial class DoorKnock : Interfaces.IKnownIndividualLog, Interfaces.IVoterLog
    {
        public string GetHtml(string[] elementsToExclude)
        {
            bool shortenName = false;
            bool shortenAddress = false;
            foreach (string item in elementsToExclude)
            {
                if (item.ToLower() == "name")
                {
                    shortenName = true;
                }
                else if (item.ToLower() == "address")
                {
                    shortenAddress = true;
                }
            }
            string suffix;
            if (SpokeToSomeoneAtTheAddress)
            {
                suffix = KnownIndividual.FirstName + " spoke to a voter.";
            }
            else
            {
                suffix = "No one was home.";
            }
            if (shortenName)
            {
                return "<div><p><b>" + KnownIndividual.FirstName + "</b> door knocked <b>" + Address.Address1 + "</b> on <i>" + this.DateAndTime.ToLongDateString() + "</i>. " + suffix + "</p></div>";
            }
            else if (shortenAddress)
            {
                return "<div><p><b>" + KnownIndividual.ToString() + "</b> door knocked this address on <i>" + this.DateAndTime.ToLongDateString() + "</i>. " + suffix + "</p></div>";
            }
            return "<div><p><b>" + KnownIndividual.ToString() + "</b> door knocked <b>" + Address.Address1 + "</b> on <i>" + this.DateAndTime.ToLongDateString() + "</i>. " + suffix + "</p></div>";
        }

        public DateTime InteractionDate
        {
            get { return DateAndTime; }
        }


        public bool RelatesToVoter(int VoterId)
        {
            bool relatesToVoter = false;
            foreach (Voter voter in this.Address.Voters)
            {
                if (voter.VoterId == VoterId)
                {
                    relatesToVoter = true;
                }
            }
            return relatesToVoter;
        }
    }
}
