using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebCLI.Core.Contracts;
using WebCLI.Core.Documentation;
using WebCLI.Core.Models;
using WebCLI.Core.Commands;
using WebCLI.Core.Queries;

namespace WebCLI.Core.Queries
{
    public class QueryFactory
    {
        private readonly IQueryDefinitionRepository _queryDefinitionRepository;
        private readonly ICommandDefinitionRepository _commandDefinitionRepository; // Added for GetDocumentationQuery

        public QueryFactory(IQueryDefinitionRepository queryDefinitionRepository, ICommandDefinitionRepository commandDefinitionRepository)
        {
            _queryDefinitionRepository = queryDefinitionRepository;
            _commandDefinitionRepository = commandDefinitionRepository; // Injected
        }

        public IQuery GetQuery(string queryName)
        {
            var definition = _queryDefinitionRepository.GetQueryDefinitionByName(queryName);
            if (definition == null)
            {
                throw new ArgumentException($"Unknown query: {queryName}");
            }

            var queryType = Type.GetType(definition.Type);
            if (queryType == null || !typeof(IQuery).IsAssignableFrom(queryType))
            {
                throw new InvalidOperationException($"Could not load query type for {queryName}: {definition.Type}");
            }

            // Special handling for GetDocumentationQuery as it has dependencies
            if (queryType == typeof(GetDocumentationQuery))
            {
                return new GetDocumentationQuery(
                    new Commands.CommandFactory(_commandDefinitionRepository), // Pass the repository
                    this);
            }

            return (IQuery)Activator.CreateInstance(queryType);
        }

        public IEnumerable<QueryDocumentation> GetQueryDocumentation()
        {
            return _queryDefinitionRepository.GetAllQueryDefinitions().Select(definition => new QueryDocumentation
            {
                Name = definition.Name,
                Description = definition.Description,
                Parameters = definition.Parameters
            });
        }
    }
}