using System.Collections.Generic;
using FaleMaisTelZir.Dominio.Interfaces.Modelo;

namespace FaleMaisTelZir.Aplicacao.Interfaces
{
    public interface IAplicacao<T> where T: class, IModelo
    {
        T Salvar(T model);
        IEnumerable<T> RetornarTodos();
    }
}
