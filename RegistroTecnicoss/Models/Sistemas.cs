namespace RegistroTecnicoss.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace RegistroTecnicos.Models
    {
        public class Sistemas
        {
            [Key]
            public int SistemaId { get; set; }

            [Required(ErrorMessage = "La descripción es obligatoria")]
            [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
            public string Descripcion { get; set; } = string.Empty;

            [Required(ErrorMessage = "La complejidad es obligatoria")]
            [RegularExpression("Alta|Media|Baja", ErrorMessage = "Valores válidos: Alta, Media, Baja")]
            public string Complejidad { get; set; } = string.Empty;

            [Required(ErrorMessage = "El precio es obligatorio")]
            [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
            [Column(TypeName = "decimal(18,2)")]
            public decimal Precio { get; set; }

            [Required(ErrorMessage = "La existencia es obligatoria")]
            [Range(0, int.MaxValue, ErrorMessage = "La existencia no puede ser negativa")]
            public int Existencia { get; set; }

            [Required(ErrorMessage = "La fecha es obligatoria")]
            [DataType(DataType.Date)]
            public DateTime FechaCreacion { get; set; } = DateTime.Now;
        }
    }
}
