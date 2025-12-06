namespace WebCLI.Core.Queries
{
    public interface IQuery
    {
        string Name { get; }
        string Description { get; }
        Task<string> ExecuteAsync(Dictionary<string, string> parameters);
    }
}