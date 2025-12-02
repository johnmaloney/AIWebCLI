using System.Collections.Generic;

namespace WebCLI.Core.Models.Definitions
{
    public class PipelineDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // "Command" or "Query"
        public List<PipeConfiguration> Pipes { get; set; }
        public Dictionary<string, string> Parameters { get; set; } // For documentation and help files
        public string ResultType { get; set; } // Expected result type for documentation
    }
}