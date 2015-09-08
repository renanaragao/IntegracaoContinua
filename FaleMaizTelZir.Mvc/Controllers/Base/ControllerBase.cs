using System.Web.Mvc;

namespace FaleMaizTelZir.Mvc.Controllers.Base
{
    public class ControllerBase : Controller
    {
        protected JsonResult JsonGetPermitido(object data)
        {
            return base.Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}