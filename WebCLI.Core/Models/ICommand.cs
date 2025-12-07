namespace WebCLI.Core.Models
{
    public interface ICommand
    {
        string Name { get; }
        System.Collections.Generic.Dictionary<string, string> Parameters { get; }
    }
}
