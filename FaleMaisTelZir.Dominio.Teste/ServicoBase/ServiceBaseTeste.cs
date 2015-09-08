using System;
using System.Linq;
using FaleMaisTelZir.Dominio.Interfaces;
using FaleMaisTelZir.Dominio.Interfaces.Repositorio;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FaleMaisTelZir.Dominio.Teste.ServicoBase
{
    /// <summary>
    /// Summary description for ServiceBaseTeste
    /// </summary>
    [TestClass]
    public class ServiceBaseTeste
    {
        [TestMethod]
        public void Deve_Inserir_Uma_Model()
        {
            var chamada = Builder<Dominio.Chamada.Chamada>.CreateNew().Build();

            var mockRepositorio = new Mock<IRepositorio<Dominio.Chamada.Chamada>>();
            mockRepositorio.Setup(x => x.Inserir(It.IsAny<Dominio.Chamada.Chamada>()))
                .Returns(new Dominio.Chamada.Chamada() { Codigo = 34 });

            var servico = new Dominio.Servico.Base.ServicoBase<Dominio.Chamada.Chamada>(mockRepositorio.Object);

            chamada = servico.Inserir(chamada);

            mockRepositorio.Verify(x => x.Inserir(It.IsAny<Dominio.Chamada.Chamada>()), Times.Once());
            Assert.AreEqual(34, chamada.Codigo);

        }

        [TestMethod]
        public void Deve_Validar_Model_Ao_Inserir()
        {
            var chamada = Builder<Dominio.Chamada.Chamada>.CreateNew().Build();

            chamada.Origem = 0;

            var mockRepositorio = new Mock<IRepositorio<Dominio.Chamada.Chamada>>();

            var servico = new Dominio.Servico.Base.ServicoBase<Dominio.Chamada.Chamada>(mockRepositorio.Object);

            try
            {
                servico.Inserir(chamada);
                Assert.Fail();
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual(@"O campo ""Origem"" é obrigatório.", ex.Message);
            }
        }

        [TestMethod]
        public void Deve_Alterar_Uma_Model()
        {
            var chamada = Builder<Dominio.Chamada.Chamada>.CreateNew().Build();

            var mockRepositorio = new Mock<IRepositorio<Dominio.Chamada.Chamada>>();

            var servico = new Dominio.Servico.Base.ServicoBase<Dominio.Chamada.Chamada>(mockRepositorio.Object);

            servico.Alterar(chamada);

            mockRepositorio.Verify(x => x.Alterar(It.IsAny<Dominio.Chamada.Chamada>()), Times.Once());
        }

        [TestMethod]
        public void Deve_Validar_Model_Ao_Alterar()
        {
            var chamada = Builder<Dominio.Chamada.Chamada>.CreateNew().Build();

            chamada.Origem = 0;

            var mockRepositorio = new Mock<IRepositorio<Dominio.Chamada.Chamada>>();

            var servico = new Servico.Base.ServicoBase<Dominio.Chamada.Chamada>(mockRepositorio.Object);

            try
            {
                servico.Alterar(chamada);
                Assert.Fail();
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual(@"O campo ""Origem"" é obrigatório.", ex.Message);
            }
        }

        [TestMethod]
        public void Deve_Retornar_Todas_As_Chamadas()
        {
            var chamadas = Builder<Dominio.Chamada.Chamada>.CreateListOfSize(2).Build();

            var mockRepositorio = new Mock<IRepositorio<Dominio.Chamada.Chamada>>();
            mockRepositorio.Setup(x => x.RetornarTodos()).Returns(chamadas);

            var servico = new Servico.Base.ServicoBase<Dominio.Chamada.Chamada>(mockRepositorio.Object);

            var todasChamadas = servico.RetornarTodos();

            Assert.AreEqual(2, todasChamadas.Count());

        }
    }
}
