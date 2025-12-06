using System.Collections.Generic;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Models
{
    public class Command : ICommandCriteria
    {
        public string Name { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public IAuthContext UserContext { get; set; }

        public Command(string name, Dictionary<string, string> options = null, IAuthContext userContext = null)
        {
            Name = name;
            Options = options ?? new Dictionary<string, string>();
            UserContext = userContext;
        }
    }
}
