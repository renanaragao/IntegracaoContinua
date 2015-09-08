using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FaleMaisTelZir.Dominio.Chamada;
using Moq;
using FaleMaisTelZir.Infraestrutura.Contexto;
using FaleMaisTelZir.Infraestrutura.Repositorio;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FaleMaisTelZir.Infraestrutura.Interfaces;
using FizzWare.NBuilder;
using System.Linq;

namespace FaleMaisTelZir.Infraestrutura.Teste
{
    /// <summary>
    /// Summary description for RepositorioBaseTeste
    /// </summary>
    [TestClass]
    public class RepositorioBaseTeste
    {
        [TestMethod]
        public void Deve_Inserir_A_Model()
        {
            var chamada = new Chamada();

            var mockDbSetModel = new Mock<DbSet<Chamada>>();

            var mockContexto = new Mock<IDbContexto>();
            mockContexto.Setup(x => x.RetornarDbSet<Chamada>()).Returns(mockDbSetModel.Object);

            var repositorio = new RepositorioBase<Chamada>(mockContexto.Object);

            chamada = repositorio.Inserir(chamada);

            mockDbSetModel.Verify(x => x.Add(It.IsAny<Chamada>()), Times.Once());
            mockContexto.Verify(x => x.SalvarModificacoes(), Times.Once());
            mockContexto.Verify(x => x.Dispose(), Times.Once());

        }

        [TestMethod]
        public void Deve_Alterar_A_Model()
        {
            var chamada = new Chamada();

            var mockContexto = new Mock<IDbContexto>();

            var repositorio = new RepositorioBase<Chamada>(mockContexto.Object);

            repositorio.Alterar(chamada);

            mockContexto.Verify(x => x.DefinirComoModificado(It.IsAny<Chamada>()), Times.Once());
            mockContexto.Verify(x => x.SalvarModificacoes(), Times.Once());
            mockContexto.Verify(x => x.Dispose(), Times.Once());
        }

        [TestMethod]
        public void Deve_Retornar_Todos_As_Models()
        {

            var chamadas = ((List<Chamada>)Builder<Chamada>.CreateListOfSize(2).Build()).AsQueryable();

            var mockDbSetModel = new Mock<DbSet<Chamada>>();
            mockDbSetModel.As<IQueryable<Chamada>>().Setup(m => m.Provider).Returns(chamadas.Provider);
            mockDbSetModel.As<IQueryable<Chamada>>().Setup(m => m.Expression).Returns(chamadas.Expression);
            mockDbSetModel.As<IQueryable<Chamada>>().Setup(m => m.ElementType).Returns(chamadas.ElementType);
            mockDbSetModel.As<IQueryable<Chamada>>().Setup(m => m.GetEnumerator()).Returns(chamadas.GetEnumerator());

            var mockContexto = new Mock<IDbContexto>();
            mockContexto.Setup(x => x.RetornarDbSet<Chamada>()).Returns(mockDbSetModel.Object);

            var repositorio = new RepositorioBase<Chamada>(mockContexto.Object);

            var models = repositorio.RetornarTodos();

            Assert.AreEqual(2, models.Count());

        }
    }
}
