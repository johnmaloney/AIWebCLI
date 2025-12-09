namespace WebCLI.API.Models
{
    public class PipelineDetailsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // e.g., "Command", "Query"
        public List<PipeDetailsDto> Pipes { get; set; }
    }
}