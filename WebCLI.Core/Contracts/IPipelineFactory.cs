using WebCLI.Core.Models;
using WebCLI.Core.Models.Definitions;

namespace WebCLI.Core.Contracts
{
    public interface IPipelineFactory
    {
        IPipe CreatePipe(PipeDefinition pipeDefinition);
        IContext CreatePipeContext(PipeDefinition pipeDefinition);
        IContext CreateInitialCommandContext(ICommand command); // New method
        IContext CreateInitialQueryContext<TResult>(IQuery<TResult> query); // New method
    }
}
