namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class KnownIndividual
    {
        public KnownIndividual()
        {
            AddressNotes = new HashSet<AddressNote>();
            AssignedCalls = new HashSet<AssignedCall>();
            AssignedCalls1 = new HashSet<AssignedCall>();
            DoorKnocks = new HashSet<DoorKnock>();
            PamphletDeliveries = new HashSet<PamphletDelivery>();
            PhoneCalls = new HashSet<PhoneCall>();
            VoterAssessments = new HashSet<VoterAssessment>();
            VoterNotes = new HashSet<VoterNote>();
        }

        public int KnownIndividualId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public bool CurrentNationalPartyMember { get; set; }

        public virtual ICollection<AddressNote> AddressNotes { get; set; }

        public virtual ICollection<AssignedCall> AssignedCalls { get; set; }

        public virtual ICollection<AssignedCall> AssignedCalls1 { get; set; }

        public virtual ICollection<DoorKnock> DoorKnocks { get; set; }

        public virtual MP MP { get; set; }

        public virtual NationalPartyMember NationalPartyMember { get; set; }

        public virtual ICollection<PamphletDelivery> PamphletDeliveries { get; set; }

        public virtual ICollection<PhoneCall> PhoneCalls { get; set; }

        public virtual ICollection<VoterAssessment> VoterAssessments { get; set; }

        public virtual ICollection<VoterNote> VoterNotes { get; set; }
    }
}
