using System.Collections.Generic;
using FaleMaisTelZir.Dominio.Interfaces.Modelo;
using FaleMaisTelZir.Dominio.Interfaces.Servico;

namespace FaleMaisTelZir.Aplicacao.AplicacaoBase
{
    public class AplicacaoBase<T> : Interfaces.IAplicacao<T> where T: class, IModelo
    {
        private readonly IServico<T> _servico;

        public AplicacaoBase(IServico<T> servico)
        {
            _servico = servico;
        }

        public T Salvar(T model)
        {
            if (model.Codigo <= 0) return _servico.Inserir(model);

            _servico.Alterar(model);

            return model;
        }

        public IEnumerable<T> RetornarTodos()
        {
            return _servico.RetornarTodos();
        }
    }
}
