using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AUSKF.Api.Entities.Identity
{
    public class UserLogin : IdentityUserLogin<Guid>
    {
        [Key]
        public Guid UserLoginId { get; set; }

        [ForeignKey("User")]
        public override Guid UserId { get; set; }

        public User User { get; set; }

        [MaxLength(256)]
        public override string ProviderKey { get; set; }

        [MaxLength(256)]
        public override string LoginProvider { get; set; }
    }
}