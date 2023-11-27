using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysGymT.EntidadesDeNegocio;

namespace SysGymT.AccesoADatos
{
    public class DetailsDAL
    {

        /* CREATE METHOD */
        public static async Task<int> Create(Details pDetails)
        {
            try
            {
                int result = 0;
                using (var bdContext = new BDContexto())
                {
                    bdContext.Add(pDetails);
                    result = await bdContext.SaveChangesAsync();
                }
                return result;

            }
            catch (Exception)
            {
                throw new Exception("Error in create method DetailsDAL");
            }
        }


        /* MODIFY METHOD */
        public static async Task<int> Modify(Details pDetails)
        {
            try
            {
                int result = 0;
                using (var bdContext = new BDContexto())
                {
                    var details = await bdContext.Details.FirstOrDefaultAsync(d => d.IdDetails == pDetails.IdDetails);
                    details.SubTotal = pDetails.SubTotal;
                    details.SalePrice = pDetails.SalePrice;
                    details.Quantity = pDetails.Quantity;
                    details.CreationsDate = pDetails.CreationsDate;
                    bdContext.Update(details);
                    result = await bdContext.SaveChangesAsync();
                }
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Error in modify method DetailsDAL");
            }
        }


        /* DELETE METHOD */

        public static async Task<int> Delete(Details pDetails)
        {
            try
            {
                int result = 0;
                using (var bdContext = new BDContexto())
                {
                    var details = await bdContext.Details.FirstOrDefaultAsync(d => d.IdDetails == pDetails.IdDetails);
                    bdContext.Details.Remove(details);
                    result = await bdContext.SaveChangesAsync();
                }
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Error in Delete method DetailsDAL");
            }
        }

        /* GET BY ID */

        public static async Task<Details> GetById(Details details)
        {
            try
            {
                var getDetails = new Details();
                using (var bdContext = new BDContexto())
                {
                    getDetails = await bdContext.Details.FirstOrDefaultAsync(d => d.IdDetails == details.IdDetails);
                }
                return getDetails;
            }
            catch (Exception)
            {
                throw new Exception("Error in GetById method of DetailsDAL");
            }
        }
        internal static IQueryable<Details> QuerySelect(IQueryable<Details> pQuery, Details pDetails)
        {
            if (pDetails.IdDetails > 0)
                pQuery = pQuery.Where(d => d.IdDetails == pDetails.IdDetails);
            if (pDetails.Id_products > 0)
                pQuery = pQuery.Where(d => d.Id_products == pDetails.Id_products);
            if (pDetails.SalePrice > 0M)
                pQuery = pQuery.Where(s => s.SalePrice == pDetails.SalePrice);
            if (pDetails.SubTotal > 0M)
                pQuery = pQuery.Where(s => s.SubTotal == pDetails.SubTotal);
            if (pDetails.Quantity > 0)
                pQuery = pQuery.Where(s => s.Quantity == pDetails.Quantity);
            if (pDetails.CreationsDate.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pDetails.CreationsDate.Year, pDetails.CreationsDate.Month, pDetails.CreationsDate.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.CreationsDate >= fechaInicial && s.CreationsDate <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.IdDetails).AsQueryable();
            if (pDetails.Top_Aux >0)
                pQuery = pQuery.Take(pDetails.Top_Aux).AsQueryable();
            return pQuery;
        }

        public static async Task<List<Details>> GetAll()
        {
            try
            {
                var details = new List<Details>();
                using (var bdContext = new BDContexto())
                {
                    details = await bdContext.Details.AsQueryable().ToListAsync();      //.Include(s => s.ola)
                }
                return details;
            }
            catch (Exception)
            {
                throw new Exception("Error in GetAll method in DetailsDAL");
            }
        }
        public static async Task<List<Details>> SearchAllasync (Details pDetails)
        {
            try
            {
                var details = new List<Details>();
                using (var bdContexo = new BDContexto())
                {
                    var select = bdContexo.Details.AsQueryable();
                    select = QuerySelect(select, pDetails);
                    details = await select.ToListAsync();
                }
                return details;
            }
            catch (Exception)
            {
                throw new Exception("Error in SearchAllsync in DetailsDAl");
            }
        }
        public static async Task<List<Details>> SearchIncludeProductsAsync (Details pDetails)
        {
            try
            {
                var details = new List<Details>();
                using (var bdContexto  = new BDContexto())
                {
                    var select = bdContexto.Details.AsQueryable();
                        select = QuerySelect(select, pDetails).Include(s => s.Products).AsQueryable();
                    details = await select.ToListAsync();
                }
                return details;
            }
            catch
            {
                throw new Exception("Error in SearchIncludeProductsAsync in DetailsDAl");
            }
        }
    }
}