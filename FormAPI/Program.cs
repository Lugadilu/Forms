/*using FormAPI.Context;
using FormAPI.Middleware;
using FormAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Form API",
        Version = "0.0.6-beta",
        Description = "Form API for Hotel-Platform",
        Contact = new OpenApiContact
        {
            Name = "Hotelplattform",
            Url = new Uri("https://hotelplatform.io"),
            Email = "info@key-card.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/license/mit/")
        }
    });

    // Configure the servers section
    //c.AddServer(new OpenApiServer
    //{
    //    Url = "http://localhost:{port}/api/v2",
    //    Description = "Your Mock",
    //    Variables = new Dictionary<string, OpenApiServerVariable>
    //    {
    //        ["port"] = new OpenApiServerVariable
    //        {
    //            Enum = new[] { "4010", "4011" },
    //            Default = "4010"
    //        }
    //    }
    //});

    // Add security definitions if required
    c.AddSecurityDefinition("openId", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OpenIdConnect,
        OpenIdConnectUrl = new Uri("https://your-auth-server/.well-known/openid-configuration"),
        Description = "OpenID Connect authentication"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "openId"
                }
            },
            new[] { "user", "admin" }
        }
    });
});

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register Repositories
builder.Services.AddScoped<IFormRepository, FormRepository>();

var app = builder.Build();

// Use Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        //c.SwaggerEndpoint("/swagger/v1/swagger.json", "Form API v1");
        c.RoutePrefix = string.Empty; // serve Swagger UI at the root URL
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
*/








/*

using FormAPI.Context;
using FormAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using FormAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FormAPI", Version = "v1" });
});

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register Repositories
builder.Services.AddScoped<IFormRepository, FormRepository>();

// Register Exception Middleware if exists
//builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// Use Exception Middleware if exists
// app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FormAPI v1");
        c.RoutePrefix = string.Empty; // serve Swagger UI at the root URL
    });
}



app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
//app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); //Routes for my API controllers
});
app.Run();

*/

/*
using FormAPI.Context;
using FormAPI.Middleware;
using FormAPI.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Runtime.Intrinsics.X86;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Form API",
        Version = "v1",
        Description = "Form API for Hotel-Platform",
        Contact = new OpenApiContact
        {
            Name = "Hotelplattform",
            Url = new Uri("https://hotelplatform.io"),
            Email = "info@key-card.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Uncomment and test configurations incrementally
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//Register ApplicationContext

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Register Repositories
builder.Services.AddScoped<IFormRepository, FormRepository>();

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

var app = builder.Build();

// Ensure database is migrated and seeded
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use the ExceptionMiddleware
app.UseMiddleware<ExceptionMiddleware>();

// Use the ContentTypeMiddleware
app.UseMiddleware<ContentTypeMiddleware>();

// Logging middleware to log each request
app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Handling request: {Path}", context.Request.Path);
    await next.Invoke();
    logger.LogInformation("Finished handling request.");
});


//app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

// Creating form fields
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Forms}/{action=Index}/{id?}");



app.Run();

*/




using FormAPI.Context;
using FormAPI.Middleware;
using FormAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Form API",
        Version = "v1",
        Description = "Form API for Hotel-Platform",
        Contact = new OpenApiContact
        {
            Name = "Hotelplattform",
            Url = new Uri("https://hotelplatform.io"),
            Email = "info@key-card.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Uncomment and test configurations incrementally
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register ApplicationContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register Repositories
builder.Services.AddScoped<IFormRepository, FormRepository>();

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

var app = builder.Build();

// Ensure database is migrated and seeded
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use the ExceptionMiddleware
app.UseMiddleware<ExceptionMiddleware>();

// Use the ContentTypeMiddleware
app.UseMiddleware<ContentTypeMiddleware>();

// Logging middleware to log each request
app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Handling request: {Path}", context.Request.Path);
    await next.Invoke();
    logger.LogInformation("Finished handling request.");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
