namespace YoungNatsDBv1.DataModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressNote> AddressNotes { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AssignedCall> AssignedCalls { get; set; }
        public virtual DbSet<DoorKnock> DoorKnocks { get; set; }
        public virtual DbSet<Electorate> Electorates { get; set; }
        public virtual DbSet<GeoTag> GeoTags { get; set; }
        public virtual DbSet<KnownIndividual> KnownIndividuals { get; set; }
        public virtual DbSet<MP> MPs { get; set; }
        public virtual DbSet<NationalPartyMember> NationalPartyMembers { get; set; }
        public virtual DbSet<PamphletDelivery> PamphletDeliveries { get; set; }
        public virtual DbSet<PamphletRun> PamphletRuns { get; set; }
        public virtual DbSet<PhoneCall> PhoneCalls { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<PoliticalParty> PoliticalParties { get; set; }
        public virtual DbSet<VoterAssessment> VoterAssessments { get; set; }
        public virtual DbSet<VoterNote> VoterNotes { get; set; }
        public virtual DbSet<Voter> Voters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .Property(e => e.Address1)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .Property(e => e.PoliticalLeanings)
                .IsUnicode(false);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.AddressNotes)
                .WithRequired(e => e.Address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.DoorKnocks)
                .WithRequired(e => e.Address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>()
                .HasOptional(e => e.GeoTag1)
                .WithRequired(e => e.Address);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.PamphletDeliveries)
                .WithRequired(e => e.Address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.Voters)
                .WithRequired(e => e.Address)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Electorate>()
                .Property(e => e.ElectorateName)
                .IsUnicode(false);

            modelBuilder.Entity<Electorate>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.Electorate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Electorate>()
                .HasMany(e => e.Voters)
                .WithRequired(e => e.Electorate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeoTag>()
                .HasMany(e => e.Addresses)
                .WithOptional(e => e.GeoTag)
                .HasForeignKey(e => e.GeoTagAddressId);

            modelBuilder.Entity<KnownIndividual>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<KnownIndividual>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<KnownIndividual>()
                .HasMany(e => e.AddressNotes)
                .WithRequired(e => e.KnownIndividual)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KnownIndividual>()
                .HasMany(e => e.AssignedCalls)
                .WithRequired(e => e.KnownIndividual)
                .HasForeignKey(e => e.AssignedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KnownIndividual>()
                .HasMany(e => e.AssignedCalls1)
                .WithRequired(e => e.KnownIndividual1)
                .HasForeignKey(e => e.AssignedTo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KnownIndividual>()
                .HasMany(e => e.DoorKnocks)
                .WithRequired(e => e.KnownIndividual)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KnownIndividual>()
                .HasOptional(e => e.MP)
                .WithRequired(e => e.KnownIndividual);

            modelBuilder.Entity<KnownIndividual>()
                .HasOptional(e => e.NationalPartyMember)
                .WithRequired(e => e.KnownIndividual);

            modelBuilder.Entity<KnownIndividual>()
                .HasMany(e => e.PamphletDeliveries)
                .WithRequired(e => e.KnownIndividual)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KnownIndividual>()
                .HasMany(e => e.PhoneCalls)
                .WithRequired(e => e.KnownIndividual)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KnownIndividual>()
                .HasMany(e => e.VoterAssessments)
                .WithRequired(e => e.KnownIndividual)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KnownIndividual>()
                .HasMany(e => e.VoterNotes)
                .WithRequired(e => e.KnownIndividual)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PamphletRun>()
                .Property(e => e.PamphletRunNotes)
                .IsUnicode(false);

            modelBuilder.Entity<PamphletRun>()
                .Property(e => e.PamphletRunShortTitle)
                .IsUnicode(false);

            modelBuilder.Entity<PamphletRun>()
                .HasMany(e => e.PamphletDeliveries)
                .WithRequired(e => e.PamphletRun)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhoneCall>()
                .Property(e => e.CallNotes)
                .IsUnicode(false);

            modelBuilder.Entity<PhoneNumber>()
                .Property(e => e.PhoneNumber1)
                .IsUnicode(false);

            modelBuilder.Entity<PhoneNumber>()
                .Property(e => e.PhoneType)
                .IsUnicode(false);

            modelBuilder.Entity<PhoneNumber>()
                .HasMany(e => e.AssignedCalls)
                .WithRequired(e => e.PhoneNumber)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhoneNumber>()
                .HasMany(e => e.PhoneCalls)
                .WithRequired(e => e.PhoneNumber)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhoneNumber>()
                .HasMany(e => e.Voters)
                .WithOptional(e => e.PhoneNumber)
                .HasForeignKey(e => e.CellNumber);

            modelBuilder.Entity<PhoneNumber>()
                .HasMany(e => e.Voters1)
                .WithOptional(e => e.PhoneNumber1)
                .HasForeignKey(e => e.HomeNumber);

            modelBuilder.Entity<PoliticalParty>()
                .Property(e => e.PartyName)
                .IsUnicode(false);

            modelBuilder.Entity<Voter>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Voter>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Voter>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Voter>()
                .Property(e => e.PoliticalLeanings)
                .IsUnicode(false);

            modelBuilder.Entity<Voter>()
                .HasMany(e => e.VoterAssessments)
                .WithRequired(e => e.Voter)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Voter>()
                .HasMany(e => e.VoterNotes)
                .WithRequired(e => e.Voter)
                .WillCascadeOnDelete(false);
        }
    }
}
