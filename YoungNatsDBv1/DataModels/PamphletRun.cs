namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PamphletRun
    {
        public PamphletRun()
        {
            PamphletDeliveries = new HashSet<PamphletDelivery>();
        }

        public int PamphletRunId { get; set; }

        public DateTime DateCreated { get; set; }

        public string PamphletRunNotes { get; set; }

        [Required]
        [StringLength(25)]
        public string PamphletRunShortTitle { get; set; }

        public virtual ICollection<PamphletDelivery> PamphletDeliveries { get; set; }
    }
}
