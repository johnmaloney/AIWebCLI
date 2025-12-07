using System.Collections.Generic;
using WebCLI.Core.Models;

namespace WebCLI.Core.Contracts
{
    public interface ICommandDefinitionRepository
    {
        IEnumerable<CommandDefinition> GetAllCommandDefinitions();
        CommandDefinition GetCommandDefinitionByName(string name);
    }
}