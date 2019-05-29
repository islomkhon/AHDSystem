using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ConsoleApp1.DAL
{
    class UserContext : DbContext
    {
        public UserContext()
            : base("UserContext")
        {
        }

        public DbSet<UserDetail> UserDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
            //modelBuilder.Conventions.Remove<pluralizingtablenameconvention>();
        }
    }
}