namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PoliticalParty
    {
        public PoliticalParty()
        {
            Addresses = new HashSet<Address>();
            VoterAssessments = new HashSet<VoterAssessment>();
            Voters = new HashSet<Voter>();
        }

        public int PoliticalPartyId { get; set; }

        [Required]
        [StringLength(50)]
        public string PartyName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<VoterAssessment> VoterAssessments { get; set; }

        public virtual ICollection<Voter> Voters { get; set; }
    }
}
