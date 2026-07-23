using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosApi.Data;
using ProductosApi.Models;

namespace ProductosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // 2. GET: api/productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _context.Productos.ToListAsync();
            return Ok(productos);
        }

        // 3. GET: api/productos/{id}  -> 404 si no existe
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound(new { mensaje = $"No se encontró el producto con Id {id}" });
            }

            return Ok(producto);
        }

        // 7. GET: api/productos/buscar?nombre=texto
        // Coincidencias parciales; lista vacía si no hay resultados.
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Producto>>> Buscar([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return Ok(new List<Producto>());
            }

            var productos = await _context.Productos
                .Where(p => p.Nombre.Contains(nombre))
                .ToListAsync();

            return Ok(productos);
        }

        // 4. POST: api/productos  -> valida y almacena en SQL Server
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        // 5. PUT: api/productos/{id}  -> actualiza un producto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest(new { mensaje = "El Id de la URL no coincide con el del producto" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existente = await _context.Productos.FindAsync(id);
            if (existente == null)
            {
                return NotFound(new { mensaje = $"No se encontró el producto con Id {id}" });
            }

            existente.Nombre = producto.Nombre;
            existente.Descripcion = producto.Descripcion;
            existente.Precio = producto.Precio;
            existente.Stock = producto.Stock;

            await _context.SaveChangesAsync();

            return Ok(existente);
        }

        // 6. DELETE: api/productos/{id}  -> elimina un producto existente
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound(new { mensaje = $"No se encontró el producto con Id {id}" });
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"Producto con Id {id} eliminado correctamente" });
        }
    }
}
