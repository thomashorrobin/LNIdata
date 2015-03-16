namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Electorate
    {
        public Electorate()
        {
            Addresses = new HashSet<Address>();
            MPs = new HashSet<MP>();
            NationalPartyMembers = new HashSet<NationalPartyMember>();
            Voters = new HashSet<Voter>();
        }

        public int ElectorateId { get; set; }

        [Required]
        [StringLength(50)]
        public string ElectorateName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<MP> MPs { get; set; }

        public virtual ICollection<NationalPartyMember> NationalPartyMembers { get; set; }

        public virtual ICollection<Voter> Voters { get; set; }
    }
}
