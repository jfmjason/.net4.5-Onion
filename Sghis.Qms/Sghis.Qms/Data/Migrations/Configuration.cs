using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Migrations.Model;
using Sghis.Core.Entity.Qms;

namespace Sghis.Qms.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Sghis.Qms.Data.QmsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //set all CreatedAt column default values to GETDATE();
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(QmsDbContext context)
        {
            var tokenSeed = context.TokenSeed.Find(1);
        
            if (tokenSeed == null)
            {
                var zone = new Zone()
                {
                    Code = "ER",
                    Name = "Emergency",
                    CreatedBy = "Seed",
                    CreatedAt = DateTime.Now,
                    OrganizationId = 1,
                    Deleted = false
                };

                var client = new Client()
                {
                    IpAddress   = "130.1.8.7",
                    Zone = zone,
                    DeviceId = "PC1",
                    ClientType = ClientType.His,
                    ActionBy = "Seed",
                    ActionAt = DateTime.Now,
                    Deleted = false,
                };

                var newTokenSeed = new TokenSeed()
                {
                    Zone = zone,
                    Number = 0,
                    ActionBy = "Seed",
                    ActionAt = DateTime.Now,
                    Deleted = false,
                };
                context.Zone.Add(zone);
                context.Client.Add(client);
                context.TokenSeed.Add(newTokenSeed);
                context.SaveChanges();
            }
        }

    }

    internal class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(AddColumnOperation addColumnOperation)
        {
            SetCreatedUtcColumn(addColumnOperation.Column);

            base.Generate(addColumnOperation);
        }

        protected override void Generate(CreateTableOperation createTableOperation)
        {
            SetCreatedUtcColumn(createTableOperation.Columns);

            base.Generate(createTableOperation);
        }

        private static void SetCreatedUtcColumn(IEnumerable<ColumnModel> columns)
        {
            foreach (var columnModel in columns)
            {
                SetCreatedUtcColumn(columnModel);
            }
        }

        private static void SetCreatedUtcColumn(PropertyModel column)
        {
            if (column.Name == "CreatedAt")
            {
                column.DefaultValueSql = "GETDATE()";
            }
        }
    }

}