namespace MWKF.Api.Entities.Identity
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class UserClaim : IdentityUserClaim<Guid>
    {
        /// <summary>
        /// Gets or sets the user claim identifier.
        /// </summary>
        /// <value>
        /// The user claim identifier.
        /// </value>
        [Key]
        public Guid UserClaimId { get; set; }


        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}