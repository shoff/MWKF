namespace MWKF.Api.Entities
{
    using Identity;
    using System; 
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema; 

    public class DojoMembership
    {
        /// <summary>
        /// Dojo membership identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DojoMembershipId { get; set; }

        /// <summary>
        /// User who's membership to the dojo is defined.
        /// </summary>
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Dojo this user is a member of
        /// </summary>
        [Required]
        [ForeignKey("Dojo")]
        public Guid DojoId { get; set; }

        /// <summary>
        /// Date the user's membership takes effect.
        /// </summary>
        [Required]
        public DateTime EffectiveStart { get; set; }

        /// <summary>
        /// Date the user's membership ended.
        /// </summary>
        public DateTime EffectiveEnd { get; set; }
    }
}