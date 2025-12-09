using System.Collections.Generic;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Models
{
    public class Command : ICommand
    {
        public string Name { get; }
        public Dictionary<string, string> Parameters { get; }
        public Dictionary<string, string> Options { get; } // Corrected type
        public IUserContext UserContext { get; }

        public Command(string name,
                       Dictionary<string, string> parameters = null,
                       Dictionary<string, string> options = null,
                       IUserContext userContext = null)
        {
            Name = name;
            Parameters = parameters ?? new Dictionary<string, string>();
            Options = options ?? new Dictionary<string, string>();
            UserContext = userContext;
        }
    }
}
