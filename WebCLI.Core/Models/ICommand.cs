using System.Collections.Generic;
using WebCLI.Core.Contracts; // Assuming IUserContext is in Contracts

namespace WebCLI.Core.Models
{
    public interface ICommand
    {
        string Name { get; }
        Dictionary<string, string> Parameters { get; }
        Dictionary<string, string> Options { get; }
        IUserContext UserContext { get; } // Added UserContext property
    }
}
