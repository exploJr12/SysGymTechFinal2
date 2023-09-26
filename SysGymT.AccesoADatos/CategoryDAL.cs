using Microsoft.EntityFrameworkCore;
using SysGymT.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.AccesoADatos
{
	public class CategoryDAL
	{
		public static async Task<int> CreateAsync(Category pCategory)
		{
			int result = 0;
			using (var bdContexto = new BDContexto())
			{
				bdContexto.Add(pCategory);
				result = await bdContexto.SaveChangesAsync();
			}
			return result;
		}
		public static async Task<int> ModifyAsync(Category pCategory)
		{
			int result = 0;
			using (var bdContexto = new BDContexto())
			{
				var category = await bdContexto.Category.FirstOrDefaultAsync(s => s.Id_Category == pCategory.Id_Category);
				category.Name_Category = pCategory.Name_Category;
				bdContexto.Update(category);
				result = await bdContexto.SaveChangesAsync();
			}
			return result;
		}
		public static async Task<int> DeleteAsync(Category pCategory)
		{
			int result = 0;
			using (var bdContexto = new BDContexto())
			{
				var category = await bdContexto.Category.FirstOrDefaultAsync(s => s.Id_Category == pCategory.Id_Category);
				bdContexto.Category.Remove(category);
				result = await bdContexto.SaveChangesAsync();
			}
			return result;
		}
		public static async Task<Category> GetByIdAsync(Category pCategory)
		{
			var category = new Category();
			using (var bdContexto = new BDContexto())
			{
				category = await bdContexto.Category.FirstOrDefaultAsync(s => s.Id_Category == pCategory.Id_Category);
			}
			return category;
		}
		public static async Task<List<Category>> GetAllAsync()
		{
			var mCategory = new List<Category>();
			using (var bdContexto = new BDContexto())
			{
				mCategory = await bdContexto.Category.ToListAsync();
			}
			return mCategory;
		}
		internal static IQueryable<Category> QuerySelect(IQueryable<Category> pQuery, Category pCategory)
		{
			if (pCategory.Id_Category > 0)
				pQuery = pQuery.Where(c => c.Id_Category == pCategory.Id_Category);
			if (!string.IsNullOrWhiteSpace(pCategory.Name_Category))
				pQuery = pQuery.Where(c => c.Name_Category.Contains(pCategory.Name_Category));
			pQuery = pQuery.OrderByDescending(c => c.Id_Category).AsQueryable();
			return pQuery;
		}
		public static async Task<List<Category>> SearchAsync(Category pCategory)
		{
			var mCategory = new List<Category>();
			using (var bdContexto = new BDContexto())
			{
				var select = bdContexto.Category.AsQueryable();
				select = QuerySelect(select, pCategory);
				mCategory = await select.ToListAsync();
			}
			return mCategory;
		}
	}
}
