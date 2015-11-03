namespace MWKF.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Individual's membership to a federation information
    /// </summary>
    public class FederationMembership
    {
        /// <summary>
        /// Federation membership identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FederationMembershipId { get; set; }
         
        /// <summary>
        /// User who's membership to the federation is defined.
        /// </summary>
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
         
        /// <summary>
        /// User who's membership to the dojo is defined.
        /// </summary>
        [Required]
        [ForeignKey("Federation")]
        public Guid FederationId { get; set; }
        
        /// <summary>
        /// Year for which this user is a member of the federation
        /// </summary>
        [Required]
        public int MembershipYear { get; set; }
    }
}