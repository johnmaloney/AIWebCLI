using System;
using System.Linq;
using System.Threading.Tasks;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;
using WebCLI.Core.Models.Definitions;

namespace WebCLI.Core.Pipes
{
    public class DynamicPipelineInitializer : IPipelineInitializer
    {
        private readonly IPipelineDefinitionRepository _pipelineDefinitionRepository;
        private readonly IPipelineFactory _pipelineFactory;

        public DynamicPipelineInitializer(
            IPipelineDefinitionRepository pipelineDefinitionRepository,
            IPipelineFactory pipelineFactory)
        {
            _pipelineDefinitionRepository = pipelineDefinitionRepository;
            _pipelineFactory = pipelineFactory;
        }

        public async Task<ICommandResult> ExecuteCommandPipeline(Command command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            var pipelineDefinition = _pipelineDefinitionRepository.GetPipelineDefinition(command.Name);
            if (pipelineDefinition == null)
            {
                return new CommandResult(false, $"Command pipeline '{command.Name}' not found.");
            }

            if (pipelineDefinition.Type != "Command")
            {
                return new CommandResult(false, $"Pipeline '{command.Name}' is not a command pipeline.");
            }

            return await BuildAndExecutePipeline(pipelineDefinition, command);
        }

        public async Task<IQueryResult> ExecuteQueryPipeline(Query query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var pipelineDefinition = _pipelineDefinitionRepository.GetPipelineDefinition(query.Name);
            if (pipelineDefinition == null)
            {
                return new QueryResult(false, $"Query pipeline '{query.Name}' not found.", null);
            }

            if (pipelineDefinition.Type != "Query")
            {
                return new QueryResult(false, $"Pipeline '{query.Name}' is not a query pipeline.", null);
            }
            
            return (IQueryResult)await BuildAndExecutePipeline(pipelineDefinition, query); // Cast is safe because we check Type above
        }

        private async Task<ICommandResult> BuildAndExecutePipeline(PipelineDefinition definition, Command command)
        {
            if (!definition.Pipes.Any()) return new CommandResult(true, "Command pipeline executed with no pipes.");

            APipe firstPipe = null;
            APipe currentPipe = null;
            IContext initialContext = null;

            foreach (var pipeConfig in definition.Pipes)
            {
                var pipe = (APipe)_pipelineFactory.CreatePipe(pipeConfig);
                var context = _pipelineFactory.CreatePipeContext(pipeConfig);

                if (firstPipe == null)
                {
                    firstPipe = pipe;
                    initialContext = context;
                }
                else
                {
                    currentPipe.SetNext(pipe);
                }
                currentPipe = pipe;
            }
            
            // Assuming the initial context needs to be populated with command/query data
            // This part needs refinement based on how APipeContext will be used and populated
            if (initialContext is GeneralContext generalContext)
            {
                generalContext.Command = command; // Example: Populating the command
                // You might need to add properties to GeneralContext or create specific contexts
            }

            var result = await firstPipe.Handle(initialContext);

            return result as ICommandResult; // Assuming all pipes return ICommandResult or a derivative
        }

         private async Task<IQueryResult> BuildAndExecutePipeline(PipelineDefinition definition, Query query)
        {
            if (!definition.Pipes.Any()) return new QueryResult(true, "Query pipeline executed with no pipes.", null);

            APipe firstPipe = null;
            APipe currentPipe = null;
            IContext initialContext = null;

            foreach (var pipeConfig in definition.Pipes)
            {
                var pipe = (APipe)_pipelineFactory.CreatePipe(pipeConfig);
                var context = _pipelineFactory.CreatePipeContext(pipeConfig);

                if (firstPipe == null)
                {
                    firstPipe = pipe;
                    initialContext = context;
                }
                else
                {
                    currentPipe.SetNext(pipe);
                };
                currentPipe = pipe;
            }

            // Assuming the initial context needs to be populated with command/query data
            // This part needs refinement based on how APipeContext will be used and populated
            if (initialContext is GeneralContext generalContext)
            {
                generalContext.Query = query; // Example: Populating the query
                // You might need to add properties to GeneralContext or create specific contexts
            }

            var result = await firstPipe.Handle(initialContext);

            return result as IQueryResult; // Assuming all pipes return IQueryResult or a derivative
        }
    }
}