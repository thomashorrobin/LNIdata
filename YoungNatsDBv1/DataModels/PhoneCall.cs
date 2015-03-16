namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PhoneCall
    {
        public PhoneCall()
        {
            AssignedCalls = new HashSet<AssignedCall>();
        }

        public int PhoneCallId { get; set; }

        public int PhoneNumberId { get; set; }

        public int KnownIndividualId { get; set; }

        public bool WasThePhoneAnswered { get; set; }

        public string CallNotes { get; set; }

        public DateTime CallDateTime { get; set; }

        public virtual ICollection<AssignedCall> AssignedCalls { get; set; }

        public virtual KnownIndividual KnownIndividual { get; set; }

        public virtual PhoneNumber PhoneNumber { get; set; }
    }
}
