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

            // Search for bills with products, users, and customers
            var taskBuscarBills = billBL.SearchIncludeProductsAsync(pBill);
            var taskBuscarProducts = productsBL.GetByIdAsync(new Products { Id_Products = pBill.Id_Products });
            var taskBuscarUsuarios = usuarioBL.ObtenerPorIdAsync(new Usuario { Id_Usuario = pBill.Id_Usuario });
            var taskBuscarCustomers = customerBL.GetByIdAsync(new Customer {  Id_Customer = pBill.Id_Customer});

            // Get all products, users, and customers
            var taskObtenerTodosProducts = productsBL.GetallAsync();
            var taskObtenerTodosUsuarios = usuarioBL.ObtenerTodosAsync();
            var taskObtenerTodosCustomers = customerBL.GetAllAsync();

            // Wait for all tasks to complete
            var bills = await taskBuscarBills;
            var products = await taskBuscarProducts;
            var usuarios = await taskBuscarUsuarios;
            var customers = await taskBuscarCustomers;

            // Add the bills, products, users, and customers to the view bag
            ViewBag.Top = pBill.Top_Aux;
            ViewBag.Bills = bills;
            ViewBag.Products = products;
            ViewBag.Usuarios = usuarios;
            ViewBag.Customers = customers;

            // Return the view
            return View(bills);
        }

        // GET: BillController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
