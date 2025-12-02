using WebCLI.Core.Contracts;
using WebCLI.Core.Models;

namespace WebCLI.Core.Pipes
{
    public abstract class APipeContext : IPipeContext
    {
        public IAuthContext UserContext { get; set; }
        // Add other common properties that all pipes might need
    }
}
