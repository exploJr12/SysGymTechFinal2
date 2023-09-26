using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.EntidadesDeNegocio
{
	public class Category
	{
		[Key]
		[Required]
		public int Id_Category { get; set; }
		[Required]
		public string Name_Category { get; set; }

		[NotMapped]
		public List<Products> products { get; set; }

	}
}
