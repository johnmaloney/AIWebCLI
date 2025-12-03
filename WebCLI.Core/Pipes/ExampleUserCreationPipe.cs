using System.Threading.Tasks;
using WebCLI.Core.Contracts;
using WebCLI.Core.Pipes;
using WebCLI.Core.Models;
using System.Collections.Generic;

namespace WebCLI.Core.Pipes
{
    public class ExampleUserCreationPipe : APipe
    {
        public override async Task<ICommandResult> Handle(IContext context)
        {
            if (context is GeneralContext generalContext && generalContext.Command != null)
            {
                // Placeholder for actual user creation logic
                string username = generalContext.Command.Criteria.GetValueOrDefault("username");
                // In a real scenario, interact with a UserRepository to create the user
                generalContext.Logger.Log($"User '{username}' created successfully (simulated).");
                return new CommandResult(true, $"User '{username}' created successfully.");
            }
            return new CommandResult(false, "User creation pipe received invalid context or command.");
        }
    }
}
