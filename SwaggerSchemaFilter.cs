using FormAPI.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace FormAPI
{
    public class SwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(FormField))
            {
                schema.Reference = null;
                schema.Type = "object";
                schema.Properties = typeof(FormField).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .ToDictionary(
                        prop => prop.Name,
                        prop =>
                        {
                            var propertySchema = context.SchemaGenerator.GenerateSchema(prop.PropertyType, context.SchemaRepository);
                            propertySchema.Nullable = prop.PropertyType.IsClass || Nullable.GetUnderlyingType(prop.PropertyType) != null;
                            return propertySchema;
                        });
            }
            else if (context.Type == typeof(FormRecord))
            {
                schema.Reference = null;
                schema.Type = "object";
                schema.Properties = typeof(FormRecord).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .ToDictionary(
                        prop => prop.Name,
                        prop =>
                        {
                            var propertySchema = context.SchemaGenerator.GenerateSchema(prop.PropertyType, context.SchemaRepository);
                            propertySchema.Nullable = prop.PropertyType.IsClass || Nullable.GetUnderlyingType(prop.PropertyType) != null;
                            return propertySchema;
                        });
            }
        }
    }
}
