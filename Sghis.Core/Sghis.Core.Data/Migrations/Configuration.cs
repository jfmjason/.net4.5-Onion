using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Sghis.Core.Entity.Base;
using Sghis.Core.Entity.Common;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Migrations.Model;

namespace Sghis.Core.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Sghis.Core.Data.SghisCoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //set all CreatedAt column default values to GETDATE();
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(SghisCoreDbContext context)
        {
            var userExist = context.User.Find(1);
            if (userExist == null)
            {
                var organization = new Organization()
                {
                    Code = "SA01",
                    Name = "SAUDI ALIMANI 01",
                    CreatedAt = DateTime.Now,
                    Deleted = true,

                };

                var organization2 = new Organization()
                {
                    Code = "SA01-A",
                    Name = "SAUDI ALIMANI 01 - BAWADI",
                    CreatedAt = DateTime.Now,
                    Deleted = true,
                    ParentOrganization = organization
                };

                var user = new User()
                {
                    UserName = "beewan",
                    Department = "ITmang",
                    Email = "beewan@sgh.net",
                    FullName = "Bee Wan",
                    CreatedBy = "System Generated",
                    CreatedAt = DateTime.Now,
                    Deleted = true,
                    OrganizationId = organization2.Id
                };
                context.Organization.Add(organization2);
                context.User.Add(user);

                if (context.Client.Count() == 0)
                {
                    context.Client.AddRange(BuildClientsList());
                }

                context.SaveChanges();
            }
        }

        private static List<Client> BuildClientsList()
        {
            List<Client> ClientsList = new List<Client>
            {
                new Client
                {   Secret= CryptoHelper.GetHash("webnative@sghgroup.net"),
                    Name="Mvc WebApi Application",
                    ApplicationType =ApplicationTypes.WebNative,
                    Active = true,
                    RefreshTokenLifeTime = 120,
                    AllowedOrigin = "*"
                },
                new Client
                {   Secret=CryptoHelper.GetHash("jquery@sghgroup.net"),
                    Name="jQuery Application Client",
                    ApplicationType =ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                } ,
                new Client
                {   Secret=CryptoHelper.GetHash("mobile@sghgroup.net"),
                    Name="Mobile Application Client",
                    ApplicationType =ApplicationTypes.Mobile,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                },
                new Client
                {   Secret=CryptoHelper.GetHash("desktop@sghgroup.net"),
                    Name="Desktop Application Client",
                    ApplicationType =ApplicationTypes.Desktop,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                }
            };

            return ClientsList;
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