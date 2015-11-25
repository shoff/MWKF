using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AUSKF.Api.Entities
{
    public class KendoRank : EntityBase
    {
        /// <summary>
        /// Gets or sets the kendo rank identifier.
        /// </summary>
        /// <value>
        /// The kendo rank identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid KendoRankId  { get; set; }

        /// <summary>
        /// Gets or sets the name of the kendo rank.
        /// </summary>
        /// <value>
        /// The name of the kendo rank.
        /// </value>
        [Required]
        public string KendoRankName { get; set; }

        /// <summary>
        /// Gets or sets the kendo rank numeric.
        /// </summary>
        /// <value>
        /// The kendo rank numeric.
        /// </value>
        [Required]
        public int KendoRankNumeric { get; set; }

        [Required, MaxLength(512)]
        public string Eligibility { get; set; }

        [Required, MaxLength(30)]
        public string MinimumRankOfExaminers { get; set; }

        [Required]
        public int NumberOfExaminers { get; set; }

        [Required]
        public int ConsentingExaminersRequired { get; set; }
    }
}