using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;
using WebCLI.Core.Pipes;
using WebCLI.Core.Models.Definitions;

namespace WebCLI.Core.Services
{
    public class PipelineInitializer : IPipelineInitializer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<string, PipelineDefinition> _pipelineDefinitions;
        private readonly ConcurrentDictionary<string, Func<ICommand, Task<CommandResult>>> _commandPipelines;
        private readonly ConcurrentDictionary<string, Func<IQuery<object>, Task<object>>> _queryPipelines; // Change here

        public PipelineInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _pipelineDefinitions = new ConcurrentDictionary<string, PipelineDefinition>();
            _commandPipelines = new ConcurrentDictionary<string, Func<ICommand, Task<CommandResult>>>();
            _queryPipelines = new ConcurrentDictionary<string, Func<IQuery<object>, Task<object>>>(); // Change here
            InitializePipelines();
        }

        // ... other methods ...

        private IEnumerable<PipelineDefinition> GetQueryDefinitions()
        {
            // ... existing code ...

            foreach (var queryDef in queryDefinitions)
            {
                var definition = new PipelineDefinition
                {
                    Name = queryDef.QueryName,
                    Description = queryDef.Description,
                    Type = PipelineType.Query,
                    Pipes = new List<PipeDefinition>()
                };

                definition.Pipes.Add(new PipeDefinition
                {
                    Name = $"{queryDef.QueryName} Pipe",
                    Description = $"Executes the {queryDef.QueryName} query.",
                    InputType = typeof(IQuery<object>), // Change here
                    OutputType = queryDef.ResultType, // Use the ResultType from the query definition
                    Parameters = new Dictionary<string, object>
                    {
                        { "QueryName", queryDef.QueryName },
                        { "ExpectedParameters", queryDef.ExpectedParameters }
                    }
                });
                yield return definition;
            }
        }

        // ... other methods ...

        private Func<IQuery<object>, Task<object>> CreateQueryPipelineDelegate(PipelineDefinition definition) // Change here
        {
            // Simplified for now, actual pipe execution logic would go here
            return async (query) =>
            {
                await Task.Delay(10); // Simulate async work
                return $"Query '{query.Name}' executed successfully. Result type: {definition.Pipes.FirstOrDefault()?.OutputType?.Name}";
            };
        }

        // ... rest of the file ...
    }
}
