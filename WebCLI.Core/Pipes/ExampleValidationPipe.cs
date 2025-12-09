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
                if (generalContext.Command.Options == null ||
                    !generalContext.Command.Options.ContainsKey("username") ||
                    string.IsNullOrWhiteSpace(generalContext.Command.Options["username"]))
                {
                    return new CommandResult(false, "Validation failed: Username is required.");
                }
                generalContext.Logger.Log("Validation successful.");
            }
            return await base.Handle(context);
        }
    }
}
