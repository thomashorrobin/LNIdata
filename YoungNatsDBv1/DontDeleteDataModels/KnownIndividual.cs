using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungNatsDBv1.DataModels
{
    public partial class KnownIndividual
    {
        public string FullName { get { return FirstName + " " + LastName; } }

        public override string ToString()
        {
            string suffix;
            if (this.MP != null)
            {
                suffix = " (List MP)";
            }
            else if (this.NationalPartyMember != null && this.NationalPartyMember.MemberSince != null)
            {
                suffix = " - member since " + ((DateTime)NationalPartyMember.MemberSince).ToShortDateString();
            }
            else
            {
                suffix = string.Empty;
            }
            return this.FirstName + " " + this.LastName + suffix;
        }
    }
}
