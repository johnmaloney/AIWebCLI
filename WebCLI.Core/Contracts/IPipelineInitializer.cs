using WebCLI.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks; // Add this line
using WebCLI.Core.Models.Definitions; // Add this line

namespace WebCLI.Core.Contracts
{
    public interface IPipelineInitializer
    {
        Task<CommandResult> ExecuteCommandPipeline(ICommand command);
        Task<TResult> ExecuteQueryPipeline<TResult>(IQuery<TResult> query);
        IEnumerable<PipelineDefinition> GetAllPipelineDefinitions();
    }
}
