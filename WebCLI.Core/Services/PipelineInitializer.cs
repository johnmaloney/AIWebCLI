using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;
using WebCLI.Core.Pipes;

namespace WebCLI.Core.Services
{
    public class PipelineInitializer : IPipelineInitializer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<string, PipelineDefinition> _pipelineDefinitions;
        private readonly ConcurrentDictionary<string, Func<ICommand, Task<CommandResult>>> _commandPipelines;
        private readonly ConcurrentDictionary<string, Func<IQuery, Task<object>>> _queryPipelines;

        public PipelineInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _pipelineDefinitions = new ConcurrentDictionary<string, PipelineDefinition>();
            _commandPipelines = new ConcurrentDictionary<string, Func<ICommand, Task<CommandResult>>>();
            _queryPipelines = new ConcurrentDictionary<string, Func<IQuery, Task<object>>>();
            InitializePipelines();
        }

        private void InitializePipelines()
        {
            var commandDefinitions = GetCommandDefinitions();
            foreach (var definition in commandDefinitions)
            {
                _pipelineDefinitions.TryAdd(definition.Name, definition);
                _commandPipelines.TryAdd(definition.Name, CreateCommandPipelineDelegate(definition));
            }

            var queryDefinitions = GetQueryDefinitions();
            foreach (var definition in queryDefinitions)
            {
                _pipelineDefinitions.TryAdd(definition.Name, definition);
                _queryPipelines.TryAdd(definition.Name, CreateQueryPipelineDelegate(definition));
            }
        }

        private IEnumerable<PipelineDefinition> GetCommandDefinitions()
        {
            var commandDefinitionType = typeof(ICommandDefinition);
            var commandDefinitions = Assembly.GetAssembly(commandDefinitionType)
                                             .GetTypes()
                                             .Where(t => commandDefinitionType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                                             .Select(Activator.CreateInstance)
                                             .Cast<ICommandDefinition>();

            foreach (var cmdDef in commandDefinitions)
            {
                var definition = new PipelineDefinition
                {
                    Name = cmdDef.CommandName,
                    Description = cmdDef.Description,
                    Type = PipelineType.Command,
                    Pipes = new List<PipeDefinition>()
                };

                // Add an initial pipe for demonstration/structure
                definition.Pipes.Add(new PipeDefinition
                {
                    Name = $"{cmdDef.CommandName} Pipe",
                    Description = $"Executes the {cmdDef.CommandName} command.",
                    InputType = typeof(ICommand),
                    OutputType = typeof(CommandResult),
                    Parameters = new Dictionary<string, object>
                    {
                        { "CommandName", cmdDef.CommandName },
                        { "ExpectedParameters", cmdDef.ExpectedParameters }
                    }
                });
                yield return definition;
            }
        }

        private IEnumerable<PipelineDefinition> GetQueryDefinitions()
        {
            var queryDefinitionType = typeof(IQueryDefinition);
            var queryDefinitions = Assembly.GetAssembly(queryDefinitionType)
                                           .GetTypes()
                                           .Where(t => queryDefinitionType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                                           .Select(Activator.CreateInstance)
                                           .Cast<IQueryDefinition>();

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
                    InputType = typeof(IQuery),
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

        private Func<ICommand, Task<CommandResult>> CreateCommandPipelineDelegate(PipelineDefinition definition)
        {
            // Simplified for now, actual pipe execution logic would go here
            return async (command) =>
            {
                // In a real scenario, this would chain pipes dynamically
                await Task.Delay(10); // Simulate async work
                return new CommandResult
                {
                    Success = true,
                    Messages = { $"Command '{command.Name}' executed successfully." },
                    Response = $"Response for {command.Name}"
                };
            };
        }

        private Func<IQuery, Task<object>> CreateQueryPipelineDelegate(PipelineDefinition definition)
        {
            // Simplified for now, actual pipe execution logic would go here
            return async (query) =>
            {
                await Task.Delay(10); // Simulate async work
                return $"Query '{query.Name}' executed successfully. Result type: {definition.Pipes.FirstOrDefault()?.OutputType?.Name}";
            };
        }

        public async Task<CommandResult> ExecuteCommandPipeline(ICommand command)
        {
            if (_commandPipelines.TryGetValue(command.Name, out var pipelineDelegate))
            {
                return await pipelineDelegate(command);
            }
            return new CommandResult
            {
                Success = false,
                Messages = { $"Command '{command.Name}' not found." }
            };
        }

        public async Task<TResult> ExecuteQueryPipeline<TResult>(IQuery<TResult> query)
        {
            if (_queryPipelines.TryGetValue(query.Name, out var pipelineDelegate))
            {
                var result = await pipelineDelegate(query);
                if (result is TResult typedResult)
                {
                    return typedResult;
                }
                // Handle type mismatch or conversion error
                throw new InvalidCastException($"Expected result of type {typeof(TResult).Name}, but got {result?.GetType().Name ?? "null"}.");
            }
            throw new InvalidOperationException($"Query '{query.Name}' not found.");
        }

        public IEnumerable<PipelineDefinition> GetAllPipelineDefinitions()
        {
            return _pipelineDefinitions.Values;
        }
    }
}
