namespace MWKF.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AddressId { get; set; }

        [MaxLength(256)]
        public string AddressLine1 { get; set; }
        
        public string AddressLine2 { get; set; }

        [MaxLength(256)]
        public string City { get; set; }

        [MaxLength(60)]
        public string State { get; set; }

        [MaxLength(10)]
        public string ZipCode { get; set; }

    }
}