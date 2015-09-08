using FaleMaisTelZir.Dominio.Interfaces.Modelo;
using FaleMaisTelZir.Dominio.Interfaces.Repositorio;
using FaleMaisTelZir.Infraestrutura.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FaleMaisTelZir.Infraestrutura.Repositorio
{
    public class RepositorioBase<T> : IRepositorio<T> where T : class, IModelo
    {
        protected IDbContexto FaleMaisTelZirContexto;

        public RepositorioBase(IDbContexto faleMaisTelZirContexto)
        {
            FaleMaisTelZirContexto = faleMaisTelZirContexto;
        }

        public T Inserir(T model)
        {
            using (FaleMaisTelZirContexto)
            {
                FaleMaisTelZirContexto.RetornarDbSet<T>().Add(model);

                FaleMaisTelZirContexto.SalvarModificacoes();
            }

            return model;

        }

        public void Alterar(T model)
        {

            using (FaleMaisTelZirContexto)
            {
                FaleMaisTelZirContexto.DefinirComoModificado(model);

                FaleMaisTelZirContexto.SalvarModificacoes();
            }

        }

        public IEnumerable<T> RetornarTodos()
        {
            return FaleMaisTelZirContexto.RetornarDbSet<T>().ToList();
        }
    }
}
