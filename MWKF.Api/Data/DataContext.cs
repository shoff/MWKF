using System.Data.Entity;
using AUSKF.Api.Entities;
using AUSKF.Api.Entities.Identity;

namespace AUSKF.Api.Data
{
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
        private IDbSet<Address> addresses;
        private IDbSet<DojoMembership> dojoMemberships;
        private IDbSet<Federation> federations;
        private IDbSet<FederationMembership> federationMemberships;
        private IDbSet<FederationOfficer> federationOfficers;
        private IDbSet<OfficerRole> officerRoles;
        
        public IDbSet<User> Users => this.users ?? (this.users = this.SetEntity<User>());

        public IDbSet<UserClaim> UserClaims => this.userClaims ?? (this.userClaims = this.SetEntity<UserClaim>());
        
        public IDbSet<UserLogin> UserLogins => this.userLogins ?? (this.userLogins = this.SetEntity<UserLogin>());

        public IDbSet<UserProfile> UserProfiles => this.userProfiles ?? (this.userProfiles = this.SetEntity<UserProfile>());

        public IDbSet<UserRole> UserRoles => this.userRoles ?? (this.userRoles = this.SetEntity<UserRole>());

        public IDbSet<KendoRank> KendoRanks => this.kendoRanks ?? (this.kendoRanks = this.SetEntity<KendoRank>());

        public IDbSet<Dojo> Dojos => this.dojos ?? (this.dojos = this.SetEntity<Dojo>());

        public IDbSet<Log> Logs => this.logs ?? (this.logs = this.SetEntity<Log>());

        public IDbSet<Address> Addresses => this.addresses ?? (this.addresses = this.SetEntity<Address>());

        public IDbSet<DojoMembership> DojoMemberships => this.dojoMemberships ?? (this.dojoMemberships = this.SetEntity<DojoMembership>());

        public IDbSet<Federation> Federations => this.federations ?? (this.federations = this.SetEntity<Federation>());

        public IDbSet<FederationMembership> FederationMemberships => this.federationMemberships ?? (this.federationMemberships = this.SetEntity<FederationMembership>());

        public IDbSet<FederationOfficer> FederationOfficers => this.federationOfficers ?? (this.federationOfficers = this.SetEntity<FederationOfficer>());

        public IDbSet<OfficerRole> OfficerRoles => this.officerRoles ?? (this.officerRoles = this.SetEntity<OfficerRole>());
    }
}