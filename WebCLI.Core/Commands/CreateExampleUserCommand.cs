using System.Collections.Generic;
using System.Threading.Tasks;
using WebCLI.Core.Pipes;
using WebCLI.Core.Models;

namespace WebCLI.Core.Commands
{
    public class CreateExampleUserCommand : ICommand
    {
        private readonly IPipelineInitializer _pipelineInitializer;

        public CreateExampleUserCommand(IPipelineInitializer pipelineInitializer)
        {
            _pipelineInitializer = pipelineInitializer;
        }

        public string Name => "create-example-user";
        public string Description => "Creates an example user using the core example pipes.";

        public async Task<string> ExecuteAsync(Dictionary<string, string> parameters)
        {
            if (!parameters.TryGetValue("username", out var username) || string.IsNullOrWhiteSpace(username))
            {
                return "Error: 'username' parameter is required.";
            }
            if (!parameters.TryGetValue("password", out var password) || string.IsNullOrWhiteSpace(password))
            {
                return "Error: 'password' parameter is required.";
            }

            // In a real scenario, you'd define a specific pipeline for this command.
            // For demonstration, we'll simulate a pipeline call.
            var command = new Command("CreateUser", parameters);
            var result = await _pipelineInitializer.ExecuteCommandPipeline(command);

            if (result.Success)
            {
                return $"Successfully created user: {username}";
            }
            else
            {
                return $"Failed to create user: {result.Message}";
            }
        }
    }
}