using System.Collections.Generic;
using FaleMaisTelZir.Dominio.Interfaces.Modelo;

namespace FaleMaisTelZir.Dominio.Interfaces.Repositorio
{
    public interface IRepositorio<T> where T: class, IModelo
    {
        T Inserir(T model);
        void Alterar(T model);
        IEnumerable<T> RetornarTodos();
    }
}
