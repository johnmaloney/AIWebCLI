using System.Collections.Generic;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;

namespace WebCLI.Core.Pipes
{
    public abstract class APipeContext : IContext
    {
        public IAuthContext UserContext { get; set; }
        public string Identifier { get; }
        public List<string> Arguments { get; }
        public Dictionary<string, object> Options { get; }
        public string[] Messages { get; }
        public string ResponseType { get; }
        public object Response { get; }

        public abstract void AddMessage(params string[] messages);
        // Add other common properties that all pipes might need
    }
}
