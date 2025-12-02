using System.Threading.Tasks;
using WebCLI.Core.Models;

namespace WebCLI.Core.Contracts
{
    public interface IPipelineInitializer
    {
        Task<ICommandResult> ExecuteCommandPipeline(Command command);
        Task<IQueryResult> ExecuteQueryPipeline(Query query);
    }
}