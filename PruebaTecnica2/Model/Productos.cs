using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica2.Model
{
    public class Productos
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public decimal Price { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
