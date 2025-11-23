using System;
using System.Collections.Concurrent;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Repositories
{
    public class QueryRepository : IQueryRepository
    {
        #region Fields

        private readonly ConcurrentDictionary<string, Func<IQueryCriteria, IQueryResult>> queryActions;

        #endregion

        #region Properties
        public IQueryResult this[IQueryCriteria criteria]
        {
            get
            {
                return this.queryActions[criteria.Identifier.ToLowerInvariant()](criteria);
            }
        }

        #endregion

        #region Methods

        public QueryRepository()
        {
            queryActions = new ConcurrentDictionary<string, Func<IQueryCriteria, IQueryResult>>();
        }

        public void AddQueryDelegate(string queryName, Func<IQueryCriteria, IQueryResult> actionDelegate)
        {
            queryActions.AddOrUpdate(queryName, actionDelegate, (cmdName, action) => action = actionDelegate);
        }

        #endregion
    }
}
