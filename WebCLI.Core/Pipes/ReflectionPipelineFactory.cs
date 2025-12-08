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
            // This method might become obsolete or simplified if initial contexts are handled differently
            return new GeneralContext(); 
        }

        public IContext CreateInitialCommandContext(ICommand command)
        {
            return new GeneralContext { Command = command, CommandResult = new CommandResult() };
        }

        public IContext CreateInitialQueryContext<TResult>(IQuery<TResult> query)
        {
            return new GeneralContext { Query = query, QueryResult = new QueryResult<TResult>() }; // Assuming QueryResult<TResult> exists
        }
    }
}
