using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebCLI.Core.Contracts; // Ensure this is present and correct
using WebCLI.Core.Models;
using WebCLI.Core.Models.Definitions;
// using WebCLI.Core.Pipes; // This using is no longer needed for IPipelineFactory

namespace WebCLI.Core.Pipes
{
    public class ReflectionPipelineFactory : WebCLI.Core.Contracts.IPipelineFactory // Explicitly implement Contracts.IPipelineFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ReflectionPipelineFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPipe CreatePipe(PipeConfiguration pipeConfiguration)
        {
            var pipeType = Assembly.GetExecutingAssembly()
                                   .GetTypes()
                                   .FirstOrDefault(t => t.Name == pipeConfiguration.Name && typeof(IPipe).IsAssignableFrom(t));

            if (pipeType == null)
            {
                throw new InvalidOperationException($"Pipe type '{pipeConfiguration.Name}' not found.");
            }

            return (IPipe)_serviceProvider.GetService(pipeType);
        }

        public IContext CreatePipeContext(PipeConfiguration pipeConfiguration)
        {
            return new GeneralContext(); 
        }

        public IContext CreateInitialCommandContext(ICommand command)
        {
            return new GeneralContext { Command = command, CommandResult = new CommandResult(true, "Command initialized.") }; 
        }

        public IContext CreateInitialQueryContext<TResult>(IQuery<TResult> query)
        {
            return new GeneralContext { Query = (IQuery<object>)query, QueryResult = new QueryResult<object>(true, "Query initialized.", null) }; 
        }
    }
}
