using FaleMaisTelZir.Dominio.Chamada;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaleMaisTelZir.Infraestrutura.Interfaces
{
    public interface IDbContexto : IDisposable
    {
        int SalvarModificacoes();
        IDbSet<T> RetornarDbSet<T>() where T : class;
        void DefinirComoModificado(object model);

        DbSet<Chamada> Chamadas { get; set; }
    }
}
