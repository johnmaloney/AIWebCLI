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
        // Add any additional configuration properties needed for a pipe here
    }
}