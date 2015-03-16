namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Address
    {
        public Address()
        {
            AddressNotes = new HashSet<AddressNote>();
            DoorKnocks = new HashSet<DoorKnock>();
            PamphletDeliveries = new HashSet<PamphletDelivery>();
            Voters = new HashSet<Voter>();
        }

        public int AddressId { get; set; }

        [Column("Address")]
        [Required]
        [StringLength(200)]
        public string Address1 { get; set; }

        public int ElectorateId { get; set; }

        [StringLength(50)]
        public string PoliticalLeanings { get; set; }

        public int? GeoTagAddressId { get; set; }

        public virtual Electorate Electorate { get; set; }

        public virtual GeoTag GeoTag { get; set; }

        public virtual ICollection<AddressNote> AddressNotes { get; set; }

        public virtual ICollection<DoorKnock> DoorKnocks { get; set; }

        public virtual GeoTag GeoTag1 { get; set; }

        public virtual ICollection<PamphletDelivery> PamphletDeliveries { get; set; }

        public virtual ICollection<Voter> Voters { get; set; }
    }
}
