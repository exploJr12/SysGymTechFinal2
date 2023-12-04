using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SysGymT.AccesoADatos;
using SysGymT.EntidadesDeNegocio;
using SysGymT.LogicaDeNegocio;

namespace SysGymT.UI.AppWebAspCore.Controllers
{
    public class SaleController : Controller
    {

        SaleBL saleBL = new SaleBL();
        ProductsBL productsBL = new ProductsBL();
        // GET: SaleController
        public async Task<IActionResult> Index(Sale pSale = null)
        {
            if (pSale == null)
                pSale = new Sale();
            if (pSale.Top_Aux == 0)
                pSale.Top_Aux = 10;
            else if (pSale.Top_Aux == -1)
                pSale.Top_Aux = 0;
            var taskSearch = saleBL.GetAllAsync();
            var getAllProducts = productsBL.GetallAsync();

            var sale = await taskSearch;
            ViewBag.Top = pSale.Top_Aux;
            ViewBag.Products = await getAllProducts;
            return View(sale);
        }

        // GET: SaleController/Details/5
        // ... (código anterior)

        public async Task<IActionResult> Details(int id)
        {
            var sale = await saleBL.GetByIdAsync(new Sale { IdSale = id });
            sale.Products = await productsBL.GetByIdAsync(new Products { Id_Products = sale.Id_Products });  
                return View(sale);
        }
        // GET: SaleController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await productsBL.GetallAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: SaleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sale pSale)
        {
            try
            {
                int result = await saleBL.CreateAsync(pSale);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occurred while saving the entity changes: " + ex.InnerException?.Message;
                ViewBag.Products = await productsBL.GetallAsync();
                return View(pSale);
            }
        }

        // GET: SaleController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var sale = await saleBL.GetByIdAsync(new Sale { IdSale = id });
            var taskGetAllProducts = productsBL.GetallAsync();
            ViewBag.Products = await taskGetAllProducts;
            ViewBag.Error = "";
            return View(sale);
        }

        // POST: SaleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sale pSale)
        {
            try
            {
                int result = await saleBL.ModifyAsync(pSale);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Products = await productsBL.GetallAsync();
                return View(pSale);
            }
        }

        // GET: SaleController/Delete/5
        public async Task<IActionResult> Delete(Sale pSale)
        {
            var sale = await saleBL.GetByIdAsync(pSale);
            sale.Products = await productsBL.GetByIdAsync(new Products { Id_Products = sale.Id_Products });
            ViewBag.Error = "";
            return View(sale);
        }

        // POST: SaleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Sale pSale)
        {
            try
            {
                int result = await saleBL.DeleteAsync(pSale);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var sale = await saleBL.GetByIdAsync(new Sale { IdSale = id });
                if (sale == null)
                    sale = new Sale();
                if (sale.Id_Products > 0)
                    sale.Products = await productsBL.GetByIdAsync(new Products { Id_Products = sale.Id_Products });
                ViewBag.Products = await productsBL.GetallAsync();
                return View(sale);
            }
        }
    }
}
