namespace MWKF.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Users holding an office of a federation
    /// </summary>
    public class FederationOfficer
    {
        /// <summary>
        /// Federation officer holder identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FederationOfficerId { get; set; }

        /// <summary>
        /// User who's holding the officer role for this federation
        /// </summary>
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Officer role this user is holding
        /// </summary>
        [Required]
        [ForeignKey("OfficerRole")]
        public Guid OfficerRoleId { get; set; }

        //TODO - Email of the officer (specific to the office and not the user) in this entity or the OfficerRole entity?
        [Required]
        [MaxLength(512)]
        public string Email { get; set; }
        
        /// <summary>
        /// Date this user assumes the officer role
        /// </summary>
        [Required]
        public DateTime EffectiveStart { get; set; }

        /// <summary>
        /// Date this user's service as the given officer ends
        /// </summary>
        public DateTime EffectiveEnd { get; set; }
    }
}