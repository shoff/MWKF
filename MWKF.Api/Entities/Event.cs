using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AUSKF.Api.Entities.Identity;

namespace AUSKF.Api.Entities
{
    public class Event : EntityBase
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