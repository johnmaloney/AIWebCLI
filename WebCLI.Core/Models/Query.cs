using System.Collections.Generic;
using WebCLI.Core.Contracts;

namespace WebCLI.Core.Models
{
    public class Query : IQueryCriteria
    {
        public string Name { get; set; }
        public Dictionary<string, string> Criteria { get; set; }
        public IAuthContext UserContext { get; set; }

        public Query(string name, Dictionary<string, string> criteria = null, IAuthContext userContext = null)
        {
            Name = name;
            Criteria = criteria ?? new Dictionary<string, string>();
            UserContext = userContext;
        }
    }
}
