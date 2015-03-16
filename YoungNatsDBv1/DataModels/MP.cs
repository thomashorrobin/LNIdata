namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KnownIndividualId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateFirstBecameMP { get; set; }

        public int? ElectorateId { get; set; }

        public virtual Electorate Electorate { get; set; }

        public virtual KnownIndividual KnownIndividual { get; set; }
    }
}
