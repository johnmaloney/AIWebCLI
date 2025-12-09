using System.Collections.Generic;

namespace WebCLI.Core.Models
{
    public class Command : ICommand
    {
        public string Name { get; }
        public Dictionary<string, string> Parameters { get; }
        public Dictionary<string, string> Options { get; }

        public Command(string name, Dictionary<string, string> parameters = null, Dictionary<string, string> options = null)
        {
            Name = name;
            Parameters = parameters ?? new Dictionary<string, string>();
            Options = options ?? new Dictionary<string, string>();
        }
    }
}
