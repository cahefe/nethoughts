using WebAPIEnvironments.Models;

namespace WebAPIEnvironments.Models
{
    public class ResultadoCalculo
    {
        public int ContadorInstancias { get; set; }
        public string Abordagem { get; set; }
        public decimal Valor { get; set; }
        public int Randomico { get; set; }
        public Employee Employee { get; set; }
    }
}