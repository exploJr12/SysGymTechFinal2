using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.EntidadesDeNegocio
{
    public class Membership
    {
        [Key]
        public int Id_Membership { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string Name_Mem { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Maximo 30 caracteres")]
        public string Description { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public List<Customer> Customer { get; set; }
    }
}
