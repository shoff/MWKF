namespace MWKF.Api.Entities.Identity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserRole : IdentityUserRole<Guid>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public override Guid RoleId { get; set; }

        [ForeignKey("User")]
        public override Guid UserId { get; set; }

        public User User { get; set; }

        [MaxLength(56)]
        public string RoleName { get; set; }
    }
}