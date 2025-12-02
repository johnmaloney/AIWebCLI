using System.Threading.Tasks;
using WebCLI.Core.Contracts;
using WebCLI.Core.Pipes;
using WebCLI.Core.Models;

namespace WebCLI.Core.Pipes
{
    public class ExampleAuthPipe : APipe
    {
        public override async Task<ICommandResult> Handle(IPipeContext context)
        {
            if (context is GeneralContext generalContext && generalContext.Command != null)
            {
                // Placeholder for actual authentication logic
                // For example, check generalContext.Command.UserContext.IsAuthenticated or roles
                if (generalContext.Command.Name == "create-user" && !IsUserAdmin(generalContext.Command.UserContext))
                {
                    return new CommandResult(false, "Unauthorized: Only administrators can create users.");
                }
                // Simulate successful authentication
                generalContext.Logger.Log("Authentication successful.");
            }
            return await base.Handle(context); // Pass to the next pipe
        }

        private bool IsUserAdmin(IAuthContext userContext)
        {
            // Placeholder: Replace with actual user role checking
            return true; // For demonstration, always true
        }
    }
}
