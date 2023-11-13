using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.EntidadesDeNegocio
{
    public class Customer
    {
        [Key]
        public int Id_Customer { get; set; }
        [ForeignKey("Membresia")]
        [Required(ErrorMessage = "Membresia es obligatoria")]
        [Display(Name = "Membresia")]
        public int Id_Membership { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string Name_Customer { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string Last_Name { get; set; }
        [Required]
        public int DUI { get; set; }
        [Required]
        public int Telephone { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        public Membership Membership { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
