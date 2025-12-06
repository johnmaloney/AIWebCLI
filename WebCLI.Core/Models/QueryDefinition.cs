using System.Collections.Generic;

namespace WebCLI.Core.Models
{
    public class QueryDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Parameters { get; set; } = new List<string>();
        public string Type { get; set; }
    }
}