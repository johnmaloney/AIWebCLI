using System.Collections.Generic;
using System.Threading.Tasks;
using WebCLI.Core.Contracts;
using System.Linq;
using WebCLI.Core.Queries;

namespace WebCLI.Core.Queries
{
    public class ListEnvironmentsQuery : IQuery
    {
        private readonly IEnvironmentRepository _environmentRepository;

        public ListEnvironmentsQuery(IEnvironmentRepository environmentRepository)
        {
            _environmentRepository = environmentRepository;
        }

        public string Name => "list-environments";
        public string Description => "Lists all configured environments.";

        public async Task<string> ExecuteAsync(Dictionary<string, string> parameters)
        {
            // Assuming IEnvironmentRepository exists and has a method to get environment info
            // For demonstration, returning a dummy list
            var environments = new List<string> { "Development", "Staging", "Production" };

            if (_environmentRepository != null)
            {
                // In a real scenario, fetch from the repository
                // environments = (await _environmentRepository.GetAllEnvironments()).Select(e => e.Name).ToList();
            }

            return await Task.FromResult($"Available Environments:\n{string.Join("\n", environments)}");
        }
    }
}