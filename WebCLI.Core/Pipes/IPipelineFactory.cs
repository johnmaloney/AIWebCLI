using WebCLI.Core.Contracts;
using WebCLI.Core.Models.Definitions;

namespace WebCLI.Core.Pipes
{
    public interface IPipelineFactory
    {
        IPipe CreatePipe(PipeConfiguration pipeConfiguration);
        IContext CreatePipeContext(PipeConfiguration pipeConfiguration);
    }
}
