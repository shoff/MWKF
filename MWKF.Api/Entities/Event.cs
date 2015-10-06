namespace MWKF.Api.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using MWKF.Api.Entities.Identity;

    public class Event
    {
        [Key]
        public Guid EventId { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime EventDate { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }

        public User CreatedBy { get; set; }

        [Required, ForeignKey("CreatedBy")]
        public Guid Id { get; set; }    

    }
}