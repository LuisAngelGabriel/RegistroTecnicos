using System.ComponentModel.DataAnnotations;

namespace RegistroTecnicoss.Models
{
    public class Tecnicos
    {
        [Key]
        public int TecnicoId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]

        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]

        public double SueldoHora { get; set; }

    }
}
