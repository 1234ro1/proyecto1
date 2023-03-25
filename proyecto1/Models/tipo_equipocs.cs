using System.ComponentModel.DataAnnotations;

namespace proyecto1.Models
{
    public class tipo_equipocs
    {
        [Key]
        public int? id_tipo_equipo { get; set; }
        public string? descripcion { get; set; }
        public string? estado { get; set; }
    }
}
