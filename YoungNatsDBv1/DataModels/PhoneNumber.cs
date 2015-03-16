namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PhoneNumber
    {
        public PhoneNumber()
        {
            AssignedCalls = new HashSet<AssignedCall>();
            PhoneCalls = new HashSet<PhoneCall>();
            Voters = new HashSet<Voter>();
            Voters1 = new HashSet<Voter>();
        }

        public int PhoneNumberId { get; set; }

        [Column("PhoneNumber")]
        [Required]
        [StringLength(20)]
        public string PhoneNumber1 { get; set; }

        [Required]
        [StringLength(15)]
        public string PhoneType { get; set; }

        public virtual ICollection<AssignedCall> AssignedCalls { get; set; }

        public virtual ICollection<PhoneCall> PhoneCalls { get; set; }

        public virtual ICollection<Voter> Voters { get; set; }

        public virtual ICollection<Voter> Voters1 { get; set; }
    }
}
