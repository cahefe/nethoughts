//using Swashbuckle.AspNetCore.SwaggerGen;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API_RoleBased_Swagger
{
    public class SwaggerAppProfilesFilter : IDocumentFilter
    {
        // public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        // {
        //     foreach (var apiDescription in apiExplorer.ApiDescriptions)
        //     {
        //         if (!apiDescription.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AppProfilesAttribute>().Any() && !apiDescription.ActionDescriptor.GetCustomAttributes<AppProfilesAttribute>().Any()) continue;
        //         var route = "/" + apiDescription.Route.RouteTemplate.TrimEnd('/');
        //         swaggerDoc.paths.Remove(route);
        //     }
        // }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // foreach (var apiDescription in context.ApiDescriptions)
            // {
            //     apiDescription.ActionDescriptor.FilterDescriptors.
            //     if (!apiDescription.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AppProfilesAttribute>().Any() && !apiDescription.ActionDescriptor.GetCustomAttributes<AppProfilesAttribute>().Any()) continue;
            //     var route = "/" + apiDescription.ActionDescriptor.RouteValues..Route.RouteTemplate.TrimEnd('/');
            //     swaggerDoc.Paths.Remove(route);
            // }
        }
    }
}
