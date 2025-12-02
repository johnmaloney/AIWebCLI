using System.Threading.Tasks;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;

namespace WebCLI.Core.Pipes
{
    public abstract class APipe
    {
        protected APipe _nextPipe;

        public APipe SetNext(APipe nextPipe)
        {
            _nextPipe = nextPipe;
            return nextPipe;
        }

        public virtual async Task<ICommandResult> Handle(IPipeContext context)
        {
            if (_nextPipe != null)
            {
                return await _nextPipe.Handle(context);
            }
            return new CommandResult(true, "Pipeline completed.");
        }
    }
}
