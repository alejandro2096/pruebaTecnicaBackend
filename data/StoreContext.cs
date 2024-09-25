using Microsoft.EntityFrameworkCore;
using StoreApi.Models; // Asegúrate de usar el espacio de nombres correcto para tus modelos

namespace StoreApi.Data // Este espacio de nombres debe coincidir con el que usas en los archivos de migración
{
    public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
    }
}
