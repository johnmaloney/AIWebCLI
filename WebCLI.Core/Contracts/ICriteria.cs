namespace WebCLI.Core.Contracts
{
    public interface ICriteria
    {
        // The name of the command or query, which also acts as its identifier for pipeline lookup.
        string Name { get; }

        // The dictionary of criteria is now the primary way to pass parameters.
        // No longer includes GetPipeline(IPipe) as pipeline construction is dynamic.
    }
}