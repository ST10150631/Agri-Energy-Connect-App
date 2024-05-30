using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Models;

namespace PROG7311_POE_PART_2_ST10150631_MICHAEL_TURNER.Controllers
{
    public class MarketplaceController : Controller
    {
        private MarketplaceModel model = new MarketplaceModel();
        private ProductModel procuctModel = new ProductModel();
        // GET: MarketplaceController
        public async Task<ActionResult> Marketplace()
        {
            if (CoreModel.UserRole == 1)
            {
                if(CoreModel.SignedInUser == null){return View();}
                List<ProductModel> productList = await model.GetAllProducts();
                if(productList == null) {return View();}
                return View(productList);
            }
            else if(CoreModel.UserRole == 2)
            {
                if (CoreModel.SignedInUser == null) { return View(); }
                List<ProductModel> productList = await model.GetFarmerProducts();
                if (productList == null) { return View(); }
                return View(productList);
            }
            else
            {
                return View();
            }
        }

        // GET: MarketplaceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MarketplaceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarketplaceController/Create
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

        // GET: MarketplaceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MarketplaceController/Delete/5
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
