using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic; // Added for IEnumerable
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;
using WebCLI.Core.Models.Definitions; // Added
using WebCLI.Core.Pipes;

namespace WebCLI.Core.Pipes
{
    public class PipelineInitializer : IPipelineInitializer
    {
        private readonly IPipelineDefinitionRepository _pipelineDefinitionRepository;
        private readonly IPipelineFactory _pipelineFactory;

        public PipelineInitializer(
            IPipelineDefinitionRepository pipelineDefinitionRepository,
            IPipelineFactory pipelineFactory)
        {
            _pipelineDefinitionRepository = pipelineDefinitionRepository;
            _pipelineFactory = pipelineFactory;
        }

        public async Task<CommandResult> ExecuteCommandPipeline(ICommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var pipelineDefinition = _pipelineDefinitionRepository.GetPipelineDefinition(command.Name);
            if (pipelineDefinition == null)
            {
                return new CommandResult(false, $"Command pipeline '{command.Name}' not found.");
            }

            // Assuming PipelineDefinition.Type is a string. If it's an enum, adjust accordingly.
            if (pipelineDefinition.Type != "Command")
            {
                return new CommandResult(false, $"Pipeline '{command.Name}' is not a command pipeline.");
            }

            return await BuildAndExecuteCommandPipeline(pipelineDefinition, command);
        }

        public async Task<TResult> ExecuteQueryPipeline<TResult>(IQuery<TResult> query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var pipelineDefinition = _pipelineDefinitionRepository.GetPipelineDefinition(query.Name);
            if (pipelineDefinition == null)
            {
                throw new InvalidOperationException($"Query pipeline '{query.Name}' not found.");
            }

            // Assuming PipelineDefinition.Type is a string. If it's an enum, adjust accordingly.
            if (pipelineDefinition.Type != "Query")
            {
                throw new InvalidOperationException($"Pipeline '{query.Name}' is not a query pipeline.");
            }
            
            // This now returns TResult directly, handling the casting internally
            return await BuildAndExecuteQueryPipeline<TResult>(pipelineDefinition, query);
        }

        private async Task<CommandResult> BuildAndExecuteCommandPipeline(PipelineDefinition definition, ICommand command)
        {
            if (!definition.Pipes.Any()) return new CommandResult(true, "Command pipeline executed with no pipes.");

            IPipe firstPipe = null;
            IPipe currentPipe = null;
            IContext initialContext = _pipelineFactory.CreateInitialCommandContext(command); // Use a specific factory method

            foreach (var pipeConfig in definition.Pipes)
            {
                var pipe = _pipelineFactory.CreatePipe(pipeConfig);
                // The context should be passed and transformed by pipes, not created per pipe here

                if (firstPipe == null)
                {
                    firstPipe = pipe;
                }
                else
                {
                    currentPipe.ExtendWith(pipe);
                }
                currentPipe = pipe;
            }
            
            await firstPipe.Process(initialContext);

            // Assuming the CommandResult is now available in the context
            if (initialContext is ICommandResult commandResult)
            {
                return (CommandResult)commandResult; // Cast to concrete type
            }
            return new CommandResult(false, "Command pipeline did not return a valid result.");
        }

         private async Task<TResult> BuildAndExecuteQueryPipeline<TResult>(PipelineDefinition definition, IQuery<TResult> query)
        {
            if (!definition.Pipes.Any()) throw new InvalidOperationException("Query pipeline executed with no pipes.");

            IPipe firstPipe = null;
            IPipe currentPipe = null;
            IContext initialContext = _pipelineFactory.CreateInitialQueryContext(query); // Use a specific factory method

            foreach (var pipeConfig in definition.Pipes)
            {
                var pipe = _pipelineFactory.CreatePipe(pipeConfig);
                
                if (firstPipe == null)
                {
                    firstPipe = pipe;
                }
                else
                {
                    currentPipe.ExtendWith(pipe);
                };
                currentPipe = pipe;
            }

            await firstPipe.Process(initialContext);

            // Assuming the TResult is now available in the context
            if (initialContext is IQueryResult queryResult && queryResult.Data is TResult result)
            {
                return result;
            }
            throw new InvalidCastException($"Expected query result of type {typeof(TResult).Name}, but got unexpected result.");
        }

        public IEnumerable<PipelineDefinition> GetAllPipelineDefinitions()
        {
            // This will retrieve all definitions from the repository
            return _pipelineDefinitionRepository.GetAllPipelineDefinitions();
        }
    }
}