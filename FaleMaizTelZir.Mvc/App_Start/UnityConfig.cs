using System.Web.Mvc;
using FaleMaisTelZir.Aplicacao.AplicacaoBase;
using FaleMaisTelZir.Aplicacao.Interfaces;
using FaleMaisTelZir.Dominio.Chamada;
using FaleMaisTelZir.Dominio.Interfaces.Repositorio;
using FaleMaisTelZir.Dominio.Interfaces.Servico;
using FaleMaisTelZir.Dominio.Servico.Base;
using FaleMaisTelZir.Infraestrutura.Contexto;
using FaleMaisTelZir.Infraestrutura.Interfaces;
using FaleMaisTelZir.Infraestrutura.Repositorio;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

namespace FaleMaizTelZir.Mvc
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IRepositorio<Chamada>, RepositorioBase<Chamada>>();
            container.RegisterType<IServico<Chamada>, ServicoBase<Chamada>>();
            container.RegisterType<IAplicacao<Chamada>, AplicacaoBase<Chamada>>();
            container.RegisterType<IDbContexto, FaleMaisTelZirContexto>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}