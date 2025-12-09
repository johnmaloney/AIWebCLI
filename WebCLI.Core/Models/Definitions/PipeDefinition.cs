using System.Collections.Generic;

namespace WebCLI.Core.Models.Definitions
{
    public class PipeDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string InputType { get; set; }
        public string OutputType { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}