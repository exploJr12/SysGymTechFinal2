using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SysGymT.EntidadesDeNegocio;
using SysGymT.LogicaDeNegocio;

namespace SysGymT.UI.AppWebAspCore.Controllers
{
    public class DetailsController : Controller
    {
        DetailsBL  detailsBL = new DetailsBL();
        ProductsBL productsBL = new ProductsBL();
        // GET: DetailsController
        public async Task<IActionResult> Index(Details pDetails = null)
        {
            if (pDetails == null)
                pDetails = new Details();
            if (pDetails.Top_Aux == 0)
                pDetails.Top_Aux = 10;
            else if (pDetails.Top_Aux == -1)
                pDetails.Top_Aux = 0;
            var taskSearch = detailsBL.SearchAllAsync(pDetails);
            var taskGetAllProducts = productsBL.GetByIdAsync(new Products { Id_Products = pDetails.Id_products });
            var details = await taskSearch;
            //var details = await taskSearch;
            ViewBag.top = pDetails.Top_Aux;
            ViewBag.Products = await productsBL.GetallAsync();
            return View(details);
        }

        // GET: DetailsController/Details/5
        public async  Task<IActionResult> Details(int id)
        {
            var details = await detailsBL.getByIdAsync(new Details { IdDetails = id });
            details.products = await productsBL.GetByIdAsync(new Products { Id_Products = details.Id_products });
            return View(details);
        }

        // GET: DetailsController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.products = await productsBL.GetallAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: DetailsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Details pDetails)
        {
            try
            {
                int result = await detailsBL.CreateAsync(pDetails);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex )
            {
                ViewBag.Error = "An error occurred while saving the entity changes: " + ex.InnerException?.Message;
                ViewBag.products = await productsBL.GetallAsync();
                return View(pDetails);
            }
        }

        // GET: DetailsController/Edit/5
        public async Task<IActionResult> Edit(Details pDetails)
        {
            var taskGetById = detailsBL.getByIdAsync(pDetails);
            var taskGetAllProducts = productsBL.GetallAsync();
            var details = await taskGetById;
            ViewBag.products = await taskGetAllProducts;
            ViewBag.Error = "";
            return View(details);
        }

        // POST: DetailsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Details pDetails)
        {
            try
            {
                int result = await detailsBL.ModifyAsync(pDetails);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.products = await productsBL.GetallAsync();
                return View(pDetails);
            }
        }

        // GET: DetailsController/Delete/5
        public async Task<IActionResult> Delete(Details pDetails)
        {
            var details = await detailsBL.getByIdAsync(pDetails);
            details.products = await productsBL.GetByIdAsync(new Products { Id_Products = details.Id_products });
            ViewBag.Error = "";
            return View(details);
        }

        // POST: DetailsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Details pDetails)
        {
            try
            {
                int reuslt = await detailsBL.DeleteAsync(pDetails);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var details = await detailsBL.getByIdAsync(pDetails);
                if (details == null)
                    details = new Details();
                if (details.IdDetails > 0)
                    details.products = await productsBL.GetByIdAsync(new Products { Id_Products = details.IdDetails });
                return View(details);
            }
        }
    }
}
