using SysGymT.AccesoADatos;
using SysGymT.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.LogicaDeNegocio
{
	public class CategoryBL
	{
		public async Task<int> CreateAsync(Category pCategory)
		{
			return await CategoryDAL.CreateAsync(pCategory);
		}
		public async Task<int> ModifyAsync(Category pCategory)
		{
			return await CategoryDAL.ModifyAsync(pCategory);
		}
		public async Task<int> DeleteAsync(Category pCategory)
		{
			return await CategoryDAL.DeleteAsync(pCategory);
		}
		public async Task<Category> GetByIdAsync(Category pCategory)
		{
			return await CategoryDAL.GetByIdAsync(pCategory);
		}
		public async Task<List<Category>> GetAllAsync()
		{
			return await CategoryDAL.GetAllAsync();
		}
		public async Task<List<Category>> SearchAsync(Category pCategory)
		{
			return await CategoryDAL.SearchAsync(pCategory);
		}
	}
}
