namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmailAddress
    {
        public EmailAddress()
        {
            Voters = new HashSet<Voter>();
        }

        [Key]
        public int EmailAddressesId { get; set; }

        [Column("EmailAddress")]
        [Required]
        [StringLength(150)]
        public string EmailAddress1 { get; set; }

        public virtual ICollection<Voter> Voters { get; set; }
    }
}
