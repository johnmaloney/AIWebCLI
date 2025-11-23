

using System.Collections.Generic;
using System.Linq;
using WCLI.Core.Contracts;

namespace WebCLI.Core.Pipes
{
    public class 
        GeneralContext : APipeContext//, IEnvironmentalContext
    {
        public string Input { get; set; }

        public GeneralContext(IEnvironmentRepository environments, Dictionary<string, object> options, params string[] arguments)
        {
            this.Arguments = arguments.ToList();
            this.Environments = environments;
            this.Options = options;
        }
    }
}