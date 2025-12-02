using System;
using System.Collections.Generic;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Contracts
{
    public interface IPipelineInitializer
    {
        Func<IPipe> PipeInitializer { get; set; }
        Func<string, object, string[], Dictionary<string, object>, IContext> ContextInitializer { get; set; }
    }
}
