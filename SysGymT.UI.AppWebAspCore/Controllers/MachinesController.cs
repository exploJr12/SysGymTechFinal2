using Microsoft.AspNetCore.Mvc;
using SysGymT.EntidadesDeNegocio;
using SysGymT.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SysGymT.UI.AppWebAspCore.Controllers
{
    public class MachinesController : Controller
    {
        MachinesBL machinesBL = new MachinesBL();

        public IActionResult Index()
        {
            return View();
        }
    }
}
