using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FormAPI.Context;
using Microsoft.OpenApi.Models;
using System.Reflection;
using FormAPI.Repositories;

namespace FormAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Register DbContext with scoped lifetime
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));


            // Register repositories
            services.AddScoped<IFormRepository, FormRepository>();

            // Other service registrations can go here
            // Add Swagger configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });

                // Include XML comments if available
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // Include definitions for FormFields and FormRecords
                c.SchemaFilter<SwaggerSchemaFilter>(); // Custom filter to include schema definitions

            });

            // Add MVC services
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure middleware
            // ...
        }
    }
}
