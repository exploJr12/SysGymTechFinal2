using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SysGymT.EntidadesDeNegocio;
using SysGymT.LogicaDeNegocio;
using System.Security.Claims;
using System.Diagnostics;

namespace SysGymT.UI.AppWebAspCore.Controllers
{
    public class BillController : Controller
    {
        BillBL billBL = new BillBL();
        UsuarioBL usuarioBL = new UsuarioBL();
        CustomerBL customerBL = new CustomerBL();
        ProductsBL productsBL = new ProductsBL();
        // GET: BillController
        public async Task<IActionResult> Index(Bill pBill = null)
        {
            if (pBill == null)
                pBill = new Bill();
            if (pBill.Top_Aux == 0)
                pBill.Top_Aux = 10;
            else if (pBill.Top_Aux == -1)
                pBill.Top_Aux = 0;

            var taskBuscarBills = billBL.SearchAsync(pBill);
            var taskBuscarProducts = productsBL.GetByIdAsync(new Products { Id_Products = pBill.Id_Products });
            var taskBuscarUsuarios = usuarioBL.ObtenerPorIdAsync(new Usuario { Id_Usuario = pBill.Id_Usuario });
            var taskBuscarCustomers = customerBL.GetByIdAsync(new Customer {  Id_Customer = pBill.Id_Customer});

            var taskObtenerTodosProducts = productsBL.GetallAsync();
            var taskObtenerTodosUsuarios = usuarioBL.ObtenerTodosAsync();
            var taskObtenerTodosCustomers = customerBL.GetAllAsync();

            var bills = await taskBuscarBills;
            var products = await taskBuscarProducts;
            var usuarios = await taskBuscarUsuarios;
            var customers = await taskBuscarCustomers;

            ViewBag.Top = pBill.Top_Aux;
            ViewBag.Products = await productsBL.GetallAsync();
            ViewBag.Usuarios = await usuarioBL.ObtenerTodosAsync();
            ViewBag.Customers = await customerBL.GetAllAsync();

            return View(bills);
        }

        // GET: BillController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var bill = await billBL.GetByIdAsync(new Bill { Id_Bill = id });
            bill.customer = await customerBL.GetByIdAsync(new Customer { Id_Customer = bill.Id_Customer });
            bill.usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id_Usuario = bill.Id_Usuario });
            bill.products = await productsBL.GetByIdAsync(new Products { Id_Products = bill.Id_Products });
            return View(bill);
        }

        // GET: BillController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BillController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BillController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BillController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
