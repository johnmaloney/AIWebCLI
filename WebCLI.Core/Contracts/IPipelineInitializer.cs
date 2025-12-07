using WebCLI.Core.Models;

namespace WebCLI.Core.Contracts
{
    public interface IPipelineInitializer
    {
        Task<CommandResult> ExecuteCommandPipeline(ICommand command);
        Task<TResult> ExecuteQueryPipeline<TResult> (IQuery<TResult> query);
        IEnumerable<PipelineDefinition> GetAllPipelineDefinitions(); // New method
    }
}
