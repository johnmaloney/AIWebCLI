using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebCLI.Core.Contracts;
using WebCLI.Core.Documentation;
using WebCLI.Core.Models;

namespace WebCLI.Core.Commands
{
    public class CommandFactory
    {
        private readonly ICommandDefinitionRepository _commandDefinitionRepository;

        public CommandFactory(ICommandDefinitionRepository commandDefinitionRepository)
        {
            _commandDefinitionRepository = commandDefinitionRepository;
        }

        public ICommand GetCommand(string commandName)
        {
            var definition = _commandDefinitionRepository.GetCommandDefinitionByName(commandName);
            if (definition == null)
            {
                throw new ArgumentException($"Unknown command: {commandName}");
            }

            var commandType = Type.GetType(definition.Type);
            if (commandType == null || !typeof(ICommand).IsAssignableFrom(commandType))
            {
                throw new InvalidOperationException($"Could not load command type for {commandName}: {definition.Type}");
            }
            return (ICommand)Activator.CreateInstance(commandType);
        }

        public IEnumerable<CommandDocumentation> GetCommandDocumentation()
        {
            return _commandDefinitionRepository.GetAllCommandDefinitions().Select(definition => new CommandDocumentation
            {
                Name = definition.Name,
                Description = definition.Description,
                Parameters = definition.Parameters
            });
        }
    }
}