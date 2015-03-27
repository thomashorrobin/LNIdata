namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Voter
    {
        public Voter()
        {
            VoterAssessments = new HashSet<VoterAssessment>();
            VoterNotes = new HashSet<VoterNote>();
        }

        public int VoterId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public int AddressId { get; set; }

        public int ElectorateId { get; set; }

        public int? CellNumber { get; set; }

        public int? HomeNumber { get; set; }

        public int? Email { get; set; }

        public int? PoliticalLeanings { get; set; }

        public virtual Address Address { get; set; }

        public virtual Electorate Electorate { get; set; }

        public virtual EmailAddress EmailAddress { get; set; }

        public virtual PhoneNumber PhoneNumber { get; set; }

        public virtual PhoneNumber PhoneNumber1 { get; set; }

        public virtual PoliticalParty PoliticalParty { get; set; }

        public virtual ICollection<VoterAssessment> VoterAssessments { get; set; }

        public virtual ICollection<VoterNote> VoterNotes { get; set; }
    }
}
