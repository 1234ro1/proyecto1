using System.ComponentModel.DataAnnotations;
namespace proyecto1.Models
{
    public class carrera
    {
        [Key]
        public int? carrera_id { get; set; }
        public string? nombre_carrera { get; set; }
        public int? facultad_id { get; set; }
        public string estado { get; set; }

    }
}
