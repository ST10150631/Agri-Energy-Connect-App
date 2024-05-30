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
        /// <summary>
        /// Will return the marketplace view based on the user role
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task<ActionResult> Marketplace()
        {
            if (CoreModel.UserRole == 1)
            {
                if(CoreModel.SignedInUser == null){return View();}
                List<ProductModel> productList = model.GetAllProducts().Result;
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
        //======================================================= End of Method ===================================================

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpGet]
        public ActionResult AddProduct()
        {
            var Product = new ProductModel();
            return View(Product);
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddProduct(ProductModel product, IFormFile file)
        {
            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    product.ProductImage = memoryStream.ToArray();
                }
            }
            await model.AddProductDB(product.ProductName, product.ProductDescription, product.ProductCategory, product.ProductionDate, product.ProductImage, product.ProductPrice, CoreModel.SignedInUser);
            return RedirectToAction("Marketplace");
        }
        //======================================================= End of Method ===================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        public async Task< ActionResult> DeleteProduct(int productId)
        {
            await model.RemoveProduct(productId);
            var productList = model.GetFarmerProducts().Result;
            return View("Marketplace", productList);
        }

        //======================================================= End of Method ===================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// ----------------------------------------------------- Start of Method ------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult OnFilterType(string type)
        {
            if (type != "All")
            {
                List<ProductModel> productList = model.FilterByType(type).Result;
                return View("Marketplace", productList);
            }
            else
            {
                List<ProductModel> productList = model.GetAllProducts().Result;
                return View("Marketplace", productList);
            }
        }

        //======================================================= End of Method ===================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ActionResult OnFilterDate(DateTime startDate, DateTime endDate)
        {
                List<ProductModel> productList = model.FilterByDate(startDate, endDate).Result;
                return View("Marketplace", productList);
        }



    }
}
