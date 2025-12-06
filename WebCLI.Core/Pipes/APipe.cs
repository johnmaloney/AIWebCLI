using System.Threading.Tasks;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models;

namespace WebCLI.Core.Pipes
{
    public abstract class APipe : IPipe
    {
        protected IPipe _nextPipe; // Changed to IPipe

        public IPipe NextPipe => _nextPipe;

        public IPipe ExtendWith(IPipe pipe)
        {
            _nextPipe = pipe;
            return pipe;
        }

        public virtual async Task Process(IContext context)
        {
            // Perform the specific logic for this pipe.
            // The Handle method can contain the core logic, or it can be directly in Process.
            // For now, let's assume Handle performs the pipe's direct action.
            await Handle(context);

            // If there's a next pipe, pass the context to it.
            if (_nextPipe != null)
            {
                await _nextPipe.Process(context);
            }
        }

        public virtual async Task<ICommandResult> Handle(IContext context)
        {
            // This method will contain the specific logic for each concrete pipe.
            // Default implementation, concrete pipes will override this.
            // This method is called by Process.
            return new CommandResult(true, "Pipe handled successfully.");
        }
    }
}
