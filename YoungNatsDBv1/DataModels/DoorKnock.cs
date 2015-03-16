namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DoorKnock
    {
        public int DoorKnockId { get; set; }

        public int KnownIndividualId { get; set; }

        public string Notes { get; set; }

        public bool SpokeToSomeoneAtTheAddress { get; set; }

        public DateTime DateAndTime { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual KnownIndividual KnownIndividual { get; set; }
    }
}
