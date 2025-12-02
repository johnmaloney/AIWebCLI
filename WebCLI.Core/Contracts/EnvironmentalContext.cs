using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCLI.Core.Contracts;

namespace WebCLI.Core.Contracts
{
    public interface IEnvironmentalContext
    {
        IEnvironmentRepository Environments { get; }
    }
}
