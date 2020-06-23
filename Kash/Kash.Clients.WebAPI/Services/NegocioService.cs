using Kash.Core.Models;
using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.ValidationAttribute;

namespace Kash.Clients.WebAPI.Services
{
    //  Referencia MS (MVC): https://docs.microsoft.com/pt-br/aspnet/mvc/overview/older-versions-1/models-data/validating-with-a-service-layer-cs
    //  https://dev.to/boriszn/error-handling-and-validation-architecture-in-net-core-3lhe
    //  https://stackoverflow.com/questions/16793982/separating-the-service-layer-from-the-validation-layer
    //  Código com IValidator: https://gist.github.com/benfoster/4017221
    public class NegocioService : INegocioService
    {
        // ;;readonly IValidator _validator;
        public void TratarErro(Entry entry)
        {
            throw new ValidationException("teste");
        }
    }
}