namespace WebCLI.API.Models
{
    public class PipeDetailsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string InputType { get; set; }
        public string OutputType { get; set; }
        public Dictionary<string, string> Parameters { get; set; } // ParameterName -> Type/Description
    }

    public class PipelineDetailsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // e.g., "Command", "Query"
        public List<PipeDetailsDto> Pipes { get; set; }
    }
}