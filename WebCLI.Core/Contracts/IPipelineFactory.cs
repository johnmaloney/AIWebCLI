using WebCLI.Core.Models;
using WebCLI.Core.Models.Definitions;

namespace WebCLI.Core.Contracts
{
    public interface IPipelineFactory
    {
        IPipe CreatePipe(PipeConfiguration pipeConfiguration);
        IContext CreatePipeContext(PipeConfiguration pipeConfiguration);
        IContext CreateInitialCommandContext(ICommand command); // New method
        IContext CreateInitialQueryContext<TResult>(IQuery<TResult> query); // New method
    }
}
