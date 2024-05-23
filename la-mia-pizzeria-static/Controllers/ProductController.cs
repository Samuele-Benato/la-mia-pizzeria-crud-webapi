using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }  
        
        public IActionResult Index(int? categoryId)
        {
            if (!categoryId.HasValue)
            {
                return View(ProductManager.GetProducts());
            }
            else
            {
                var products = ProductManager.GetProducts().Where(p => p.CategoryId == categoryId.Value);
                return View(products);
            }
        }

        [Authorize(Roles = "ADMIN,USER")]
        public IActionResult Details(int id)
        {
            var pizza = ProductManager.GetProduct(id);
            if (pizza != null)
                return View(pizza);
            else
                return View("errore");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            ProductFormModel model = new ProductFormModel();
            model.Product = new Product();
            model.Categories = ProductManager.GetCategories();
            model.CreateIngredients();
            return View(model);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.Categories = ProductManager.GetCategories();
                data.CreateIngredients();

                return View("Create", data);
            }

            ProductManager.InsertProduct(data.Product, data.SelectedIngredients);
           
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            var productToEdit = ProductManager.GetProduct(id);

            if (productToEdit == null)
            {
                return NotFound();
            }
            else
            {
                ProductFormModel model = new ProductFormModel(productToEdit, ProductManager.GetCategories());
                model.CreateIngredients();
                return View(model);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, ProductFormModel data)
        {           

            if (!ModelState.IsValid)
            {
                data.Categories = ProductManager.GetCategories();
                data.CreateIngredients();
                return View("Update", data);
            }

            
            if (ProductManager.UpdateProduct(
                id, 
                data.Product.Name,
                data.Product.Description,
                data.Product.Price,
                data.Product.CategoryId,
                data.SelectedIngredients))
                return RedirectToAction("Index");
            else
                return NotFound();

        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (ProductManager.DeleteProduct(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
