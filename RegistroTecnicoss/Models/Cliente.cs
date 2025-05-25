using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicoss.Models
{
    public class Cliente
    {

        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        public DateTime FechaIngreso { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        public string Nombres { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(150, ErrorMessage = "La dirección no puede superar los 150 caracteres")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El RNC es obligatorio")]
        [StringLength(11, ErrorMessage = "El RNC debe tener 11 caracteres")]
        public string Rnc { get; set; } = string.Empty;

        [Required(ErrorMessage = "El límite de crédito es obligatorio")]
        [Range(0, 1000000, ErrorMessage = "El límite de crédito no puede superar 1,000,000")]
        public decimal LimiteCredito { get; set; } = 1000000;

        [Required(ErrorMessage = "Debe seleccionar un técnico")]
        public int TecnicoId { get; set; }

        [ForeignKey("TecnicoId")]
        public Tecnico? Tecnico { get; set; }
    }
}
