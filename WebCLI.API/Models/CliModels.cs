namespace WebCLI.API.Models
{
    public class CliCommandRequest
    {
        public string CommandName { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }

    public class CliCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
        public string ResponseType { get; set; }
    }
}