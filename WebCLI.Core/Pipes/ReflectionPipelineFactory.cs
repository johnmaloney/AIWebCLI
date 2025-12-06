using System;
using System.Linq;
using System.Reflection;
using WebCLI.Core.Contracts;
using WebCLI.Core.Models.Definitions;

namespace WebCLI.Core.Pipes
{
    public class ReflectionPipelineFactory : IPipelineFactory
    {
        public IPipe CreatePipe(PipeConfiguration pipeConfiguration)
        {
            if (pipeConfiguration == null) throw new ArgumentNullException(nameof(pipeConfiguration));
            if (string.IsNullOrWhiteSpace(pipeConfiguration.Type)) throw new ArgumentException("Pipe Type cannot be null or empty.", nameof(pipeConfiguration));

            Type pipeType = GetTypeFromConfiguration(pipeConfiguration);

            if (pipeType == null) throw new InvalidOperationException($"Could not find pipe type '{pipeConfiguration.Type}'.");
            if (!typeof(IPipe).IsAssignableFrom(pipeType)) throw new InvalidOperationException($"Type '{pipeConfiguration.Type}' does not implement IPipe.");

            return (IPipe)Activator.CreateInstance(pipeType);
        }

        public IContext CreatePipeContext(PipeConfiguration pipeConfiguration)
        {
            if (pipeConfiguration == null) throw new ArgumentNullException(nameof(pipeConfiguration));
            if (string.IsNullOrWhiteSpace(pipeConfiguration.ContextType)) throw new ArgumentException("Pipe ContextType cannot be null or empty.", nameof(pipeConfiguration));

            Type contextType = GetTypeFromConfiguration(pipeConfiguration, useContextType: true);

            if (contextType == null) throw new InvalidOperationException($"Could not find pipe context type '{pipeConfiguration.ContextType}'.");
            if (!typeof(IContext).IsAssignableFrom(contextType)) throw new InvalidOperationException($"Type '{pipeConfiguration.ContextType}' does not implement IContext."); // Corrected message

            return (IContext)Activator.CreateInstance(contextType);
        }

        private Type GetTypeFromConfiguration(PipeConfiguration pipeConfiguration, bool useContextType = false)
        {
            string typeName = useContextType ? pipeConfiguration.ContextType : pipeConfiguration.Type;
            string assemblyName = pipeConfiguration.Assembly;

            if (!string.IsNullOrWhiteSpace(assemblyName))
            {
                // Load the specified assembly
                try
                {
                    var assembly = Assembly.Load(assemblyName);
                    return assembly.GetType(typeName);
                }
                catch (Exception ex)
                {
                    // Log the exception (e.g., assembly not found or could not be loaded)
                    Console.WriteLine($"Error loading assembly '{assemblyName}' or type '{typeName}': {ex.Message}");
                    return null;
                }
            }
            else
            {
                // Search in all currently loaded assemblies if no assembly is specified
                return AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(a => a.GetTypes())
                                .FirstOrDefault(t => t.FullName.Equals(typeName, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
