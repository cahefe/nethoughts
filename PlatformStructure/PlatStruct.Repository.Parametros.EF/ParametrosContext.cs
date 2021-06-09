using AppStruct.Domain.Parametros.Model;
using Microsoft.EntityFrameworkCore;

namespace PlatStruct.Repository.Parametros.EF
{
    public class ParametrosContext : DbContext
    {
        public DbSet<Parametro> Parametros { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
    }
}