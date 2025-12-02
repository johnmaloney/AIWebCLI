using System.Collections.Generic;

namespace WebCLI.Core.Contracts
{
    public interface ICommandCriteria : ICriteria
    {
        public string CommandDirective { get; set; }
        public string[] Arguments{ get; set; }
        public Dictionary<string, object> Options { get; set; }
    }
}