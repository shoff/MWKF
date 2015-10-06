namespace MWKF.Api.Data
{
    using System.Data.Entity;
    using MWKF.Api.Interfaces;

    public sealed class EntityContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>, ISingletonLifestyle
    {
        protected override void Seed(DataContext context)
        {
            // TODO
        }
    }
}