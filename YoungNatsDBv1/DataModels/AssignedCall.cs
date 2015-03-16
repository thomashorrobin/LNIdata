namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AssignedCall
    {
        public int AssignedCallId { get; set; }

        public int AssignedBy { get; set; }

        public int AssignedTo { get; set; }

        public int PhoneNumberId { get; set; }

        public DateTime DateTimeAssigned { get; set; }

        public int? PhoneCallId { get; set; }

        public bool CallCompleted { get; set; }

        public virtual KnownIndividual KnownIndividual { get; set; }

        public virtual KnownIndividual KnownIndividual1 { get; set; }

        public virtual PhoneNumber PhoneNumber { get; set; }

        public virtual PhoneCall PhoneCall { get; set; }
    }
}
