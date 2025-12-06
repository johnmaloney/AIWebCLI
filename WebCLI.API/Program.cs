using WebCLI.Core.Contracts;
using WebCLI.Core.Repositories;
using WebCLI.Core.Pipes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Reflection;
using System.IO; // Added for Path.Combine

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
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // Dependency Injection for WebCLI.Core services
            builder.Services.AddSingleton<IPipelineDefinitionRepository>(sp =>
            {
                var pipelineDefinitionPath = builder.Configuration["PipelineDefinitionPath"];
                if (string.IsNullOrEmpty(pipelineDefinitionPath))
                {
                    throw new InvalidOperationException("PipelineDefinitionPath configuration is missing.");
                }
                return new JsonFilePipelineDefinitionRepository(pipelineDefinitionPath);
            });
            builder.Services.AddSingleton<IPipelineFactory, ReflectionPipelineFactory>();
            builder.Services.AddSingleton<IPipelineInitializer, DynamicPipelineInitializer>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Good practice for development
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebCLI.API v1");
                    options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            // app.UseStaticFiles(); // Added as a diagnostic step if static files aren't loading correctly

            app.UseRouting(); // Ensure UseRouting is before UseAuthorization
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
