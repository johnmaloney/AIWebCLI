using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCLI.Core.Contracts
{
    public interface IContext
    {
        /// <summary>
        /// This is the lead indicator on what pipeline is used in the execution of this
        /// command
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// Stores the arguments sent from the console.
        /// </summary>
        List<string> Arguments { get; }

        /// <summary>
        /// Contains Key Value pairs for the pipe to use in decision making
        /// </summary>
        Dictionary<string, object> Options { get; }

        /// <summary>
        /// Used to return information to the UI.
        /// </summary>
        string[] Messages { get; }

        /// <summary>
        /// Inidcate to the client what this response type is, e.g. text, JSON, HTML, etc.
        /// </summary>
        string ResponseType { get; }

        /// <summary>
        /// Represents the generic serializable response from whatever pipeline was executed //
        /// </summary>
        object Response { get; }

        void AddMessage(params string[] messages);
    }
}
