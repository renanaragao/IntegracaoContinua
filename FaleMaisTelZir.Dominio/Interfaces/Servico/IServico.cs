using System.Collections.Generic;
using FaleMaisTelZir.Dominio.Interfaces.Modelo;

namespace FaleMaisTelZir.Dominio.Interfaces.Servico
{
    public interface IServico<T> where T: class, IModelo
    {
        T Inserir(T model);
        void Alterar(T model);
        IEnumerable<T> RetornarTodos();
    }
}
