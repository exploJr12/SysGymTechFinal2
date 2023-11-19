using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SysGymT.EntidadesDeNegocio
{
    public class Customer
    {
        [Key]
        public int Id_Customer { get; set; }


        [Required]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        [DisplayName("Nombre")]
        public string Name_Customer { get; set; }


        [Required]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        [DisplayName("Apellido")]

        public string Last_Name { get; set; }


        [Required]
        [DisplayName("Dui")]
        public string DUI { get; set; }


        [Required]
        [DisplayName("Telefono")]
        public int Telephone { get; set; }


        [Required]
        [DisplayName("Peso")]
        public double Weight { get; set; }


        [Required]
        [DisplayName("Altura")]
        public double Height { get; set; }


        [Required]
        [DisplayName("Edad")]
        public int Age { get; set; }


        [Required]
        [DisplayName("Genero")]
        public string Gender { get; set; }


        [Required]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        [DisplayName("Membresia")]
        public string Membership { get; set; }


        public List<Bill> Bills { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }

}
