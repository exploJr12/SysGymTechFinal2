using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace SysGymT.EntidadesDeNegocio
{
    public class Machines
    {
        [Key]
        [Required]
        public int Id_Machines { get; set; }


        [ForeignKey("Usuario")]
        [Display(Name = "Usuario")]
        public int Id_Usuario { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La cantidad maxima de caracteres permitida es de 50")]
        [MinLength(3, ErrorMessage = "La cantidad minima de caracteres permitida es de 3")]
        [DisplayName("Maquinaria")]
        public string? Machines_Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "la cantidad maxima de carateres permitidos es de 30")]
        [DisplayName("Marca")]
        public string? Brand { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "La cantidad maxima de caracteres permitida es de 50")]
        [MinLength(3, ErrorMessage = "La cantidad minima de caracteres permitida es de 3")]
        [DisplayName("Codigo")]
        public string? Serial_Number { get; set; }


        [Required(ErrorMessage = "Estatus es obligatorio")]
        [DisplayName("Estado")]
        public bool Status { get; set; }


        [Display(Name = "Adquisicion")]
        public DateTime? Acquisition_Date { get; set; }


        [Display(Name = "Mantenimiento")]
        public DateTime? Maintenance_Date { get; set; }


        [Display(Name = "Siguiente Mantenimiento")]
        public DateTime? Next_Maintenance_Date { get; set; }
        public Usuario Usuario { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }

        public enum Status_Machine
        {
            ACTIVO = 1,
            INACTIVO = 2
        }
    }
}
