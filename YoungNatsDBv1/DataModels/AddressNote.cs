namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AddressNote
    {
        public int AddressNoteId { get; set; }

        public int AddressId { get; set; }

        [Required]
        public string NoteText { get; set; }

        public int KnownIndividualId { get; set; }

        public DateTime NoteDate { get; set; }

        public virtual Address Address { get; set; }

        public virtual KnownIndividual KnownIndividual { get; set; }
    }
}
