namespace la_mia_pizzeria_static.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public Ingredient() { }

        public Ingredient(string name) 
        {
            Name = name;
        }
    }
}
