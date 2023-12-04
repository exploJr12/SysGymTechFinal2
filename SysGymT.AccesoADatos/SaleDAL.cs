using SysGymT.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.IdentityModel.Tokens;

namespace SysGymT.AccesoADatos
{
    public class SaleDAL
    {
        /* CREATE METHOD */
        public static async Task<int> CreateAsync(Sale sale)
        {
            try
            {
                int result = 0;
                using (var bdContext = new BDContexto())
                {
                    bdContext.Add(sale);
                    result = await bdContext.SaveChangesAsync();
                }
                return result;

            }
            catch (Exception)
            {
                throw new Exception("Error Create method in SaleDAL");
            }
        }


        /* MODIFY METHOD */
        public static async Task<int> ModifyAsync(Sale pSale)
        {
            try
            {
                int result = 0;
                using (var bdContext = new BDContexto())
                {
                    var findSale = await bdContext.Sale.FirstOrDefaultAsync(s => s.IdSale == pSale.IdSale);
                    if (findSale != null)
                    {
                        findSale.Id_Prodructs = pSale.Id_Prodructs;
                        findSale.AmountAditional = pSale.AmountAditional;
                        findSale.AmountPage = pSale.AmountPage;
                        findSale.RegisterDate = pSale.RegisterDate;
                        // ADD RELATIONS HERE

                        bdContext.Update(findSale);
                        result = await bdContext.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("La Venta no existe");
                    }
                }
                return result;

            }
            catch (Exception)
            {
                throw new Exception("Error Modify method in SaleDAL");
            }
        }


        /* DELETE METHOD */
        public static async Task<int> DeleteAsync(Sale sale)
        {
            try
            {
                int result = 0;
                using (var bdContext = new BDContexto())
                {
                    var findSale = await bdContext.Sale.FirstOrDefaultAsync(s => s.IdSale == sale.IdSale);
                    if (findSale != null)
                    {
                        bdContext.Sale.Remove(findSale);
                        result = await bdContext.SaveChangesAsync();
                    }
                }
                return result;

            }
            catch (Exception)
            {
                throw new Exception("Error in Delete method in SaleDAL");
            }
        }


        /* GET BY ID */

        public static async Task<Sale> GetByIdAsync(Sale sale)
        {
            try
            {
                var getSale = new Sale();
                using (var bdContext = new BDContexto())
                {
                    getSale = await bdContext.Sale.FirstOrDefaultAsync(s => s.IdSale == sale.IdSale);
                }
                return getSale;

            }
            catch (Exception)
            {
                throw new Exception("Error in GetById method in SaleDAL");
            }
        }


        /* GET ALL */

        public static async Task<List<Sale>> GetAllAsync()
        {
            try
            {
                var sales = new List<Sale>();
                using (var bdContext = new BDContexto())
                {
                    sales = await bdContext.Sale.ToListAsync();      
                }
                return sales;
            }
            catch (Exception)
            {
                throw new Exception("Error in GetAll method in SaleDAL");
            }
        }
        
        internal static IQueryable<Sale> QuerySelect(IQueryable<Sale> pQuery , Sale pSale)
        {
            if (pSale.IdSale > 0)
                pQuery = pQuery.Where(s => s.IdSale == pSale.IdSale);
            if (pSale.Id_Prodructs > 0)
                pQuery = pQuery.Where(s => s.Id_Prodructs == pSale.Id_Prodructs);
            if (pSale.AmountAditional > 0M)
                pQuery = pQuery.Where(s => s.AmountAditional == pSale.AmountAditional);
            if (pSale.TotalAmount > 0M)
                pQuery = pQuery.Where(s => s.TotalAmount == pSale.TotalAmount);
            if (pSale.AmountPage > 0M)
                pQuery = pQuery.Where(s => s.AmountPage == pSale.AmountPage);
            if (!string.IsNullOrWhiteSpace(pSale.Description))
                pQuery = pQuery.Where(s => s.Description.Contains(pSale.Description));
            if (pSale.RegisterDate.Year > 1000)
            {
                DateTime firstDate = new DateTime(pSale.RegisterDate.Year, pSale.RegisterDate.Month, pSale.RegisterDate.Day, 0, 0, 0);
                DateTime finalDate = firstDate.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.RegisterDate >= firstDate && s.RegisterDate <= finalDate);
            }
            pQuery = pQuery.OrderByDescending(s => s.IdSale).AsQueryable();
            if (pSale.Top_Aux > 0)
                pQuery = pQuery.Take(pSale.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Sale>> SearchIncludeProductsAsync(Sale pSale)
        {
            var sale = new List<Sale>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Sale.AsQueryable();
                select = QuerySelect(select, pSale).Include(s => s.Products).AsQueryable();
                sale = await select.ToListAsync();
            }
            return sale;
        }
    }
}