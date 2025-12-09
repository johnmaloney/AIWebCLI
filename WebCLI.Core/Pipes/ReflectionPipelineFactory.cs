using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;
using WebCLI.Core.Models.Definitions;

namespace WebCLI.Core.Pipes
{
    public class ReflectionPipelineFactory : IPipelineFactory
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
            return new GeneralContext { Command = command, CommandResult = new CommandResult(true, "Command initialized.") }; // Fixed CommandResult instantiation
        }

        public IContext CreateInitialQueryContext<TResult>(IQuery<TResult> query)
        {
            // Changed GeneralContext.Query to IQuery<object> in previous step.
            // Here, we can directly assign if we make GeneralContext.Query generic or use IQuery<object> as intended.
            // For now, let's assume GeneralContext.Query becomes IQuery<object> as per the previous todo.
            return new GeneralContext { Query = (IQuery<object>)query, QueryResult = new QueryResult<object>(true, "Query initialized.", null) }; // Fixed type cast and QueryResult instantiation
        }
    }
}
