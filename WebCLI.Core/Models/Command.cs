using System.Collections.Generic;

namespace WebCLI.Core.Models
{
    public class Command : ICommand
    {
        public string Name { get; }
        public Dictionary<string, string> Parameters { get; }

        public Command(string name, Dictionary<string, string> parameters = null)
        {
            Name = name;
            Parameters = parameters ?? new Dictionary<string, string>();
        }
    }
}
