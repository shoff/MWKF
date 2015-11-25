namespace AUSKF.Api.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ConfigurationAccount : DbMigrationsConfiguration<AUSKF.Api.Models.Account.ApplicationDbContext>
    {
        public ConfigurationAccount()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AUSKF.Api.Models.Account.ApplicationDbContext context)
        { 
        }
    }
}
