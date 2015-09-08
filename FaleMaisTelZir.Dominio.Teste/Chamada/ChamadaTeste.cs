using System;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FaleMaisTelZir.Dominio.Teste.Chamada
{
    /// <summary>
    /// Summary description for ChamadaTeste
    /// </summary>
    [TestClass]
    public class ChamadaTeste
    {
        [TestMethod]
        public void A_Chamada_Deve_Ter_Uma_Origem()
        {
            var chamada = Builder<Dominio.Chamada.Chamada>.CreateNew().Build();
            chamada.Origem = 0;

            try
            {
                chamada.Validar();
                Assert.Fail();
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual(@"O campo ""Origem"" é obrigatório.", ex.Message);
            }
        }

        [TestMethod]
        public void A_Chamada_Deve_Ter_Um_Destino()
        {
            var chamada = Builder<Dominio.Chamada.Chamada>.CreateNew().Build();
            chamada.Destino = 0;

            try
            {
                chamada.Validar();
                Assert.Fail();
            }
            catch (ApplicationException ex)
            {
                Assert.AreEqual(@"O campo ""Destino"" é obrigatório.", ex.Message);
            }
        }
    }
}
