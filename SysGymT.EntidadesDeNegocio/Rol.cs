using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SysGymT.EntidadesDeNegocio
{
    public class Rol
    {
        [Key]
        public int Id_Rol { get; set; }
        [Required(ErrorMessage = "Nombre es obligatorio")]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string Name { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
        public List<User> User { get; set; }
    }
}
