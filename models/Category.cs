// Models/Category.cs

namespace StoreApi.Models
{
    public class Category
    {
        public int Id { get; set; } // ID único de la categoría
        public string Name { get; set; } // Nombre de la categoría
        
        // Relación con los productos: Una categoría tiene muchos productos
        public ICollection<Product> Products { get; set; }
    }
}
