using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungNatsDBv1.DataModels
{
    public partial class PhoneCall : Interfaces.IVoterLog, Interfaces.IKnownIndividualLog, Interfaces.IAddressLog
    {
        public int AddressId { get { return this.AddressId; } }

        public DateTime InteractionDate
        {
            get { return CallDateTime; }
        }

        public bool RelatesToVoter(int VoterId)
        {
            foreach (Voter voter in this.PhoneNumber.Voters)
            {
                if (voter.VoterId == VoterId)
                {
                    return true;
                }
            }
            foreach (Voter voter in this.PhoneNumber.Voters1)
            {
                if (voter.VoterId == VoterId)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetHtml(string[] elementsToExclude)
        {
            bool shortenCallersName = false;
            foreach (string item in elementsToExclude)
            {
                if (item.ToLower() == "caller")
                {
                    shortenCallersName = true;
                }
            }
            string html = "<div><p>";
            if (shortenCallersName)
            {
                html += "<b>" + this.KnownIndividual.FirstName + "</b>";
            }
            else
            {
                html += "<b>" + this.KnownIndividual.ToString() + "</b>";
            }
            html += " called <b>" + this.PhoneNumber.PhoneNumber1 + "</b> on <i>" + this.CallDateTime.ToLongDateString() + "</i> at <i>" + this.CallDateTime.ToLongTimeString() + "</i>. ";
            if (WasThePhoneAnswered)
            {
                html += this.KnownIndividual.FirstName + " spoke to a voter.";
            }
            else
            {
                html += "No one answered.";
            }
            html += "</p></div>";
            return html;
        }
    }
}
