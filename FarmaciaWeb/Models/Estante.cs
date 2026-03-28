using System.ComponentModel.DataAnnotations;

namespace FarmaciaWeb.Models
{
    public class Estante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        public string Ubicacion { get; set; }

        public string Descripcion { get; set; }
    }
}