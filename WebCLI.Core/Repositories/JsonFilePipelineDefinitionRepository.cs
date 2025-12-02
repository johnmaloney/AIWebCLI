using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models.Definitions;

namespace WebCLI.Core.Repositories
{
    public class JsonFilePipelineDefinitionRepository : IPipelineDefinitionRepository
    {
        private readonly string _pipelineDefinitionPath;
        private readonly List<PipelineDefinition> _definitions;

        public JsonFilePipelineDefinitionRepository(string pipelineDefinitionPath)
        {
            _pipelineDefinitionPath = pipelineDefinitionPath;
            _definitions = LoadPipelineDefinitions();
        }

        private List<PipelineDefinition> LoadPipelineDefinitions()
        {
            var definitions = new List<PipelineDefinition>();
            if (!Directory.Exists(_pipelineDefinitionPath))
            {
                // Log or handle the error appropriately
                return definitions;
            }

            foreach (var file in Directory.GetFiles(_pipelineDefinitionPath, "*.json"))
            {
                var json = File.ReadAllText(file);
                var definition = JsonConvert.DeserializeObject<PipelineDefinition>(json);
                if (definition != null)
                {
                    definitions.Add(definition);
                }
            }
            return definitions;
        }

        public IEnumerable<PipelineDefinition> GetAllPipelineDefinitions()
        {
            return _definitions;
        }

        public PipelineDefinition GetPipelineDefinition(string name)
        {
            return _definitions.FirstOrDefault(d => d.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}