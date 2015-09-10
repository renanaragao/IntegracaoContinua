using System;
using System.Linq;
using FaleMaisTelZir.Aplicacao.AplicacaoBase;
using FaleMaisTelZir.Dominio.Chamada;
using FaleMaisTelZir.Dominio.Interfaces.Servico;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FaleMaisTelzir.Aplicacao.Teste.AplicacaoBase
{
    [TestClass]
    public class AplicacaoBaseTeste
    {
        [TestMethod]
        public void Deve_Inserir_Uma_Model()
        {
            var chamada = new Chamada();

            var mockServico = new Mock<IServico<Chamada>>();
            mockServico.Setup(x => x.Inserir(It.IsAny<Chamada>())).Returns(new Chamada() {Codigo = 34});

            var aplicacao = new AplicacaoBase<Chamada>(mockServico.Object);

            chamada = aplicacao.Salvar(chamada);
            
            Assert.Fail();
            mockServico.Verify(x => x.Inserir(It.IsAny<Chamada>()), Times.Once());
            mockServico.Verify(x => x.Alterar(It.IsAny<Chamada>()), Times.Never());
            Assert.AreEqual(34, chamada.Codigo);

        }

        [TestMethod]
        public void Deve_Alterar_Uma_Model()
        {
            var chamada = new Chamada();
            chamada.Codigo = 34;

            var mockServico = new Mock<IServico<Chamada>>();

            var aplicacao = new AplicacaoBase<Chamada>(mockServico.Object);

            aplicacao.Salvar(chamada);

            mockServico.Verify(x => x.Inserir(It.IsAny<Chamada>()), Times.Never());
            mockServico.Verify(x => x.Alterar(It.IsAny<Chamada>()), Times.Once);

        }

        [TestMethod]
        public void Deve_Retornar_Todas_As_Chamadas()
        {

            var chamadas = Builder<Chamada>.CreateListOfSize(2).Build();

            var mockServico = new Mock<IServico<Chamada>>();
            mockServico.Setup(x => x.RetornarTodos()).Returns(chamadas);

            var aplicacao = new AplicacaoBase<Chamada>(mockServico.Object);

            var todasChamadas = aplicacao.RetornarTodos();

            Assert.AreEqual(2, todasChamadas.Count());

        }
    }
}
