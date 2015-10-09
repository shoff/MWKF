namespace MWKF.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Dojo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DojoId { get; set; }

        [Required]
        public string DojoName { get; set; }

        [ForeignKey("Address")]
        public Guid AddressId { get; set; }

        public Address Address { get; set; }

        [Required]
        public string Contact { get; set; }

        // TODO - NEVER expose an email address on the internet! 
        // TODO - We'll create a contact form and send a request on behalf
        [MaxLength(512)]
        public string EmailAddress { get; set; }
    }
}