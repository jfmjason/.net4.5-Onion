using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sghis.Core.Entity.Base;
using Sghis.Core.Entity.Common;
using Sghis.Core.Entity.Tariff;
using Sghis.Core.Entity.Master;
using Sghis.Core.Entity.Wards;

namespace Sghis.Core.Data
{
    public class SghisCoreDbContext : DbContext
    {

        public SghisCoreDbContext()
            : base("SghisConnection")
        {
            //Database.CreateIfNotExists();
        }

        #region Admin
        public DbSet<User> User { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Organization> Organization { get; set; }
        #endregion

        #region Tariff
        public DbSet<ServiceItemMaster> ServiceItemMaster { get; set; }
        public DbSet<IpPrice> IpPrice { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //set all c# decimal values to sql numeric(18,4)
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 4).HasColumnType("NUMERIC"));

            modelBuilder.Entity<User>().ToTable("User", DbSchema.Common.ToString());
            modelBuilder.Entity<Client>().ToTable("Client", DbSchema.Common.ToString());
            modelBuilder.Entity<Organization>().ToTable("Organization", DbSchema.Common.ToString());
            

            modelBuilder.Entity<ServiceItemMaster>().ToTable("ServiceItemMaster", DbSchema.Master.ToString());
            modelBuilder.Entity<IpPrice>().ToTable("IpPrice", DbSchema.Master.ToString());
        }

    }

}

