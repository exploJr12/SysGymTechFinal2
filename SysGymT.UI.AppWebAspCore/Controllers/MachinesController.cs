
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

        public async Task<IActionResult> Index(Machines pMachines = null)
        {
            if (pMachines == null)
                pMachines = new Machines();
            if (pMachines.Top_Aux == 0)
                pMachines.Top_Aux = 10;
            else if (pMachines.Top_Aux == -1)
                pMachines.Top_Aux = 0;
            var machines = await machinesBL.SearchAsync(pMachines);
            ViewBag.Top = pMachines.Top_Aux;
            return View(machines);
        }

        public async Task<IActionResult> Details(int id)
        {
            var machines = await machinesBL.GetByIdAync(new Machines { Id_Machines = id });
            return View(machines);
        }

        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        //HTPP METHODS

        //Post

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Machines pMachines)
        {
            try
            {
                int result = await machinesBL.CreateAsync(pMachines);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pMachines);
            }
        }

        public async Task<IActionResult> Edit(Machines pMachines)
        {
            var machines = await machinesBL.GetByIdAync(pMachines);
            ViewBag.Error = "";
            return View(machines);
        }

        // POST METHOD

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Machines pMachines)
        {
            try
            {
                int result = await machinesBL.ModifyAsync(pMachines);
                return RedirectToAction(nameof(Index));
            } 
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pMachines);
            }
        }

        public async Task<IActionResult> Delete(Machines pMachines)
        {
            ViewBag.Error = "";
            var machines = await machinesBL.GetByIdAync(pMachines);
            return View(machines);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int id, Machines pMachines)
        {
            try
            {
                int result = await machinesBL.DeleteAsync(pMachines);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pMachines);
            }
        }
    }
}
