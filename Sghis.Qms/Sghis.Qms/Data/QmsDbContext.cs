using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sghis.Core.Entity.Base;
using Sghis.Core.Entity.Qms;

namespace Sghis.Qms.Data
{
    public class QmsDbContext : DbContext
    {

        public QmsDbContext() : base("SghisQmsConnection")
        {
            //Database.CreateIfNotExists();
        }

        public DbSet<TokenSeed> TokenSeed { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<TokenLog> TokenLog { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientConnection> ClientConnection { get; set; }
        public DbSet<Zone> Zone { get; set; }
        public DbSet<ClientService> ClientService { get; set; }
        public DbSet<ZoneService> ZoneService { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //set all c# decimal values to sql numeric(18,4)
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 4).HasColumnType("NUMERIC"));

            modelBuilder.Entity<TokenSeed>().ToTable("TokenSeed", DbSchema.Qms.ToString());
            modelBuilder.Entity<Token>().ToTable("Token", DbSchema.Qms.ToString());
            modelBuilder.Entity<TokenLog>().ToTable("TokenLog", DbSchema.Qms.ToString());
            modelBuilder.Entity<Client>().ToTable("Client", DbSchema.Qms.ToString());
            modelBuilder.Entity<ClientConnection>().ToTable("ClientConnection", DbSchema.Qms.ToString());
            modelBuilder.Entity<Zone>().ToTable("Zone", DbSchema.Qms.ToString());
            modelBuilder.Entity<ClientService>().ToTable("ClientService", DbSchema.Qms.ToString());
            modelBuilder.Entity<ZoneService>().ToTable("ZoneService", DbSchema.Qms.ToString());
        }

    }

}

