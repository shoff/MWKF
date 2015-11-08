using System.Collections.Generic;
using System.Reflection;

namespace AUSKF.Api.Services.Interfaces
{
    public interface IAssemblyDiscoveryService
    {
        /// <summary>
        /// Generates the dependency list.
        /// </summary>
        void GenerateDependencyList();

        /// <summary>
        /// Gets or sets the assembly list.
        /// </summary>
        /// <value>
        /// The assembly list.
        /// </value>
        HashSet<Assembly> AssemblyList { get; set; }
    }
}