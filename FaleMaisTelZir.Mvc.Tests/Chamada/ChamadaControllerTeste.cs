using System;
using System.Text;
using System.Collections.Generic;
using FaleMaisTelZir.Aplicacao.Interfaces;
using FaleMaizTelZir.Mvc.Controllers;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FaleMaisTelZir.Mvc.Tests.Chamada
{
    /// <summary>
    /// Summary description for ChamadaControllerTeste
    /// </summary>
    [TestClass]
    public class ChamadaControllerTeste
    {
        [TestMethod]
        public void Deve_Salvar_Um_Chamado()
        {
            var mockAplicacao = new Mock<IAplicacao<Dominio.Chamada.Chamada>>();
            mockAplicacao.Setup(x => x.Salvar(It.IsAny<Dominio.Chamada.Chamada>()))
                .Returns(new Dominio.Chamada.Chamada {Codigo = 34});

            var controller = new ChamadaController(mockAplicacao.Object);

            var result = controller.Salvar(new Dominio.Chamada.Chamada());

            Assert.AreEqual(34, ((Dominio.Chamada.Chamada) result.Data).Codigo);
        }

        [TestMethod]
        public void Deve_Tratar_ApplicationException_Ao_Salvar_Uma_Chamada()
        {
            var mockAplicacao = new Mock<IAplicacao<Dominio.Chamada.Chamada>>();
            mockAplicacao.Setup(x => x.Salvar(It.IsAny<Dominio.Chamada.Chamada>()))
                .Throws(new ApplicationException("validação"));

            var controller = new ChamadaController(mockAplicacao.Object);

            var result = controller.Salvar(new Dominio.Chamada.Chamada());

            Assert.AreEqual("validação", result.Data.GetType().GetProperty("erro").GetValue(result.Data));
        }

        [TestMethod]
        public void Deve_Tratar_Exception_Ao_Salvar_Uma_Chamada()
        {
            var mockAplicacao = new Mock<IAplicacao<Dominio.Chamada.Chamada>>();
            mockAplicacao.Setup(x => x.Salvar(It.IsAny<Dominio.Chamada.Chamada>()))
                .Throws(new Exception("erro interno do servidor"));

            var controller = new ChamadaController(mockAplicacao.Object);

            var result = controller.Salvar(new Dominio.Chamada.Chamada());

            Assert.AreEqual("erro interno do servidor", result.Data.GetType().GetProperty("erro").GetValue(result.Data));
        }

        [TestMethod]
        public void Deve_Retornar_Todas_As_Chamadas()
        {
            var chamadas = Builder<Dominio.Chamada.Chamada>.CreateListOfSize(2).Build();

            var mockAplicacao = new Mock<IAplicacao<Dominio.Chamada.Chamada>>();
            mockAplicacao.Setup(x => x.RetornarTodos())
                .Returns(chamadas);

            var controller = new ChamadaController(mockAplicacao.Object);

            var result = controller.RetornarChamadas();

            Assert.AreEqual(2, ((List<Dominio.Chamada.Chamada>) result.Data).Count);
        }
    }
}
