using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmaciaWeb.Models
{
    public class Medicamento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del medicamento es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        [DisplayName("Nombre del Producto")]
        public string Nombre { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "Debe ingresar un precio")]
        [Range(0.01, 10000, ErrorMessage = "El precio debe ser mayor a 0")]
        [DisplayName("Precio Unitario")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Indique la cantidad en stock")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        [DisplayName("Stock Actual")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayName("Fecha de Vencimiento")]
        public DateTime FechaVencimiento { get; set; }

        // --- NUEVOS CAMPOS SEGÚN LA RÚBRICA ---
        [StringLength(250)]
        [DisplayName("Descripción")]
        public string? Descripcion { get; set; }

        [DisplayName("Estado")]
        public bool Activo { get; set; } = true; // Por defecto será "Activo" (true)
        // --------------------------------------

        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        [DisplayName("Categoría")]
        public int CategoriaId { get; set; }
        public virtual Categoria? Categoria { get; set; }

        [DisplayName("Estante/Ubicación")]
        public int? EstanteId { get; set; }
        public virtual Estante? Estante { get; set; }
    }
}