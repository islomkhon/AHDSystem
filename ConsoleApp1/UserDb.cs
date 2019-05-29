namespace ConsoleApp1
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UserDb : DbContext
    {
        public UserDb()
            : base("name=UserDb")
        {
        }

        public virtual DbSet<UserDetail> UserDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetail>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.LoginName)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.LoginNameWithDomain)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.StreetAddress)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.PostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.HomePhone)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Extension)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Manager)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.ManagerEmail)
                .IsUnicode(false);

            modelBuilder.Entity<UserDetail>()
                .Property(e => e.Department)
                .IsUnicode(false);
        }
    }
}
