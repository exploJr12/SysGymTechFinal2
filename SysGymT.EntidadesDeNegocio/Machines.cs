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

        [Required]
        [StringLength(50, ErrorMessage ="La cantidad maxima de caracteres permitida es de 50")]
        [MinLength(3, ErrorMessage ="Lacantidad minima de caracteres permitida es de 3")]
        public string? Machines_Name { get; set; }

        [StringLength(50, ErrorMessage ="la cantidad maxima de carateres permitidos es de 30")]
        public string? Brand { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
