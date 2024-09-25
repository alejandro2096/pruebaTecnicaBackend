using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Data;
using StoreApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreApi.Controllers
{
    [ApiController]  // Marca la clase como un controlador de API
    [Route("api/products")]  // Define la ruta base: api/products
    public class ProductController(StoreContext context) : ControllerBase
    {
        private readonly StoreContext _context = context;  // El DbContext inyectado para interactuar con la base de datos

        // GET: api/products
        // Método para obtener todos los productos
        // [HttpGet]
        //  public IActionResult Get()
        // {
        //     return Ok("hola Mundo");
        // }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return Ok(products);  // Retorna los productos en formato JSON
        }

        // GET: api/products/{id}
        // Método para obtener un producto por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category)
                                                 .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();  // Retorna 404 si no se encuentra el producto
            }

            return Ok(product);  // Retorna el producto encontrado
        }

        // POST: api/products
        // Método para crear un nuevo producto
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            // Añadimos el producto al DbContext
            _context.Products.Add(product);
            await _context.SaveChangesAsync();  // Guardamos los cambios en la base de datos

            // Retorna el producto creado con el código 201 (Created)
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/products/{id}
        // Método para actualizar un producto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)  // Verificamos si el ID de la URL coincide con el ID del producto
            {
                return BadRequest();  // Retorna un 400 si los IDs no coinciden
            }

            // Marcamos el producto como modificado en el contexto
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();  // Guardamos los cambios
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))  // Verifica si el producto existe
                {
                    return NotFound();  // Retorna 404 si el producto no existe
                }
                else
                {
                    throw;  // Relanzamos la excepción si ocurre otro error
                }
            }

            return NoContent();  // Retorna 204 si la actualización fue exitosa
        }

        // DELETE: api/products/{id}
        // Método para eliminar un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);  // Busca el producto por su ID

            if (product == null)
            {
                return NotFound();  // Retorna 404 si el producto no se encuentra
            }

            _context.Products.Remove(product);  // Eliminamos el producto del contexto
            await _context.SaveChangesAsync();  // Guardamos los cambios en la base de datos

            return NoContent();  // Retorna 204 si la eliminación fue exitosa
        }

        // Método auxiliar privado para verificar si un producto existe
        private bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    }
}
