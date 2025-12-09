using Microsoft.AspNetCore.Mvc;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models.Definitions;

namespace WebCLI.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IPipelineDefinitionRepository _pipelineDefinitionRepository;
        private readonly IPipelineInitializer _pipelineInitializer;

        public AdminController(IPipelineDefinitionRepository pipelineDefinitionRepository, IPipelineInitializer pipelineInitializer)
        {
            _pipelineDefinitionRepository = pipelineDefinitionRepository;
            _pipelineInitializer = pipelineInitializer;
        }

        [HttpGet("pipelines")]
        public ActionResult<IEnumerable<PipelineDefinition>> GetAllPipelineDefinitions()
        {
            var definitions = _pipelineInitializer.GetAllPipelineDefinitions();
            return Ok(definitions);
        }

        [HttpGet("pipelines/{name}")]
        public ActionResult<PipelineDefinition> GetPipelineDefinition(string name)
        {
            var definition = _pipelineInitializer.GetAllPipelineDefinitions().FirstOrDefault(p => p.Name == name); // Modified to use _pipelineInitializer
            if (definition == null)
            {
                return NotFound();
            }
            return Ok(definition);
        }
    }
}