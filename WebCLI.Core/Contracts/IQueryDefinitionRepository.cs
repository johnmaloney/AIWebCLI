using System.Collections.Generic;
using WebCLI.Core.Models;

namespace WebCLI.Core.Contracts
{
    public interface IQueryDefinitionRepository
    {
        IEnumerable<QueryDefinition> GetAllQueryDefinitions();
        QueryDefinition GetQueryDefinitionByName(string name);
    }
}