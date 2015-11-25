using AUSKF.Api.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUSKF.Api.Entities
{
    [Table("Dojos")]
    public class Dojo : EntityBase 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DojoId { get; set; }
        
        [ForeignKey("Federation")] 
        public Guid FederationId { get; set; }

        public Federation Federation { get; set; }

        [ForeignKey("Address")]
        public Guid AddressId { get; set; }

        public Address Address { get; set; }

        [ForeignKey("PrimaryContact")]
        public Guid PrimaryContactId { get; set; }

        public User PrimaryContact { get; set; }

        [Required]
        public string DojoName { get; set; }

        [MaxLength(13), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [MaxLength(512)]
        public string WebsiteUrl { get; set; }

        // TODO - NEVER expose an email address on the internet! 
        // TODO - We'll create a contact form and send a request on behalf
        [MaxLength(512)]
        public string EmailAddress { get; set; }

        [MaxLength(1024)]
        public string Notes { get; set; }
    }
}