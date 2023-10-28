using Microsoft.AspNetCore.Mvc;
using SysGymT.EntidadesDeNegocio;
using SysGymT.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Threading.Tasks;
using SysGymT.AccesoADatos;

namespace SysGymT.UI.AppWebAspCore.Controllers
{
    public class MachinesController : Controller
    {
        MachinesBL machinesBL = new MachinesBL();
        UsuarioBL usuarioBL = new UsuarioBL();
        //GET : MachinesController
        public async Task<IActionResult> Index(Machines pMachines = null)
        {
            if (pMachines == null)
                pMachines = new Machines();
            if (pMachines.Top_Aux == 0)
                pMachines.Top_Aux = 10;
            else if (pMachines.Top_Aux == -1)
                pMachines.Top_Aux = 0;
            var taskSearch = machinesBL.GetAllAsync(pMachines);
            //var taskObtenerTodosUsuarios = usuarioBL.ObtenerTodosAsync(new Usuario { Id_Usuario = pMachines.Id_Usuario });
            //var machines = await taskSearch;
            var machines = await taskSearch;
            ViewBag.Top = pMachines.Top_Aux;
            ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
            return View(machines);
        }


        public async Task<IActionResult> Details(int id)
        {
            var machine = await machinesBL.GetByIdAsync(new Machines { Id_Machines = id});
            machine.Usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id_Usuario = machine.Id_Usuario });

            return View(machine);
        }

        // GET: MachinesController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: MachinesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Machines pMachine)
        {
            try
            {
                int result = await machinesBL.CreateAsync(pMachine);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occurred while saving the entity changes: " + ex.InnerException?.Message;
                ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
                return View(pMachine);
            }
        }

        // GET: MachinesController/Edit/5
        public async Task<IActionResult> Edit(Machines pMachine)
        {
            var machine = await machinesBL.GetByIdAsync(pMachine);
            var taskObtenerTodosUsuarios = usuarioBL.ObtenerTodosAsync();
            ViewBag.Usuario = await taskObtenerTodosUsuarios;
            ViewBag.Error = "";
            return View(machine);
        }

        // POST: MachinesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Machines pMachine)
        {
            try
            {
                int result = await machinesBL.ModifyAsync(pMachine);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuario = await usuarioBL.ObtenerTodosAsync();
                return View(pMachine);
            }
        }

        // GET: MachinesController/Delete/5
        public async Task<IActionResult> Delete(Machines pMachine)
        {
            var machine = await machinesBL.GetByIdAsync(pMachine);
            machine.Usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id_Usuario = machine.Id_Usuario });
            ViewBag.Error = "";
            return View(machine);
        }

        // POST: MachinesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Machines pMachine)
        {
            try
            {
                int result = await machinesBL.DeleteAsync(pMachine);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var machine = await machinesBL.GetByIdAsync(pMachine);
                if (machine == null)
                    machine = new Machines();
                if (machine.Id_Usuario > 0)
                    machine.Usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id_Usuario = machine.Id_Usuario });
                return View(machine);
            }
        }

    }
}
