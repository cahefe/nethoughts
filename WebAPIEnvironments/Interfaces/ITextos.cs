namespace WebAPIEnvironments.Interfaces
{
    public interface ITextos
    {
        string Classe { get; }
        string GerarTexto(int digitos);
    }
}