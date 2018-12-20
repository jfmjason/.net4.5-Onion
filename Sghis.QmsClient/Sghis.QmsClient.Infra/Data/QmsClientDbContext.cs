
using System.Data.Entity;


namespace Sghis.QmsClient.Infra.Data
{
    public class QmsClientDbContext : DbContext
    {

        public QmsClientDbContext() : base("SghisQmsClientConnection")
        {
            //Database.CreateIfNotExists();
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //set all c# decimal values to sql numeric(18,4)
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 4).HasColumnType("NUMERIC"));
        }

    }

}

