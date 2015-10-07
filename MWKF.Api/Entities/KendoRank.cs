namespace MWKF.Api.Entities
{
    using System;

    public class KendoRank
    {
        /// <summary>
        /// Gets or sets the kendo rank identifier.
        /// </summary>
        /// <value>
        /// The kendo rank identifier.
        /// </value>
        public Guid KendoRankId  { get; set; }

        /// <summary>
        /// Gets or sets the name of the kendo rank.
        /// </summary>
        /// <value>
        /// The name of the kendo rank.
        /// </value>
        public string KendoRankName { get; set; }

        /// <summary>
        /// Gets or sets the kendo rank numeric.
        /// </summary>
        /// <value>
        /// The kendo rank numeric.
        /// </value>
        public int KendoRankNumeric { get; set; }
    }
}