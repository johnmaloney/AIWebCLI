using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebCLI.Core.Documentation;

namespace WebCLI.Core.Queries
{
    public class QueryFactory
    {
        private readonly Dictionary<string, Type> _queries = new Dictionary<string, Type>();

        public QueryFactory()
        {
            RegisterQueriesFromAssembly(Assembly.GetExecutingAssembly());
        }

        public void RegisterQuery<TQuery>() where TQuery : IQuery
        {
            var queryName = Activator.CreateInstance<TQuery>().Name;
            _queries[queryName] = typeof(TQuery);
        }

        public IQuery GetQuery(string queryName)
        {
            if (_queries.TryGetValue(queryName, out var queryType))
            {
                // Need to handle constructor injection for GetDocumentationQuery
                if (queryType == typeof(GetDocumentationQuery))
                {
                    // This is a simplified approach. In a real application, you'd use a DI container.
                    return new GetDocumentationQuery(
                        new Commands.CommandFactory(), // This will create new instances, which might not be desired for singletons
                        this);
                }
                return (IQuery)Activator.CreateInstance(queryType);
            }
            throw new ArgumentException($"Unknown query: {queryName}");
        }

        private void RegisterQueriesFromAssembly(Assembly assembly)
        {
            var queryTypes = assembly.GetTypes()
                .Where(type => typeof(IQuery).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            foreach (var queryType in queryTypes)
            {
                var queryInstance = (IQuery)Activator.CreateInstance(queryType);
                _queries[queryInstance.Name] = queryType;
            }
        }

        public IEnumerable<QueryDocumentation> GetQueryDocumentation()
        {
            foreach (var queryType in _queries.Values)
            {
                var queryInstance = (IQuery)Activator.CreateInstance(queryType);
                var doc = new QueryDocumentation
                {
                    Name = queryInstance.Name,
                    Description = queryInstance.Description
                };

                // For now, we'll hardcode parameters for GetDocumentationQuery.
                // In a real scenario, you might use reflection to find constructor parameters or a custom attribute.
                if (queryInstance.Name == "get-docs")
                {
                    // This query doesn't take any specific parameters via the CLI, but its dependencies are factories.
                }
                yield return doc;
            }
        }
    }
}