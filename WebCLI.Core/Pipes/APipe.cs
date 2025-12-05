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
            // The Handle method is effectively the 'Process' logic for the current pipe.
            // After handling, it passes the context to the next pipe if one exists.
            await Handle(context);

            if (_nextPipe != null)
            {
                await _nextPipe.Process(context);
            }
        }

        public virtual async Task<ICommandResult> Handle(IContext context)
        {
            // This method will contain the specific logic for each concrete pipe.
            // For now, it's a placeholder or can contain common pipe logic.
            return new CommandResult(true, "Pipe handled successfully.");
        }
    }
}
