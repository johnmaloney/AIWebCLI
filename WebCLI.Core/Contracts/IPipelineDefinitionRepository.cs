using System.Collections.Generic;
using WebCLI.Core.Models.Definitions;

namespace WebCLI.Core.Contracts
{
    public interface IPipelineDefinitionRepository
    {
        IEnumerable<PipelineDefinition> GetAllPipelineDefinitions();
        PipelineDefinition GetPipelineDefinition(string name);
    }
}