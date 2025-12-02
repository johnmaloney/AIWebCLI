namespace WebCLI.Core.Contracts
{
    // IQueryCriteria now simply extends ICriteria and assumes parameters are in the Criteria dictionary.
    public interface IQueryCriteria : ICriteria
    {
        // The 'Criteria' dictionary on the concrete Query class fulfills this.
        // No additional members are explicitly needed on this interface for the current architecture.
    }
}