namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VoterAssessment")]
    public partial class VoterAssessment
    {
        public int VoterAssessmentId { get; set; }

        public int VoterId { get; set; }

        public int KnownIndividualId { get; set; }

        public DateTime AssessmentDate { get; set; }

        public int VotingNationalLikelihood { get; set; }

        public int? PoliticalPartyId { get; set; }

        public int VotingLikelihood { get; set; }

        public virtual KnownIndividual KnownIndividual { get; set; }

        public virtual PoliticalParty PoliticalParty { get; set; }

        public virtual Voter Voter { get; set; }
    }
}
