using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysGymT.EntidadesDeNegocio
{
    public class Machines
    {
        [Key]
        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Unicamente se permiten valores numericos.")]
        public int Id_Machines { get; set; }
        [ForeignKey("Usuario")]
        [Display(Name = "Usuario")]
        public int Id_Usuario { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La cantidad maxima de caracteres permitida es de 50")]
        [MinLength(3, ErrorMessage = "La cantidad minima de caracteres permitida es de 3")]
        public string? Machines_Name { get; set; }

        [StringLength(50, ErrorMessage = "la cantidad maxima de carateres permitidos es de 30")]
        public string? Brand { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La cantidad maxima de caracteres permitida es de 50")]
        [MinLength(3, ErrorMessage = "La cantidad minima de caracteres permitida es de 3")]
        public string? Serial_Number { get; set; }
        [Required(ErrorMessage = "Estatus es obligatorio")]
        public bool Status { get; set; }
        [Display(Name = "Fecha adquisicion")]
        public DateTime? Acquisition_Date { get; set; }
        [Display(Name = "Fecha de mantenimiento")]
        public DateTime? Maintenance_Date { get; set; }
        [Display(Name = "Fecha proxima")]
        public DateTime? Next_Maintenance_Date { get; set; }
        public Usuario usuario { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
