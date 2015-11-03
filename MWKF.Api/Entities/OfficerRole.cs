namespace MWKF.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines the roles of various officers within federations (President, Secretary, Treasurer, etc for example)
    /// </summary>
    public class OfficerRole
    {
        /// <summary>
        /// Officer role identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OfficerRoleId { get; set; }

        [Required]
        public string Name { get; set; }

    }
}