using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq.Mapping;

namespace WinContador.Entity
{
    [Table("Juegos")]
    public class JuegoEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public DateTime FechaLanzamiento { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }

        public virtual CategoriaEntity Categoria { get; set; }
    }
}
