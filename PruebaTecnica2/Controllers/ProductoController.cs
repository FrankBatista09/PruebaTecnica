using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica2.Context;
using PruebaTecnica2.Dtos;
using PruebaTecnica2.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaTecnica2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly PruebaContext _context;

        public ProductoController (PruebaContext context)
        {
            _context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var prod = await _context.productos.ToListAsync();

            return Ok(prod);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var prod = await _context.productos.FindAsync(id);

            return Ok(prod);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> AddAsync(ProductoDTO productos)
        {

            var producto = new Productos
            {
                Nombre = productos.Nombre,
                Price = productos.Price,
            };
            _context.productos.Add(producto);
            await _context.SaveChangesAsync();

            return Ok(await _context.productos.ToListAsync());    
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Productos updatedProd, int id)
        {
            var prod = await _context.productos.FindAsync(id);
            if (prod == null) 
                return NotFound("Product not found");

            prod.Nombre = updatedProd.Nombre;
            prod.Price = updatedProd.Price;

            _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var prod = await _context.productos.FindAsync(id);
            if (prod == null)
                return NotFound("Product not found");

            _context.productos.Remove(prod);
            _context.SaveChangesAsync();

            return Ok();
        }
    }
}
