using System;
using System.Collections.Generic;
using FaleMaisTelZir.Dominio.Interfaces.Modelo;
using FaleMaisTelZir.Dominio.Interfaces.Repositorio;
using FaleMaisTelZir.Dominio.Interfaces.Servico;

namespace FaleMaisTelZir.Dominio.Servico.Base
{
    public class ServicoBase<T> : IServico<T> where T : class, IModelo
    {
        private readonly IRepositorio<T> _repositorio;

        public ServicoBase(IRepositorio<T> repositorio)
        {
            _repositorio = repositorio;
        }

        public T Inserir(T model)
        {
            model.Validar();

            return _repositorio.Inserir(model);
        }

        public void Alterar(T model)
        {
            model.Validar();

            _repositorio.Alterar(model);
        }

        public IEnumerable<T> RetornarTodos()
        {
            return _repositorio.RetornarTodos();
        }
    }
}
