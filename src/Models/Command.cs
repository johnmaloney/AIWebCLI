using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCLI.Core.Models
{
    public class Command
    {
        public string Name { get; set; }
        public string Directive { get; set; }
        public string Descriptor { get; set; }
        public string CommandPipeType { get; set; }
    }
}
