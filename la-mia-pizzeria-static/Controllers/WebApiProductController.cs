using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace la_mia_pizzeria_static.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WebApiProductController : ControllerBase
    {
        // GET: api/WebApiProduct/GetProducts
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(ProductManager.GetProducts());
        }

        // GET: api/WebApiProduct/GetProductsByFilter?filter=NAPOLI
        [HttpGet]
        public IActionResult GetProductsByFilter(string? filter)
        {
            List<Product> listproducts = ProductManager.GetProducts();

            if (filter == null)
                return Ok(listproducts);

            var filteredProducts = listproducts.Where(p => p.Name.ToLower().Contains(filter.ToLower())).ToList();
            return Ok(filteredProducts);
        }


        // GET: api/WebApiProduct/GetPostByTitle/{name}-> SENZA GRAFFE MA IL NOME DIRETTO
        [HttpGet("{name}")]
        public IActionResult GetPostByTitle(string name)
        {
            var post = ProductManager.GetProductByTitle(name);
            if (post == null)
                return NotFound("ERRORE");
            return Ok(post);
        }
    }

}
