using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysGymT.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace SysGymT.AccesoADatos
{
    public class ProductsDAL
    {
        public static async Task<int> CreateAsync(Products pProduct)
        {
            int result = 0;
            using (var bdContext = new BDContexto())
            {
                bdContext.Add(pProduct);
                result = await bdContext.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModifyAsync(Products pProduct)
        {
            int result = 0;
            using (var bdContext = new BDContexto())
            {
                var products = await bdContext.Products.FirstOrDefaultAsync(s => s.Id_Products == pProduct.Id_Products);
                products.Products_Name = pProduct.Products_Name;
                bdContext.Update(products);
                result = await bdContext.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> DeleteAsync(Products pProduct)
        {
            int result = 0;
            using (var bdContext = new BDContexto())
            {
                var products = await bdContext.Products.FirstOrDefaultAsync(s => s.Id_Products == pProduct.Id_Products);
                bdContext.Products.Remove(products);
                result = await bdContext.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Products> GetByIdAsync(Products pProduct)
        {
            var products = new Products();
            using (var bdContext = new BDContexto())
            {
                products = await bdContext.Products.FirstOrDefaultAsync(s => s.Id_Products == pProduct.Id_Products);
            }
            return products;
        }
        public static async Task<List<Products>> GetallAsync()
        {
            var products = new List<Products>();
            using (var bdContext = new BDContexto())
            {
                products = await bdContext.Products.ToListAsync();
            }
            return products;
        }
        internal static IQueryable<Products> QuerySelect(IQueryable<Products> pQuery, Products pProduct)
        {
            if (pProduct.Id_Products > 0)
                pQuery = pQuery.Where(s => s.Id_Products == pProduct.Id_Products);
            if (!string.IsNullOrWhiteSpace(pProduct.Products_Name))
                pQuery = pQuery.Where(s => s.Products_Name.Contains(pProduct.Products_Name));
            pQuery = pQuery.OrderByDescending(s => s.Id_Products).AsQueryable();
            if (pProduct.Top_Aux > 0)
                pQuery = pQuery.Take(pProduct.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Products>> SearchASync(Products pProduct)
        {
            var products = new List<Products>();
            using (var bdContext = new BDContexto())
            {
                var select = bdContext.Products.AsQueryable();
                select = QuerySelect(select, pProduct);
                products = await select.ToListAsync();
            }
            return products;
        }
    }
}
