using Microsoft.AspNetCore.Mvc;
using SysGymT.EntidadesDeNegocio;
using SysGymT.LogicaDeNegocio;

namespace SysGymT.UI.AppWebAspCore.Controllers
{
    public class ProductsController : Controller
    {
        ProductsBL ProductBL = new ProductsBL();
        public async Task<IActionResult> Index(Products pProduct = null)
        {
            if (pProduct == null)
                pProduct = new Products();
            if (pProduct.Top_Aux == 0)
                pProduct.Top_Aux = 10;
            else if (pProduct.Top_Aux == -1)
                pProduct.Top_Aux = 0;
            var products = await ProductBL.SearchASync(pProduct);
            ViewBag.Top = pProduct.Top_Aux;
            return View(products);
        }
        public async Task<IActionResult> Details(int id)
        {
            var products = await ProductBL.GetByIdAsync(new Products { Id_products = id });
            return View(products);
        }
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products pProduct)
        {
            try
            {
                int result = await ProductBL.CreateAsync(pProduct);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pProduct);
            }
        }

        // GET: RolController/Edit/5
        public async Task<IActionResult> Edit(Products pProduct)
        {
            var products = await ProductBL.GetByIdAsync(pProduct);
            ViewBag.Error = "";
            return View(products);
        }

        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Products pProduct)
        {
            try
            {
                int result = await ProductBL.ModifyAsync(pProduct);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pProduct);
            }
        }

        // GET: RolController/Delete/5
        public async Task<IActionResult> Delete(Products pProduct)
        {
            ViewBag.Error = "";
            var products = await ProductBL.GetByIdAsync(pProduct);
            return View(products);
        }

        // POST: RolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Products pProduct)
        {
            try
            {
                int result = await ProductBL.DeleteAsync(pProduct);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pProduct);
            }
        }
    }
}
