using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCLI.Core.Contracts
{
    public interface ICommandRepository
    {
        IPipe this[IContext criteria] { get; }
        void AddCommandDelegate(string identifier, Func<IContext, IPipe> actionDelegate);
    }
}
