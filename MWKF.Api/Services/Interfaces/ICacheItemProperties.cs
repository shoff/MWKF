﻿using System;
using System.Web.Caching;

namespace AUSKF.Api.Services.Interfaces
{
    public interface ICacheItemProperties
    {
        /// <summary>
        ///   Gets or sets the dependency.
        /// </summary>
        /// <value> The dependency. </value>
        CacheDependency Dependency { get; set; }

        /// <summary>
        ///   Gets or sets the absolute expiration.
        /// </summary>
        /// <value> The absolute expiration. </value>
        DateTime AbsoluteExpiration { get; set; }

        /// <summary>
        ///   Gets or sets the sliding expiration.
        /// </summary>
        /// <value> The sliding expiration. </value>
        TimeSpan SlidingExpiration { get; set; }

        /// <summary>
        ///   Gets or sets the cache priority.
        /// </summary>
        /// <value> The cache priority. </value>
        CacheItemPriority CachePriority { get; set; }

        /// <summary>
        ///   Gets or sets the callback.
        /// </summary>
        /// <value> The callback. </value>
        Delegate Callback { get; set; }
    }
}