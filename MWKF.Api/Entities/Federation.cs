namespace MWKF.Api.Entities
{ 
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// AUSKF member federation information
    /// </summary>
    public class Federation
    {
        /// <summary>
        /// Federation identifier
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid FederationId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(512)]
        public string Email { get; set; }

        [MaxLength(13), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [MaxLength(512)]
        public string WebsiteUrl { get; set; }
    }
}