namespace MWKF.Api.Entities.Identity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Newtonsoft.Json;

    public class UserRole : IdentityUserRole<Guid>
    {
        public UserRole()
        {
            this.Users = new List<User>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public override Guid RoleId { get; set; }

        [NotMapped, JsonIgnore]
        public override Guid UserId { get; set; }

        [MaxLength(56)]
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}