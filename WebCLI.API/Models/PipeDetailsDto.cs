namespace WebCLI.API.Models
{
    public class PipeDetailsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string InputType { get; set; }
        public string OutputType { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
    }
}