using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using WebCLI.Core.Models;
using WebCLI.Core.Models.Definitions;
using WebCLI.Core.Contracts;
using WebCLI.Core.Configuration;
using Microsoft.Extensions.Options;
using System;
using Microsoft.Extensions.Logging;

namespace WebCLI.Core.Repositories
{
    public class JsonFilePipelineDefinitionRepository : IPipelineDefinitionRepository
    {
        private readonly string _pipelineDefinitionPath;
        private readonly Dictionary<string, PipelineDefinition> _pipelineDefinitions;
        private readonly ILogger<JsonFilePipelineDefinitionRepository> _logger;

        public JsonFilePipelineDefinitionRepository(IOptions<PipelineSettings> pipelineSettings, ILogger<JsonFilePipelineDefinitionRepository> logger = null)
        {
            _pipelineDefinitionPath = pipelineSettings.Value.PipelineDefinitionPath;
            _logger = logger;

            if (string.IsNullOrEmpty(_pipelineDefinitionPath))
            {
                throw new InvalidOperationException("PipelineDefinitionPath configuration is missing in PipelineSettings.");
            }
            _pipelineDefinitions = LoadPipelineDefinitions();
        }

        private Dictionary<string, PipelineDefinition> LoadPipelineDefinitions()
        {
            var definitions = new Dictionary<string, PipelineDefinition>();

            if (!Directory.Exists(_pipelineDefinitionPath))
            {
                _logger?.LogWarning($"Pipeline definition path does not exist: {_pipelineDefinitionPath}");
                return definitions;
            }

            var jsonFiles = Directory.EnumerateFiles(_pipelineDefinitionPath, "*.json", SearchOption.TopDirectoryOnly);

            foreach (var file in jsonFiles)
            {
                try
                {
                    var jsonContent = File.ReadAllText(file);
                    var definition = JsonConvert.DeserializeObject<PipelineDefinition>(jsonContent);

                    if (definition != null && !string.IsNullOrEmpty(definition.Name))
                    {
                        if (!definitions.TryAdd(definition.Name, definition))
                        {
                            _logger?.LogWarning($"Duplicate pipeline definition name '{definition.Name}' found in file '{file}'. Skipping.");
                        }
                    }
                    else
                    {
                        _logger?.LogWarning($"Skipping file '{file}' due to invalid or missing PipelineDefinition name.");
                    }
                }
                catch (JsonSerializationException ex)
                {
                    _logger?.LogError(ex, $"Error deserializing pipeline definition from file '{file}'. Skipping file.");
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, $"An unexpected error occurred while processing file '{file}'. Skipping file.");
                }
            }

            return definitions;
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