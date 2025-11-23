namespace WebCLI.Core.Contracts
{
    public interface ICriteria
    {
        string Identifier { get; }

        IPipe GetPipeline(IPipe parentPipeline);
    }
}