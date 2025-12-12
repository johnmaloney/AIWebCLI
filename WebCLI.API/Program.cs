using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using WebCLI.Core.Contracts;
using WebCLI.Core.Pipes;
using WebCLI.Core.Repositories;
using WebCLI.Core.Configuration; 

namespace WebCLI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // API Versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("x-api-version"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure PipelineSettings
            builder.Services.AddOptions<PipelineSettings>()
                    .Bind(builder.Configuration.GetSection("PipelineSettings"))
                    .Validate(s => !string.IsNullOrWhiteSpace(s.PipelineDefinitionPath), "PipelineDefinitionPath must be set");

            // Simplified DI registration for IPipelineDefinitionRepository
            builder.Services.AddSingleton<WebCLI.Core.Contracts.IPipelineDefinitionRepository, WebCLI.Core.Repositories.JsonFilePipelineDefinitionRepository>();

            // Dependency Injection for WebCLI.Core services
            builder.Services.AddSingleton<WebCLI.Core.Contracts.IPipelineFactory, WebCLI.Core.Pipes.ReflectionPipelineFactory>(); // Explicitly use Contracts.IPipelineFactory
            builder.Services.AddSingleton<WebCLI.Core.Contracts.IPipelineInitializer, WebCLI.Core.Pipes.DynamicPipelineInitializer>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
