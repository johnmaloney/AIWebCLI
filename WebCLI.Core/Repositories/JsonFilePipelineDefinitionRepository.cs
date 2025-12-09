using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WebCLI.Core.Models;
using WebCLI.Core.Models.Definitions;

namespace WebCLI.Core.Repositories
{
    public class JsonFilePipelineDefinitionRepository : IPipelineDefinitionRepository
    {
        private readonly string _pipelineDefinitionPath;
        private readonly Dictionary<string, PipelineDefinition> _pipelineDefinitions;

        public JsonFilePipelineDefinitionRepository(string pipelineDefinitionPath)
        {
            _pipelineDefinitionPath = pipelineDefinitionPath;
            _pipelineDefinitions = LoadPipelineDefinitions();
        }

        private Dictionary<string, PipelineDefinition> LoadPipelineDefinitions()
        {
            if (!File.Exists(_pipelineDefinitionPath))
            {
                // Log or throw an exception if the file doesn't exist
                return new Dictionary<string, PipelineDefinition>();
            }
            var json = File.ReadAllText(_pipelineDefinitionPath);
            var definitions = JsonConvert.DeserializeObject<List<PipelineDefinition>>(json);
            return definitions?.ToDictionary(d => d.Name, d => d) ?? new Dictionary<string, PipelineDefinition>();
        }

        public PipelineDefinition GetPipelineDefinition(string name)
        {
            _pipelineDefinitions.TryGetValue(name, out var definition);
            return definition;
        }

        public IEnumerable<PipelineDefinition> GetAllPipelineDefinitions()
        {
            return _pipelineDefinitions.Values;
        }
    }
}
