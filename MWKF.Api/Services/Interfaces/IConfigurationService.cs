namespace MWKF.Api.Services.Interfaces
{
    using System.Collections.Specialized;
    using System.Configuration;

    public interface IConfigurationService  
    {
        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <value>
        /// The application settings.
        /// </value>
        NameValueCollection AppSettings { get; }
        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        /// <value>
        /// The connection strings.
        /// </value>
        ConnectionStringSettingsCollection ConnectionStrings { get; }
        /// <summary>
        /// Gets the section.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns></returns>
        object GetSection(string sectionName);
        /// <summary>
        /// Opens the executable configuration.
        /// </summary>
        /// <param name="userLevel">The user level.</param>
        /// <returns></returns>
        Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel);
        /// <summary>
        /// Opens the machine configuration.
        /// </summary>
        /// <returns></returns>
        Configuration OpenMachineConfiguration();
        /// <summary>
        /// Refreshes the section.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        void RefreshSection(string sectionName);
    }
}