using System;
using FaleMaisTelZir.Dominio.Interfaces.Modelo;

namespace FaleMaisTelZir.Dominio.Chamada
{
    public class Chamada : IModelo
    {
        public int Codigo { get; set; }
        public int Destino { get; set; }
        public int Origem { get; set; }
        public int Minuto { get; set; }
        public decimal ValorPorMinuto { get; set; }

        public void Validar()
        {
            if(Origem <= 0) throw new ApplicationException(@"O campo ""Origem"" é obrigatório.");

            if(Destino <= 0) throw new ApplicationException(@"O campo ""Destino"" é obrigatório.");

        }
    }
}
