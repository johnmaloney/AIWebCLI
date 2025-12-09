using Microsoft.AspNetCore.Mvc;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;
using WebCLI.API.Models;
using System.Dynamic;
using System.Linq; 
using WebCLI.Core.Models.Definitions; // Add this line

namespace WebCLI.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CliController : ControllerBase
    {
        private readonly IPipelineInitializer _pipelineInitializer;

        public CliController(IPipelineInitializer pipelineInitializer)
        {
            _pipelineInitializer = pipelineInitializer;
        }

        [HttpPost("execute")]
        public async Task<IActionResult> ExecuteCommand([FromBody] CliCommandRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CommandName))
            {
                return BadRequest(new CliCommandResponse
                { Success = false, Message = "CommandName cannot be empty.", ResponseType = "text" });
            }

            var command = new Command(
                request.CommandName,
                request.Parameters?.ToDictionary(p => p.Key, p => p.Value?.ToString()));

            var commandResult = await _pipelineInitializer.ExecuteCommandPipeline(command);

            if (commandResult != null)
            {
                return Ok(new CliCommandResponse
                {
                    Success = commandResult.Success,
                    Message = string.Join(Environment.NewLine, commandResult.Messages),
                    Data = commandResult.Response,
                    ResponseType = commandResult.ResponseType ?? "text"
                });
            }

            return StatusCode(500, new CliCommandResponse
            { Success = false, Message = "An unexpected error occurred.", ResponseType = "text" });
        }

        [HttpGet("pipelines")]
        public IActionResult GetPipelineDetails()
        {
            var pipelineDefinitions = _pipelineInitializer.GetAllPipelineDefinitions();

            var pipelineDetailsList = pipelineDefinitions.Select(pd => new PipelineDetailsDto
            {
                Name = pd.Name,
                Description = pd.Description,
                Type = pd.Type.ToString(), // Assuming Type is an enum or has a sensible ToString()
                Pipes = pd.Pipes.Select(p => new PipeDetailsDto
                {
                    Name = p.Name,
                    Description = p.Description,
                    InputType = p.InputType,
                    OutputType = p.OutputType,
                    Parameters = p.Parameters?.ToDictionary(param => param.Key, param => param.Value?.ToString())
                }).ToList()
            }).ToList();

            return Ok(pipelineDetailsList);
        }
    }
}