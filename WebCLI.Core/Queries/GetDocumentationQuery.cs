using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebCLI.Core.Commands;
using WebCLI.Core.Documentation;

namespace WebCLI.Core.Queries
{
    public class GetDocumentationQuery : IQuery
    {
        private readonly CommandFactory _commandFactory;
        private readonly QueryFactory _queryFactory;

        public GetDocumentationQuery(CommandFactory commandFactory, QueryFactory queryFactory)
        {
            _commandFactory = commandFactory;
            _queryFactory = queryFactory;
        }

        public string Name => "get-docs";
        public string Description => "Retrieves documentation for all available commands and queries.";

        public async Task<string> ExecuteAsync(Dictionary<string, string> parameters)
        {
            var documentationBuilder = new StringBuilder();

            documentationBuilder.AppendLine("--- Commands ---");
            foreach (var commandDoc in _commandFactory.GetCommandDocumentation())
            {
                documentationBuilder.AppendLine($"Name: {commandDoc.Name}");
                documentationBuilder.AppendLine($"  Description: {commandDoc.Description}");
                if (commandDoc.Parameters.Any())
                {
                    documentationBuilder.AppendLine($"  Parameters: {string.Join(", ", commandDoc.Parameters)}");
                }
                documentationBuilder.AppendLine();
            }

            documentationBuilder.AppendLine("--- Queries ---");
            foreach (var queryDoc in _queryFactory.GetQueryDocumentation())
            {
                documentationBuilder.AppendLine($"Name: {queryDoc.Name}");
                documentationBuilder.AppendLine($"  Description: {queryDoc.Description}");
                if (queryDoc.Parameters.Any())
                {
                    documentationBuilder.AppendLine($"  Parameters: {string.Join(", ", queryDoc.Parameters)}");
                }
                documentationBuilder.AppendLine();
            }

            return await Task.FromResult(documentationBuilder.ToString());
        }
    }
}