namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PamphletDelivery
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PamphletRunId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AddressId { get; set; }

        public int KnownIndividualId { get; set; }

        public DateTime DateTimeDelivered { get; set; }

        public virtual Address Address { get; set; }

        public virtual KnownIndividual KnownIndividual { get; set; }

        public virtual PamphletRun PamphletRun { get; set; }

    }
}
