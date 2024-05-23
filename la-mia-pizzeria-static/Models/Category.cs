namespace la_mia_pizzeria_static.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public Category() { }
        public Category(string name)
        {
            Name = name;
        }
        
    }
}
