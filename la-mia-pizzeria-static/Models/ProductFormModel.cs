using la_mia_pizzeria_static.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace la_mia_pizzeria_static.Models
{
    public class ProductFormModel
    {
        public Product Product { get; set; }
        public List<Category>? Categories { get; set; }
        public List<SelectListItem>? Ingredients { get; set; } // Gli elementi dei tag selezionabili per la select (analogo a Categories)
        public List<string>? SelectedIngredients { get; set; } // Gli ID degli elementi effettivamente selezionati


        public ProductFormModel() { }

        public ProductFormModel(Product product, List<Category>? categories)
        {
            Product = product;
            Categories = categories;
        }

        public void CreateIngredients()
        {
            Ingredients = new List<SelectListItem>();
            SelectedIngredients = new List<string>();
            var ingredientsFromDB = ProductManager.GetIngredients();
            foreach (var ingredient in ingredientsFromDB)
            {
                bool isSelected = Product.Ingredients?.Any(i => i.Id == ingredient.Id) == true;
                Ingredients.Add(new SelectListItem() // lista degli elementi selezionabili
                {
                    Text = ingredient.Name, // Testo visualizzato
                    Value = ingredient.Id.ToString(), // SelectListItem vuole una generica stringa, non un int
                    Selected = isSelected // es. ingrediente1, ingrediente5, ingrediente9
                });
                if (isSelected)
                    SelectedIngredients.Add(ingredient.Id.ToString()); // lista degli elementi selezionati
            }
        }
    }
}
