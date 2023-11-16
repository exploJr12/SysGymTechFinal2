using Microsoft.AspNetCore.Mvc;
using SysGymT.EntidadesDeNegocio;
using SysGymT.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using SysGymT.AccesoADatos;

namespace SysGymT.UI.AppWebAspCore.Controllers
{
    public class CustomerController : Controller
    {
        CustomerBL customerBL = new CustomerBL();
        // GET: CustomerController
        public async Task<IActionResult> Index(Customer pCustomer = null)
        {
            if (pCustomer == null)
                pCustomer = new Customer();
            if (pCustomer.Top_Aux == 0)
                pCustomer.Top_Aux = 10;
            else if (pCustomer.Top_Aux == -1)
                pCustomer.Top_Aux = 0;
            var customers = await customerBL.SearchAsync(pCustomer);
            ViewBag.Top = pCustomer.Top_Aux;
            return View(customers);
        }

        // GET: CustomerController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var customers = await customerBL.GetByIdAsync(new Customer { Id_Customer = id });
            return View(customers);
        }

        // GET: CustomerController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer pCustomer)
        {
            try
            {
                int result = await customerBL.CreateAsync(pCustomer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCustomer);
            }
        }

        // GET: CustomerController/Edit/5
        public async Task<IActionResult> Edit(Customer pCustomer)
        {
            var getCustomer = await customerBL.GetByIdAsync(pCustomer);
            ViewBag.Error = "";
            return View(getCustomer);
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer pCustomer)
        {
            try
            {
                int getCustomer = await customerBL.ModifyAsync(pCustomer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCustomer);
            }
        }

        // GET: CustomerController/Delete/5
        public async Task<IActionResult> Delete(Customer pCustomer)
        {
            ViewBag.Error = "";
            var getCustomer = await customerBL.GetByIdAsync(pCustomer);
            return View(getCustomer);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Customer pCustomer)
        {
            try
            {
                int result = await customerBL.DeleteAsync(pCustomer);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCustomer);
            }
        }
    }
}
