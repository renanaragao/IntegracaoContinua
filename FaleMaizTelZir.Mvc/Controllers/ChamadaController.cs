using System;
using System.Web.Mvc;
using FaleMaisTelZir.Aplicacao.Interfaces;
using FaleMaisTelZir.Dominio.Chamada;

namespace FaleMaizTelZir.Mvc.Controllers
{
    public class ChamadaController : Base.ControllerBase
    {
        private readonly IAplicacao<Chamada> _aplicacao;

        public ChamadaController(IAplicacao<Chamada> aplicacao)
        {
            _aplicacao = aplicacao;
        }

        // GET: Chamada
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Salvar(Chamada chamada)
        {
            try
            {
                var resul = _aplicacao.Salvar(chamada);

                return JsonGetPermitido(resul);
            }
            catch (ApplicationException ex)
            {
                return JsonGetPermitido(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonGetPermitido(new { erro = ex.Message });
            }
        }

        public JsonResult RetornarChamadas()
        {
            return JsonGetPermitido(_aplicacao.RetornarTodos());
        }
    }
}