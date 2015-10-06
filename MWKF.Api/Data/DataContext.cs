namespace MWKF.Api.Data
{
    using System.Data.Entity;
    using MWKF.Api.Entities;

    public sealed partial class DataContext
    {
        private IDbSet<Log> logs;
        
        public IDbSet<Log> Logs => this.logs ?? (this.logs = this.SetEntity<Log>());
    }
}