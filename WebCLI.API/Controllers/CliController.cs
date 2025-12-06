using Microsoft.AspNetCore.Mvc;
using WebCLI.Core.Commands;
using WebCLI.Core.Queries;
using WebCLI.API.Models;
using System.Dynamic;
using System.Linq;

namespace WebCLI.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CliController : ControllerBase
    {
        private readonly CommandFactory _commandFactory;
        private readonly QueryFactory _queryFactory;

        public CliController(CommandFactory commandFactory, QueryFactory queryFactory)
        {
            _commandFactory = commandFactory;
            _queryFactory = queryFactory;
        }

        [HttpPost("execute")]
        public async Task<IActionResult> Execute([FromBody] CliCommandRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CommandName))
            {
                return BadRequest(new CliCommandResponse
                { Success = false, Message = "CommandName cannot be empty.", ResponseType = "text" });
            }

            try
            {
                if (_commandFactory.GetCommandDocumentation().Any(c => c.Name == request.CommandName))
                {
                    var command = _commandFactory.GetCommand(request.CommandName);
                    var result = await command.ExecuteAsync(request.Parameters?.ToDictionary(p => p.Key, p => p.Value?.ToString()));
                    return Ok(new CliCommandResponse { Success = true, Message = result, ResponseType = "text" });
                }
                else if (_queryFactory.GetQueryDocumentation().Any(q => q.Name == request.CommandName))
                {
                    var query = _queryFactory.GetQuery(request.CommandName);
                    var result = await query.ExecuteAsync(request.Parameters?.ToDictionary(p => p.Key, p => p.Value?.ToString()));
                    return Ok(new CliCommandResponse { Success = true, Message = result, ResponseType = "text" });
                }
                else
                {
                    return NotFound(new CliCommandResponse { Success = false, Message = $"Unknown command or query: {request.CommandName}", ResponseType = "text" });
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new CliCommandResponse { Success = false, Message = ex.Message, ResponseType = "text" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CliCommandResponse { Success = false, Message = $"An unexpected error occurred: {ex.Message}", ResponseType = "text" });
            }
        }
    }
}