using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
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

        // GET: api/WebApiProduct/GetProductById?id=1
        [HttpGet]
        public IActionResult GetProductById(int id) 
        {
            var product = ProductManager.GetProduct(id);
            if (product == null)
                return NotFound("ERRORE");
            return Ok(product);
        }

        // POST: api/WebApiProduct/CreatePost

            //  Clicca su Body.
            //  Seleziona raw.
            //  Cambia il tipo di input a JSON (dal menu a discesa a destra del campo di input raw).

            //  Inserisci un JSON che rappresenta il prodotto da creare:
            //  {
            //      "Id": 1005,
            //      "Name": "Nuovo Prodotto",
            //      "Description": "Descrizione del nuovo prodotto",
            //      "Price": 9.99
            //  }

        [HttpPost]
        public IActionResult CreatePost([FromBody] Product product)
        {
            ProductManager.InsertProduct(product, null);
            return Ok();
        }

        // PUT: api/WebApiProduct/UpdatePost/{id}-> SENZA GRAFFE MA IL ID DIRETTO
        // Poi come il create (body, raw, jason ...)

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] Product product)
        {
            var oldProduct = ProductManager.GetProduct(id);
            if (oldProduct == null)
                return NotFound("ERRORE");
            ProductManager.UpdateProduct(id, product.Name, product.Description, product.Price, product.CategoryId, null);
            return Ok();
        }

        // DELETE: api/WebApiProduct/DeleteProduct/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            bool found = ProductManager.DeleteProduct(id);
            if (found)
                return Ok();
            return NotFound();
        }
    }

}
