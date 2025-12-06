using System.Threading.Tasks;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;

namespace WebCLI.Core.Pipes
{
    public abstract class APipe : IPipe
    {
        protected IPipe _nextPipe;

        public IPipe NextPipe => _nextPipe;

        public IPipe ExtendWith(IPipe pipe)
        {
            _nextPipe = pipe;
            return pipe;
        }

        public virtual async Task Process(IContext context)
        {
            // Specific pipe logic will be implemented in derived classes by overriding the Handle method.
            // This base Process method ensures the chain of responsibility pattern.
            await Handle(context);

            if (_nextPipe != null)
            {
                await _nextPipe.Process(context);
            }
        }

        public virtual async Task<ICommandResult> Handle(IContext context)
        {
            // Default implementation, concrete pipes will override this.
            return new CommandResult(true, "Pipe handled successfully.");
        }
    }
}
