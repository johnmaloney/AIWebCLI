using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCLI.Core.Contracts
{
    public interface IQueryRepository
    {
        /// <summary>
        /// Add the query actions that will be available for a client application to use.
        /// </summary>
        /// <param name="queryName"></param>
        /// <param name="actionDelegate"></param>
        void AddQueryDelegate(string queryName, Func<IQueryCriteria, IQueryResult> actionDelegate);
    }
}
