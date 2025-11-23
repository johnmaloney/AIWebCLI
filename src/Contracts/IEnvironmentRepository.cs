using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCLI.Core.Contracts
{
    public interface IEnvironmentRepository
    {
        /// <summary>
        /// Stores the default context name for the auth credentials
        /// </summary>
        string DefaultContext { get; set; }

        /// <summary>
        /// This is the containing folder for the *settings.json files.
        /// </summary>
        string RootDirectory { get; set; }

        /// <summary>
        /// Individual Authorization contexts related to an individuals account.
        /// </summary>
        Dictionary<string, IAuthContext> AuthorizationContexts { get; }

        /// <summary>
        /// Environmental variables related to each specific servers urls. 
        /// </summary>
        IEnvironmentInfo this[string environment] { get; }

        /// <summary>
        /// Returns the available environment names.
        /// </summary>
        IEnumerable<string> AvailableEnvironments { get; }

        IAuthContext GetDefaultAuthContext();

        void AddEnvironment(string environmentName, IEnvironmentInfo environment);
    }
}
