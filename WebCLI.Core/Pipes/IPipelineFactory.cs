using WebCLI.Core.Models.Definitions;

namespace WebCLI.Core.Pipes
{
    public interface IPipelineFactory
    {
        IPipe CreatePipe(PipeConfiguration pipeConfiguration);
        IPipeContext CreatePipeContext(PipeConfiguration pipeConfiguration);
    }
}
