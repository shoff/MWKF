namespace AUSKF.Api.Migrations
{
    using Data; 
    using System.Data.Entity.Migrations; 

    internal sealed class Configuration : DbMigrationsConfiguration<AUSKF.Api.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(AUSKF.Api.Data.DataContext context)
        {
            new EntityContextInitializer().InitializeDatabase(context);
        } 
    }
} 
