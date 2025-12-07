using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebCLI.Core.Commands;
using WebCLI.Core.Contracts;
using WebCLI.Core.Documentation;
using WebCLI.Core.Queries;

namespace WebCLI.Core.Queries
{
    public class GetDocumentationQuery : IQuery
    {
        private readonly ICommandDefinitionRepository _commandDefinitionRepository;
        private readonly IQueryDefinitionRepository _queryDefinitionRepository;

        public GetDocumentationQuery(ICommandDefinitionRepository commandDefinitionRepository, IQueryDefinitionRepository queryDefinitionRepository)
        {
            _commandDefinitionRepository = commandDefinitionRepository;
            _queryDefinitionRepository = queryDefinitionRepository;
        }

        public string Name => "get-docs";
        public string Description => "Retrieves documentation for all available commands and queries.";

        public async Task<string> ExecuteAsync(Dictionary<string, string> parameters)
        {
            var documentationBuilder = new StringBuilder();

            documentationBuilder.AppendLine("--- Commands ---");
            foreach (var commandDef in _commandDefinitionRepository.GetAllCommandDefinitions())
            {
                documentationBuilder.AppendLine($"Name: {commandDef.Name}");
                documentationBuilder.AppendLine($"  Description: {commandDef.Description}");
                if (commandDef.Parameters.Any())
                {
                    documentationBuilder.AppendLine($"  Parameters: {string.Join(", ", commandDef.Parameters)}");
                }
                documentationBuilder.AppendLine();
            }

            documentationBuilder.AppendLine("--- Queries ---");
            foreach (var queryDef in _queryDefinitionRepository.GetAllQueryDefinitions())
            {
                documentationBuilder.AppendLine($"Name: {queryDef.Name}");
                documentationBuilder.AppendLine($"  Description: {queryDef.Description}");
                if (queryDef.Parameters.Any())
                {
                    documentationBuilder.AppendLine($"  Parameters: {string.Join(", ", queryDef.Parameters)}");
                }
                documentationBuilder.AppendLine();
            }

            return await Task.FromResult(documentationBuilder.ToString());
        }
    }
}