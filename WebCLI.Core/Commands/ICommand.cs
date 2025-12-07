using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCLI.Core.Commands
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        Task<string> ExecuteAsync(Dictionary<string, string> parameters);
    }
}