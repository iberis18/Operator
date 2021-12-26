using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public partial class MobileOperator : DbContext
    {
        public MobileOperator()
            : base("name=MobileOperator")
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Call> Call { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<FL> FL { get; set; }
        public virtual DbSet<Rate> Rate { get; set; }
        public virtual DbSet<RateHistory> RateHistory { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceHistory> ServiceHistory { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Type> Type { get; set; }
        public virtual DbSet<UL> UL { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<Call>()
                .Property(e => e.callerNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Call>()
                .Property(e => e.calledNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Call>()
                .Property(e => e.cost)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Client>()
                .Property(e => e.number)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Call)
                .WithOptional(e => e.Client)
                .HasForeignKey(e => e.calledId);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Call1)
                .WithRequired(e => e.Client1)
                .HasForeignKey(e => e.callerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasOptional(e => e.FL)
                .WithRequired(e => e.Client);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.RateHistory)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.clientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.ServiceHistory)
                .WithRequired(e => e.Client)
                .HasForeignKey(e => e.clientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasOptional(e => e.UL)
                .WithRequired(e => e.Client);

            modelBuilder.Entity<FL>()
                .Property(e => e.FIO)
                .IsUnicode(false);

            modelBuilder.Entity<FL>()
                .Property(e => e.pasportDetales)
                .IsUnicode(false);

            modelBuilder.Entity<Rate>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Rate>()
                .HasMany(e => e.Client)
                .WithOptional(e => e.Rate1)
                .HasForeignKey(e => e.rate);

            modelBuilder.Entity<Rate>()
                .HasMany(e => e.RateHistory)
                .WithRequired(e => e.Rate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServiceHistory)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Type>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .HasMany(e => e.Call)
                .WithRequired(e => e.Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UL>()
                .Property(e => e.organizationName)
                .IsUnicode(false);

            modelBuilder.Entity<UL>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Admin)
                .WithRequired(e => e.User);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.Client)
                .WithRequired(e => e.User);
        }
    }
}
