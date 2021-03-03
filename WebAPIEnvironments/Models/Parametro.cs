namespace WebAPIEnvironments.Models
{
    /// <summary>
    /// Informações sobre um parâmetro de configuração da plataforma
    /// </summary>
    public class Parametro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public object Valor { get; set; }
    }
}