// Models/WishlistItem.cs

namespace StoreApi.Models
{
    public class WishlistItem
    {
        public int Id { get; set; } // ID único de la lista de deseos
        public int ProductId { get; set; } // ID del producto deseado
        public Product Product { get; set; } // Relación con la entidad de producto
        
        public string UserId { get; set; } // ID del usuario
    }
}
