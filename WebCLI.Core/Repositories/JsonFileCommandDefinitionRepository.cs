using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;
using Newtonsoft.Json;

namespace WebCLI.Core.Repositories
{
    public class JsonFileCommandDefinitionRepository : ICommandDefinitionRepository
    {
        private readonly string _filePath;
        private List<CommandDefinition> _commandDefinitions;

        public JsonFileCommandDefinitionRepository(string filePath)
        {
            _filePath = filePath;
            LoadDefinitions();
        }

        private void LoadDefinitions()
        {
            if (!File.Exists(_filePath))
            {
                _commandDefinitions = new List<CommandDefinition>();
                return;
            }

            var json = File.ReadAllText(_filePath);
            _commandDefinitions = JsonConvert.DeserializeObject<List<CommandDefinition>>(json) ?? new List<CommandDefinition>();
        }

        public IEnumerable<CommandDefinition> GetAllCommandDefinitions()
        {
            return _commandDefinitions;
        }

        public CommandDefinition GetCommandDefinitionByName(string name)
        {
            return _commandDefinitions.FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}