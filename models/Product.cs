// Models/Product.cs
namespace StoreApi.Models
{
    public class Product
    {
        public int Id { get; set; } // ID único del producto
        public string Name { get; set; } // Nombre del producto
        public decimal Price { get; set; } // Precio del producto
        public string Description { get; set; } // Descripción del producto
        
        // Relación con la tabla de categorías
        public int CategoryId { get; set; }
        public Category? Category { get; set; } // Propiedad de navegación para la categoría
    }
}
