using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using la_mia_pizzeria_static.Models;
using static System.Net.Mime.MediaTypeNames;

namespace la_mia_pizzeria_static.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [MinWords(5)]
        public string Description { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        //public IFormFile ImageFile { get; set; }
        public string? Image { get; set; }

        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Range(2, 50, ErrorMessage = "il prezzo deve essere tra 2 e 50 euro")]
        public double Price { get; set; }
        
        public List<Ingredient>? Ingredients { get; set; }
        public Product() 
        {
          
        }
        public Product(string name, string description, string? image, double price)
        {
            Name = name;
            Description = description;
            Image = image ?? "~/img/Marghe-pizza-bufala.webp";
            Price = price;
        }

        public string GetCategory()
        {
            if (Category == null)
                return "Nessuna categoria";
            return Category.Name;

            // Versione più sintetica:
            //return Category?.Title ?? "Nessuna categoria";
        }
    }
}
