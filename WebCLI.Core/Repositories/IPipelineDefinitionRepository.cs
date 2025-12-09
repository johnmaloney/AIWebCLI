using WebCLI.Core.Models.Definitions;
using WebCLI.Core.Models;
using System.Collections.Generic;

namespace WebCLI.Core.Repositories
{
    public interface IPipelineDefinitionRepository
    {
        PipelineDefinition GetPipelineDefinition(string name);
        IEnumerable<PipelineDefinition> GetAllPipelineDefinitions(); // New method
    }
}
