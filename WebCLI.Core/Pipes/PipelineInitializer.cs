using System;
using System.Collections.Generic;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Pipes
{
    public class PipelineInitializer : IPipelineInitializer
    {
        public Func<IPipe> PipeInitializer { get; set; }
        public Func<string, object, string[], Dictionary<string, object>, IContext> ContextInitializer { get; set; }
    }
}
