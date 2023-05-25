using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysGymT.EntidadesDeNegocio
{
    public class Products
    {
        [Key]
        [Required]
        public int Id_products { get; set; }

        [Required]
        public int Code { get; set; }

        [Required]
        [StringLength(25)]
        public String Product_Name { get; set; }

        [StringLength(50)]
        public String Description { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        [StringLength(25)]
        public String Type_Product { get; set; }

        [Required]
        public int Existence { get; set; }

        [Required]
        [StringLength(25)]
        public String Brand { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }
    }
}
