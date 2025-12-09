using WebCLI.Core.Models;
using WebCLI.Core.Models.Definitions;

namespace WebCLI.Core.Contracts
{
    public interface IPipelineFactory
    {
        IPipe CreatePipe(PipeConfiguration pipeConfiguration);
        IContext CreatePipeContext(PipeConfiguration pipeConfiguration);
        IContext CreateInitialCommandContext(ICommand command); // This method should be present
        IContext CreateInitialQueryContext<TResult>(IQuery<TResult> query); // This method should be present
    }
}
