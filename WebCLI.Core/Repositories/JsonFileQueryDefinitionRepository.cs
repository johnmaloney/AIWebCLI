using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;
using Newtonsoft.Json;

namespace WebCLI.Core.Repositories
{
    public class JsonFileQueryDefinitionRepository : IQueryDefinitionRepository
    {
        private readonly string _filePath;
        private List<QueryDefinition> _queryDefinitions;

        public JsonFileQueryDefinitionRepository(string filePath)
        {
            _filePath = filePath;
            LoadDefinitions();
        }

        private void LoadDefinitions()
        {
            if (!File.Exists(_filePath))
            {
                _queryDefinitions = new List<QueryDefinition>();
                return;
            }

            var json = File.ReadAllText(_filePath);
            _queryDefinitions = JsonConvert.DeserializeObject<List<QueryDefinition>>(json) ?? new List<QueryDefinition>();
        }

        public IEnumerable<QueryDefinition> GetAllQueryDefinitions()
        {
            return _queryDefinitions;
        }

        public QueryDefinition GetQueryDefinitionByName(string name)
        {
            return _queryDefinitions.FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}