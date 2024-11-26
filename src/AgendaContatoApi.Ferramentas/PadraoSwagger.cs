using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AgendaContatoApi.Ferramentas
{
    public class PadraoSwagger : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            foreach (var property in model.Properties)
            {
                if (property.Value.Default == null && property.Value.Type == "boolean")
                {
                    property.Value.Default = new OpenApiBoolean(false);
                }
                else if (property.Value.Type == "string" && property.Value.Format == "date-time")
                {
                    property.Value.Type = "string";
                    property.Value.Format = "date-time";
                }
                else if (string.Equals(property.Value.Type, "string", StringComparison.OrdinalIgnoreCase))
                {
                    property.Value.Default = new OpenApiString(string.Empty);
                }
                else if (string.Equals(property.Value.Type, "array", StringComparison.OrdinalIgnoreCase))
                {
                    property.Value.Default = new OpenApiArray();
                }
            }
        }
    }
}