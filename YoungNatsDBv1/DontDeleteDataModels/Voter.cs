using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungNatsDBv1.DataModels
{
    public partial class Voter
    {
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
