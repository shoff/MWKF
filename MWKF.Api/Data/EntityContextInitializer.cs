namespace MWKF.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using MWKF.Api.Entities;
    using MWKF.Api.Entities.Identity;
    using MWKF.Api.Extensions;
    using MWKF.Api.Interfaces;

    public sealed class EntityContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>, ISingletonLifestyle
    {
        private List<KendoRank> kendoRanks;
        private List<UserRole> userRoles;
        
        protected override void Seed(DataContext context)
        {
            AddKendoRanks(context);
            AddRoles(context);
            AddAdminUser(context);
        }

        private void AddKendoRanks(DataContext context)
        {
            kendoRanks = new List<KendoRank>
            {
                new KendoRank {  KendoRankName="Nikyu", KendoRankNumeric = 10,
                    Eligibility ="The examination for kyu shall be determined by each organization.",
                    ConsentingExaminersRequired = 1, MinimumRankOfExaminers = "-", NumberOfExaminers=1  },

                new KendoRank {KendoRankName="Ikkyu", KendoRankNumeric = 9,
                    Eligibility ="No time period stipulated. Matches, Kata 1-3, Written examination",
                    ConsentingExaminersRequired = 1, MinimumRankOfExaminers = "-", NumberOfExaminers=1 },

                new KendoRank {  KendoRankName="Shodan", KendoRankNumeric = 8,
                    Eligibility ="3 months or more after receipt of Ikkyu and age 14 or higher. Matches, Kata 1-5, Written examination",
                    ConsentingExaminersRequired = 3, MinimumRankOfExaminers = "Yondan or higher", NumberOfExaminers=5 },

                new KendoRank {  KendoRankName="Nidan", KendoRankNumeric = 7,
                    Eligibility ="1 year or more after receipt of Shodan. Matches, Kata 1-7, Written examination",
                    ConsentingExaminersRequired = 3, MinimumRankOfExaminers = "Godan or higher", NumberOfExaminers=5  },

                new KendoRank {  KendoRankName="Sandan", KendoRankNumeric = 6,
                    Eligibility ="2 years or more after receipt of Nidan. Matches, Kata 1-7 and kodachi kata 1-3, Written examination",
                    ConsentingExaminersRequired = 3, MinimumRankOfExaminers = "Godan or higher", NumberOfExaminers=5  },

                new KendoRank {  KendoRankName="Yondan", KendoRankNumeric = 5,
                    Eligibility ="3 years or more after receipt of Sandan. Matches, Kata 1-7 and kodachi kata 1-3, Written examination",
                    ConsentingExaminersRequired = 5, MinimumRankOfExaminers = "Rokudan or higher", NumberOfExaminers=7  },

                new KendoRank {  KendoRankName="Godan", KendoRankNumeric = 4,
                    Eligibility ="4 years or more after receipt of Yondan. Kata 1-7 and kodachi kata 1-3, Written examination",
                    ConsentingExaminersRequired = 5, MinimumRankOfExaminers = "Nanadan or higher", NumberOfExaminers=7  },

                new KendoRank {  KendoRankName="Rokudan", KendoRankNumeric = 3,
                    Eligibility ="5 years or more after receipt of Godan. Kata 1-7 and kodachi kata 1-3, Written examination & refereeing" ,
                    ConsentingExaminersRequired = 5, MinimumRankOfExaminers = "Nanadan or higher", NumberOfExaminers=7 },

                new KendoRank {  KendoRankName="Nanadan", KendoRankNumeric = 2,
                    Eligibility ="6 years or more after receipt of Rokudan. Kata 1-7 and kodachi kata 1-3, Written examination & refereeing" ,
                    ConsentingExaminersRequired = 5, MinimumRankOfExaminers = "Nanadan or higher", NumberOfExaminers=7 },

                new KendoRank {  KendoRankName="Hachi-Dan", KendoRankNumeric = 1,
                    Eligibility ="10 years or more after receipt of Nanadan and age 46 or higher. Kata 1-7 and kodachi kata 1-3 Written examination & thesis",
                    ConsentingExaminersRequired = 7, MinimumRankOfExaminers = "Hachi-Dan", NumberOfExaminers=7  },
            };

            kendoRanks.ForEach(kr => context.KendoRanks.Add(kr));
            context.Commit();
        }

        private void AddRoles(DataContext context)
        {
            this.userRoles = new List<UserRole>
            {
                 new UserRole { RoleName="Administrator" },
                 new UserRole {RoleName = "Contributor" },
                 new UserRole { RoleName="Member" }
            };
            this.userRoles.ForEach(ur => context.UserRoles.Add(ur));
            context.Commit();
        }

        private void AddAdminUser(DataContext context)
        {
            User user = new User
            {
                Active = true,
                DisplayName = "Webmaster",
                Email = "Admin@mwkf.org",
                EmailConfirmed = true,
                JoinedDate = DateTime.UtcNow,
                KendoRank = this.kendoRanks.FindLast(x => x.KendoRankName != ""),
                LastLogin = DateTime.UtcNow,
                LastSearch = "na",
                MaximumDaysBetweenPasswordChange = 180,
                PasswordLastChangedDate = DateTime.UtcNow,
                Profile = new UserProfile
                {
                     AllowHtmlSig = true
                },
                UserName = "Admin",
                Password = "P@ssword1".Sha256Hash(),
                PasswordHash = "P@ssword1".Sha256Hash()
            };
            this.userRoles.ForEach(ur => user.Roles.Add(ur));
            context.Users.Add(user);
            context.Commit();
        }
    }
}