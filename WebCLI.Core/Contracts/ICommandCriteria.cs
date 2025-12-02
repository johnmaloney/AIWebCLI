using System.Collections.Generic;

namespace WebCLI.Core.Contracts
{
    // ICommandCriteria now simply extends ICriteria and assumes parameters are in the Criteria dictionary.
    // Specific properties like CommandDirective, Arguments, Options are removed as they are now parsed from Criteria.
    public interface ICommandCriteria : ICriteria
    {
        // The 'Criteria' dictionary on the concrete Command class fulfills this.
        // No additional members are explicitly needed on this interface for the current architecture.
    }
}