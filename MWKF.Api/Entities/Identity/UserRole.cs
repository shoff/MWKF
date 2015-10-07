namespace MWKF.Api.Entities.Identity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserRole : IdentityUserRole<Guid>
    {
        [Key]
        public override Guid RoleId { get; set; }
    }
}