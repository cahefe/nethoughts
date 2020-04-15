using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API_RoleBased_Swagger
{
    public class SwaggerAppProfilesFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // RecriaDocumento(swaggerDoc, context);
            ViaCustomAttribute(swaggerDoc, context);
        }
        void ViaCustomAttribute(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            MethodInfo methodInfo;
            //AppProfilesAttribute? customParam;
            EnumAppProfiles appProfile = EnumAppProfiles.Public;
            //  Se não conseguir converter o documento em um item do enumerador limpa todo o documento...
            if (!Enum.TryParse(swaggerDoc.Info.Title, true, out appProfile))
                swaggerDoc.Paths.Clear();
            //  Se não, prepara um documento apenas com os métodos liberados para o perfil...
            else
                foreach (var apiDescription in context.ApiDescriptions)
                {
                    //  Por padrão assume que DEVE remover o item do documento
                    var removeRoute = true;
                    //  ... se foi possível obter o método associado à API...
                    if (apiDescription.TryGetMethodInfo(out methodInfo))
                    {
                        //  ... se foi possível encontrar um atributo que define qual o perfil pode acessar o método..
                        var customParam = methodInfo.GetCustomAttribute<AppProfilesAttribute>();
                        if (customParam != null)
                            //  ... se o atributo do perfil indica acesso público ou para o Perfil que representa o documento, então MANTEM no documento, caso contrário elimina-o
                            removeRoute = (customParam.Prifiles & EnumAppProfiles.Public | customParam.Prifiles & appProfile) == EnumAppProfiles.Undefined;
                    }
                    //  Se o parâmetro permaneceu "true" então a rota deve ser REMOVIDA...
                    if (removeRoute)
                        swaggerDoc.Paths.Remove("/" + apiDescription.RelativePath.TrimEnd('/'));
                }
        }
        void RecriaDocumento(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new Dictionary<string, OpenApiPathItem>(swaggerDoc.Paths);
            swaggerDoc.Paths.Clear();
            foreach (var path in paths)
                if (path.Key.Contains("Fore"))
                    swaggerDoc.Paths.Add(path.Key, path.Value);
        }
    }
}
