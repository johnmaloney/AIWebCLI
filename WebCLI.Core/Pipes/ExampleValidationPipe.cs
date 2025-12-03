using System.Threading.Tasks;
using WebCLI.Core.Contracts;
using WebCLI.Core.Pipes;
using WebCLI.Core.Models;
using System.Collections.Generic;

namespace WebCLI.Core.Pipes
{
    public class ExampleValidationPipe : APipe
    {
        public override async Task<ICommandResult> Handle(IContext context)
        {
            if (context is GeneralContext generalContext && generalContext.Command != null)
            {
                // Placeholder for actual validation logic
                if (generalContext.Command.Criteria.GetValueOrDefault("username") == null ||
                    string.IsNullOrWhiteSpace(generalContext.Command.Criteria["username"])) {
                    return new CommandResult(false, "Validation failed: Username is required.");
                }
                // Simulate successful validation
                generalContext.Logger.Log("Validation successful.");
            }
            return await base.Handle(context); // Pass to the next pipe
        }
    }
}
