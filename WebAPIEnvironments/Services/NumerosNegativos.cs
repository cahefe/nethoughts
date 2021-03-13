using WebAPIEnvironments.Interfaces;
using System;

namespace WebAPIEnvironments.Services
{
    public class NumerosNegativos : INumeros
    {
        Random rnd = new Random();
        public long GerarNumero(int digitos)
        {
            long n = rnd.Next(1,10);
            for (int i = 0; i < rnd.Next(digitos > 19 ? 19 : digitos); i++)
                n = n * 10 + rnd.Next(10);
            return -n;
        }
    }
}