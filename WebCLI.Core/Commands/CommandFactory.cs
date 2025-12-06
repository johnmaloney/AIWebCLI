using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebCLI.Core.Documentation;

namespace WebCLI.Core.Commands
{
    public class CommandFactory
    {
        private readonly Dictionary<string, Type> _commands = new Dictionary<string, Type>();

        public CommandFactory()
        {
            RegisterCommandsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public void RegisterCommand<TCommand>() where TCommand : ICommand
        {
            var commandName = Activator.CreateInstance<TCommand>().Name;
            _commands[commandName] = typeof(TCommand);
        }

        public ICommand GetCommand(string commandName)
        {
            if (_commands.TryGetValue(commandName, out var commandType))
            {
                return (ICommand)Activator.CreateInstance(commandType);
            }
            throw new ArgumentException($"Unknown command: {commandName}");
        }

        private void RegisterCommandsFromAssembly(Assembly assembly)
        {
            var commandTypes = assembly.GetTypes()
                .Where(type => typeof(ICommand).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            foreach (var commandType in commandTypes)
            {
                var commandInstance = (ICommand)Activator.CreateInstance(commandType);
                _commands[commandInstance.Name] = commandType;
            }
        }

        public IEnumerable<CommandDocumentation> GetCommandDocumentation()
        {
            foreach (var commandType in _commands.Values)
            {
                var commandInstance = (ICommand)Activator.CreateInstance(commandType);
                var doc = new CommandDocumentation
                {
                    Name = commandInstance.Name,
                    Description = commandInstance.Description
                };

                // For now, we'll hardcode parameters for SqlExecutorCommand.
                // In a real scenario, you might use reflection to find constructor parameters or a custom attribute.
                if (commandInstance.Name == "sql-exec")
                {
                    doc.Parameters.Add("query");
                }
                yield return doc;
            }
        }
    }
}