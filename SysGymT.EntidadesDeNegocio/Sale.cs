using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Globalization;

namespace SysGymT.EntidadesDeNegocio
{
    public class Sale
    {
        [Key]
        public int IdSale { get; set; }
        [DisplayName("Usuario")]
        public int Id_Usuario { get; set; }

        [DisplayName("Cliente")]
        public int Id_Customer { get; set; }
        [Required]
        [DisplayName("Detalles de venta")]
        public int Id_Prodructs { get; set; }
        [Required]
        [DisplayName("Monto a pagar")]
        public decimal AmountPage { get; set; }
        [Required]
        [DisplayName("Monto de cambio")]
        public decimal AmountChanges { get; set; }
        [Required]
        [DisplayName("Monto total")]
        public decimal TotalAmount { get; set; }
        public DateTime RegisterDate { get; set; }
        public Products Products { get; set; }
        public Customer Customers { get; set; }
        public Usuario Usuario { get; set; }
        [NotMapped]
        public int Top_Aux { get; set; }
    }
}