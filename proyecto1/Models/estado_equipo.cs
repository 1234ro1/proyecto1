using System.ComponentModel.DataAnnotations;
namespace proyecto1.Models
{
    public class estado_equipo
    {
        [Key]
        public int? id_estados_equipo { get; set; }
        public string? descripcion { get; set; }
        public string? estado { get; set; }
    }
}
