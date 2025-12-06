using Microsoft.AspNetCore.Mvc;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;
using WebCLI.API.Models;
using System.Dynamic;

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

            // Determine if it's a Command or Query based on definition (requires repository lookup)
            // For now, let's assume all are commands unless explicitly a query
            // This logic will need refinement based on how PipelineDefinition.Type is used

            // Attempt to execute as a Command
            var command = new Command(request.CommandName);
            foreach (var param in request.Parameters)
            { 
                command.Options.Add(param.Key, param.Value);
            }

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
    }
}