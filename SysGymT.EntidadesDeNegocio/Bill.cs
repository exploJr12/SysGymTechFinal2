using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.EntidadesDeNegocio
{
    public class Bill
    {
        [Key]
        public int Id_Bill { get; set; }
        [ForeignKey("Usuario")]
        [Required(ErrorMessage = "Usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public int Id_Usuario { get; set; }
        [ForeignKey("Customer")]
        [Required(ErrorMessage = "Cliente es obligatorio")]
        [Display(Name = "Cliente")]
        public int Id_Customer { get; set; }
        [ForeignKey("Products")]
        [Required(ErrorMessage = "Producto es obligatorio")]
        [Display(Name = "Producto")]
        public int Id_Products { get; set; }
        [Required(ErrorMessage = "La descripcion es obligatoria")]
        [StringLength(30, ErrorMessage = "Maximo 500 caracteres")]
        public string Descriptions { get; set; }
        [Required(ErrorMessage = "EL tipo de pago es obligatorio")]
        public string Page_Type { get; set; }
        [Required]
        public decimal Sale_Total { get; set; }
        public DateTime Register_Date { get; set; }
    }
}
