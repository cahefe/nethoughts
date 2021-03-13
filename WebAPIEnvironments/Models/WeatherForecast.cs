using System;

namespace WebAPIEnvironments.Models
{
    public class WeatherForecast
    {
        public string Textos { get; set; } = "undefined";
        public long Numeros { get; set; } = 0;
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public string Summary { get; set; }
        public ResultadoCalculo Calculo { get; set; }
        public string TextoAleatorio { get; set; }
    }
}
