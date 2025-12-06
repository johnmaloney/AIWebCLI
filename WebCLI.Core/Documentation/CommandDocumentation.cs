using System.Collections.Generic;

namespace WebCLI.Core.Documentation
{
    public class CommandDocumentation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Parameters { get; set; } = new List<string>();
    }
}