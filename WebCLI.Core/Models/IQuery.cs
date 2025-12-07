namespace WebCLI.Core.Models
{
    public interface IQuery<out TResult>
    {
        string Name { get; }
        System.Collections.Generic.Dictionary<string, string> Parameters { get; }
    }
}
