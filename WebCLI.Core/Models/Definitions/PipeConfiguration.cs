using System;
using System.Collections.Generic;
using System.Text;

namespace WebCLI.Core.Models.Definitions
{
    public class PipeConfiguration
    {
        public string Name { get; set; } // Added Name property
        public string Type { get; set; }
        public string Assembly { get; set; }
        public string Namespace { get; set; }
        public string ContextType { get; set; }
        public string Description { get; set; }
        public string InputType { get; set; }
        public string OutputType { get; set; }
        public Dictionary<string, string> Parameters { get; set; } // ParameterName -> Type/Description
    }
}