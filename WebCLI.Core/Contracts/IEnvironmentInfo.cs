namespace WebCLI.Core.Contracts
{
    public interface IEnvironmentInfo
    {
        // Example properties, adjust as needed
        string OSVersion { get; }
        string MachineName { get; }
        string CurrentDirectory { get; }
    }
}
