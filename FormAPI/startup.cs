using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FormAPI.Context;
using FormAPI.Repositories;

using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using Microsoft.OpenApi.Models;
using AutoMapper;
using FormAPI.Models;
using FormAPI.Middleware;
using System.Text.Json.Serialization;


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

            // Add the FormRepository and FormService as scoped services
            services.AddScoped<IFormRepository, FormRepository>();
            //services.AddScoped<FormService>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddAutoMapper(typeof(Startup));


            services.AddControllers()
         .AddJsonOptions(options =>
         {
             options.JsonSerializerOptions.IgnoreNullValues = true;
             options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
         });




            //cnfiguring Dto automapper fr what you chse to be displayed in the response

            //services.AddSingleton<FormService>();

            // Add Swagger configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });

                // Include XML comments if available
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // Include definitions for FormFields and FormRecords
                //c.SchemaFilter<SwaggerSchemaFilter>(); // Custom filter to include schema definitions
            });

            // Add MVC services
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
                });
            }
            else
            {
                app.UseExceptionHandler("/error"); // Optional: Configure a global error handler
                app.UseHsts();
            }
            // Register the custom exception handling middleware
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization(); // Add it here
           app.UseEndpoints(endpoints =>
            {
                // Endpoint routing  // Map controllers using attribute routing
                endpoints.MapControllers();
            });

            
        }
    }
}
