using Microsoft.EntityFrameworkCore;
using PruebaTecnica2.Model;

namespace PruebaTecnica2.Context
{
    public class PruebaContext : DbContext
    {
        public DbSet<Productos> productos { get; set; }
        public PruebaContext(DbContextOptions<PruebaContext> options) : base(options)
        {

        }

        
    }
}
