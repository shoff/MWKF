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
        private IDbSet<UserClaim> userClaims;
        private IDbSet<UserRole> userRoles;
        private IDbSet<KendoRank> kendoRanks;
        private IDbSet<Dojo> dojos;


        public IDbSet<User> Users => this.users ?? (this.users = this.SetEntity<User>());

        public IDbSet<UserClaim> UserClaims => this.userClaims ?? (this.userClaims = this.SetEntity<UserClaim>());
        
        public IDbSet<UserLogin> UserLogins => this.userLogins ?? (this.userLogins = this.SetEntity<UserLogin>());

        public IDbSet<UserProfile> UserProfiles => this.userProfiles ?? (this.userProfiles = this.SetEntity<UserProfile>());

        public IDbSet<UserRole> UserRoles => this.userRoles ?? (this.userRoles = this.SetEntity<UserRole>());

        public IDbSet<KendoRank> KendoRanks => this.kendoRanks ?? (this.kendoRanks = this.SetEntity<KendoRank>());

        public IDbSet<Dojo> Dojos => this.dojos ?? (this.dojos = this.SetEntity<Dojo>());

        public IDbSet<Log> Logs => this.logs ?? (this.logs = this.SetEntity<Log>());
    }
}