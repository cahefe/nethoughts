using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace API_RoleBased_Swagger
{
    public class SwaggerProfileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var customParam = context.MethodInfo.GetCustomAttribute<AppProfilesAttribute>();
            if (customParam != null)
                operation.Description += "Papéis necessários: " + ((AppProfilesAttribute)customParam).Prifiles.ToString();
        }
    }
}