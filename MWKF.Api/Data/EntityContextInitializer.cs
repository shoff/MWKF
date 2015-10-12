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
        private List<Dojo> dojos;
        
        protected override void Seed(DataContext context)
        {
            AddKendoRanks(context);
            AddRoles(context);
            AddAdminUser(context);
            AddDojos(context);
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

        private void AddDojos(DataContext context)
        {
            this.dojos = new List<Dojo>
            {
                new Dojo
                {
                    DojoName = "Minnehaha Kendo Club",
                    Address = new Address
                    {
                        AddressLine1 = "Minnesota Sword Club",
                        AddressLine2 = "4744 Chicago Ave.",
                        City = "Minneapolis",
                        State = "MN",
                        ZipCode = "55417"
                    },
                    Contact = "Robert Cochran",
                    Phone = "612-823-6715",
                    EmailAddress = "Rcochran_Minnehaha_at_msn_dot_com",
                    WebsiteUrl= "http://minnehahakendo.org/"

                },
                new Dojo
                {
                    DojoName = "Battle Creek Kendo Kai",
                    Address = new Address
                    {
                        AddressLine1 = "West Dickman Fitness Center",
                        AddressLine2 = "2851 W. Dickman Rd.",
                        City = "Battle Creek",
                        State = "MI",
                        ZipCode = "49037"
                    },
                    Contact = "David Christman",
                    Phone = "269-965-6590",
                    EmailAddress = "dtc12_at_comcast_dot_net"
                },
                new Dojo
                {
                    DojoName = "Bloomington Normal Mitsubishi Kendo Club",
                    Address = new Address
                    {
                        AddressLine1 = "Bromen Adult Day Care Center",
                        AddressLine2 = "202 E. Locust",
                        City = "Bloomington",
                        State = "IL",
                        ZipCode = "61701"
                    },
                    Contact = "David Shaneman",
                    Phone = "unknown",
                    EmailAddress = "ilwroby_at_springnet1_dot_com"
                },
                new Dojo
                {
                    DojoName = "Carleton College Kendo Club",
                    Address = new Address
                    {
                        AddressLine1 = "",
                        AddressLine2 = "",
                        City = "",
                        State = "MN",
                        ZipCode = ""
                    },
                    Contact = "John T.Born",
                    Phone = "unknown",
                    EmailAddress = "John born_at_mnkyudo_dot_org"
                },
                new Dojo
                {
                    DojoName = "Central Indiana Kendo Club",
                    Address = new Address
                    {
                        AddressLine1 = "",
                        AddressLine2 = "",
                        City = "",
                        State = "IN",
                        ZipCode = ""
                    },
                    Contact = "Hajime Sugawara",
                    Phone = "unknown",
                    EmailAddress = "hajime222_at_yahoo_dot_com"
                },
                new Dojo
                {
                    DojoName = "Chicago Kendo Dojo",
                    Address = new Address
                    {
                        AddressLine1 = "Buddhist Temple of Chicago",
                        AddressLine2 = "1151 W. Leland Ave.",
                        City = "Chicago",
                        State = "IL",
                        ZipCode = "60640"
                    },
                    Contact = "Frank Matsumoto",
                    Phone = "773-83-2282",
                    EmailAddress = "",
                    WebsiteUrl = "http://www.chicagokendo.org"
                },
                new Dojo
                {
                    DojoName = "Choyokan Kendo Dojo - Des Plaines",
                    Address = new Address
                    {
                        AddressLine1 = "Golf Maine Park District Gym",
                        AddressLine2 = "9229 W. Emerson Ave.",
                        City = "Des Plaines",
                        State = "IL",
                        ZipCode = "60016"
                    },
                    Contact = "Tom Okawara",
                    Phone = "",
                    EmailAddress = "Tomo-jo_at_sbcglobal_dot_net",
                    WebsiteUrl = ""
                },
                new Dojo
                {
                    DojoName = "Detroit Kendo Dojo",
                    Address = new Address
                    {
                        AddressLine1 = "Seaholm High School",
                        AddressLine2 = "2436 W. Lincoln St.",
                        City = "Birmingham",
                        State = "MI",
                        ZipCode = "48009"
                    },
                    Contact = "Katsuhiko Kan",
                    Phone = "",
                    EmailAddress = "detroitkendodojo_at_hotmail_dot_com",
                    WebsiteUrl = ""
                },
                new Dojo
                {
                    DojoName = "Choyokan Kendo Dojo",
                    Address = new Address
                    {
                        AddressLine1 = "Ravenswood Fellowship United Methodist Church",
                        AddressLine2 = "4511 N Hermitage",
                        City = "Chicago",
                        State = "IL",
                        ZipCode = "60640"
                    },
                    Contact = "Dr. Ken Sakamoto",
                    Phone = "",
                    EmailAddress = "sakamotok_at_mac_dot_com",
                    WebsiteUrl = "http://www.choyokan-kendo.org",
                    Notes = "W F 8-10pm"
                },
                new Dojo
                {
                    DojoName = "Choyokan Kendo Dojo",
                    Address = new Address
                    {
                        AddressLine1 = "UIC Sports and Fitness Center",
                        AddressLine2 = "828 S. Wolcott",
                        City = "Chicago",
                        State = "IL",
                        ZipCode = "60612"
                    },
                    Contact = "Dr. Ken Sakamoto",
                    Phone = "",
                    EmailAddress = "sakamotok_at_mac_dot_com",
                    WebsiteUrl = "http://www.choyokan-kendo.org",
                    Notes = "Su Noon-2pm"
                },
                new Dojo
                {
                    DojoName = "Eastern Michigan Kendo Dojo",
                    Address = new Address
                    {
                        AddressLine1 = "Eastern Michigan University",
                        AddressLine2 = "Warner Gymnasium",
                        City = "Ypsilanti",
                        State = "MI",
                        ZipCode = "48197"
                    },
                    Contact = "Charlie Kondek",
                    Phone = "734-484-3284",
                    EmailAddress = "charliekondek_at_yahoo_dot_com",
                    WebsiteUrl = ""
                },
                new Dojo
                {
                    DojoName = "University of Wisconsin Kendo Club",
                    Address = new Address
                    {
                        AddressLine1 = "University of Wisconsin-Madison Natatorium Gym Unit II",
                        AddressLine2 = "2000 Observatory Drive",
                        City = "Madison",
                        State = "WI",
                        ZipCode = "53706"
                    },
                    Contact = "J. Mark Kenoyer",
                    Phone = "",
                    EmailAddress = " jkenoyer_at_wisc_dot_edu",
                    WebsiteUrl = ""
                },
                new Dojo
                {
                    DojoName = "Valley View Kendo Club",
                    Address = new Address
                    {
                        AddressLine1 = "",
                        AddressLine2 = "3939 County Road B",
                        City = "La Crosse",
                        State = "WI",
                        ZipCode = "53706"
                    },
                    Contact = "Matthew Reardon",
                    Phone = "",
                    EmailAddress = "mjreards_at_yahoo_at_com",
                    WebsiteUrl = "",
                    Notes = " Th 8-9:30pm Sa 11am-1pm"
                },
                new Dojo
                {
                    DojoName = "University of Chicago Kendo Club",
                    Address = new Address
                    {
                        AddressLine1 = "Bartlette Gym",
                        AddressLine2 = "5640 S. University Ave.",
                        City = "Chicago",
                        State = "IL",
                        ZipCode = "60637"
                    },
                    Contact = "Hyonggun Choi",
                    Phone = "773-702-0187",
                    EmailAddress = "choihyonggun_at_msn_dot_com",
                    WebsiteUrl = ""
                },
                new Dojo
                {
                    DojoName = "Musoshindenryu Iaido - Moorhead Dojo",
                    Address = new Address
                    {
                        AddressLine1 = "",
                        AddressLine2 = "121 6th Ave S",
                        City = "Moorhead",
                        State = "MN",
                        ZipCode = "56560"
                    },
                    Contact = "Bradley Anderson",
                    Phone = "218-329-7284",
                    EmailAddress = "",
                    WebsiteUrl = "http://www.musoshindenryu.com",
                    Notes = "W 6:30-9:30pm"
                },
                new Dojo
                {
                    DojoName = "Moline Kendo Dojo",
                    Address = new Address
                    {
                        AddressLine1 = "Birdsell Chiropractic",
                        AddressLine2 = "1201 5th avenue",
                        City = "Moline",
                        State = "IL",
                        ZipCode = "61265"
                    },
                    Contact = "David Birdesll",
                    Phone = "309-736-0240",
                    EmailAddress = "davidbirdsell_at_sbcglobal_dot_net",
                    WebsiteUrl = ""
                },
                new Dojo
                {
                    DojoName = "Hokkyokusei Kendo Club",
                    Address = new Address
                    {
                        AddressLine1 = "Rochester Family YMCA",
                        AddressLine2 = "720 1st Ave. SW",
                        City = "Rochester",
                        State = "MN",
                        ZipCode = "55902"
                    },
                    Contact = "Stephen Voss",
                    Phone = "507-281-3163",
                    EmailAddress = "vosssenshu_at_aol_dot_com",
                    WebsiteUrl = ""
                }
            };

            this.dojos.ForEach(d => context.Dojos.Add(d));
            context.Commit();
        }
    }
}