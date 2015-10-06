namespace MWKF.Api.Data
{
    using System.Data.Entity;
    using MWKF.Api.Entities;
    using Entities.Identity;

    public sealed partial class DataContext
    {
        private IDbSet<Log> logs;
        private IDbSet<User> users;
        private IDbSet<UserProfile> userProfiles;
        private IDbSet<UserLogin> userLogins;
        private IDbSet<UserRole> userGroups;
        private IDbSet<UserClaim> userClaims;
        private IDbSet<KendoRank> kendoRanks;


        public IDbSet<User> Users => this.users ?? (this.users = this.SetEntity<User>());

        public IDbSet<UserClaim> UserClaims => this.userClaims ?? (this.userClaims = this.SetEntity<UserClaim>());

        public IDbSet<UserRole> UserGroups => this.userGroups ?? (this.userGroups = this.SetEntity<UserRole>());

        public IDbSet<UserLogin> UserLogins => this.userLogins ?? (this.userLogins = this.SetEntity<UserLogin>());

        public IDbSet<UserProfile> UserProfiles => this.userProfiles ?? (this.userProfiles = this.SetEntity<UserProfile>());

        public IDbSet<KendoRank> KendoRanks => this.kendoRanks ?? (this.kendoRanks = this.SetEntity<KendoRank>());

        public IDbSet<Log> Logs => this.logs ?? (this.logs = this.SetEntity<Log>());
    }
}