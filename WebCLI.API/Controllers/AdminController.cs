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

        public AdminController(IPipelineDefinitionRepository pipelineDefinitionRepository)
        {
            _pipelineDefinitionRepository = pipelineDefinitionRepository;
        }

        [HttpGet("pipelines")]
        public ActionResult<IEnumerable<PipelineDefinition>> GetAllPipelineDefinitions()
        {
            var definitions = _pipelineDefinitionRepository.GetAllPipelineDefinitions();
            return Ok(definitions);
        }

        [HttpGet("pipelines/{name}")]
        public ActionResult<PipelineDefinition> GetPipelineDefinition(string name)
        {
            var definition = _pipelineDefinitionRepository.GetPipelineDefinition(name);
            if (definition == null)
            {
                return NotFound();
            }
            return Ok(definition);
        }
    }
}