namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VoterNote
    {
        public int VoterNoteId { get; set; }

        public int VoterId { get; set; }

        [Required]
        public string NoteText { get; set; }

        public int KnownIndividualId { get; set; }

        public DateTime NoteDate { get; set; }

        public virtual KnownIndividual KnownIndividual { get; set; }

        public virtual Voter Voter { get; set; }
    }
}
