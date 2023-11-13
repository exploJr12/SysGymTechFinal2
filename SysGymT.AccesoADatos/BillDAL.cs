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
    public class BillDAL
    {
        public static async Task<int> CreateAsync(Bill pBill)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pBill);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModifyAsync(Bill pBill)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var existingBill = await bdContexto.Bill.FirstOrDefaultAsync(b => b.Id_Bill == pBill.Id_Bill);
                if (existingBill != null)
                {
                    existingBill.Id_Usuario = pBill.Id_Usuario;
                    existingBill.Id_Customer = pBill.Id_Customer;
                    existingBill.Id_Products = pBill.Id_Products;
                    existingBill.Descriptions = pBill.Descriptions;
                    existingBill.Page_Type = pBill.Page_Type;
                    existingBill.Sale_Total = pBill.Sale_Total;

                    bdContexto.Update(existingBill);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("La Factura no existe");
                }
            }
            return result;
        }
        public static async Task<int> DeleteAsync(Bill pBill)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var existingBill = await bdContexto.Bill.FirstOrDefaultAsync(b => b.Id_Bill == pBill.Id_Bill);
                if (existingBill != null)
                {
                    bdContexto.Bill.Remove(existingBill);
                    result = await bdContexto.SaveChangesAsync();
                }
            }
            return result;
        }
        public static async Task<Bill> GetByIdAsync(Bill pBill)
        {
            var bill = new Bill();
            using (var bdContexto = new BDContexto())
            {
                bill = await bdContexto.Bill.FirstOrDefaultAsync(b => b.Id_Bill == pBill.Id_Bill);
            }
            return bill;
        }
        public static async Task<List<Bill>> GetAllAsync()
        {
            var bill = new List<Bill>();
            using (var bdContexto = new BDContexto())
            {
                bill = await bdContexto.Bill.ToListAsync();
            }
            return bill;
        }
        internal static IQueryable<Bill> QuerySelect(IQueryable<Bill> pQuery, Bill pBill)
        {
            if (pBill.Id_Bill > 0)
                pQuery = pQuery.Where(b => b.Id_Bill == pBill.Id_Bill);
            if (pBill.Id_Usuario > 0)
                pQuery = pQuery.Where(b => b.Id_Usuario == pBill.Id_Usuario);
            if (pBill.Id_Customer > 0)
                pQuery = pQuery.Where(b => b.Id_Customer == pBill.Id_Customer);
            if (pBill.Id_Products > 0)
                pQuery = pQuery.Where(b => b.Id_Products == pBill.Id_Products);
            if (!string.IsNullOrWhiteSpace(pBill.Descriptions))
                pQuery = pQuery.Where(b => b.Descriptions.Contains(pBill.Descriptions));
            if (!string.IsNullOrWhiteSpace(pBill.Page_Type))
                pQuery = pQuery.Where(b => b.Page_Type.Contains(pBill.Page_Type));
            if (pBill.Register_Date.HasValue)
            {
                DateTime fechaInicial = pBill.Register_Date.Value.Date;
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(b => b.Register_Date >= fechaInicial && b.Register_Date <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(b => b.Id_Bill).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Bill>> SearchAsync(Bill pBill)
        {
            var bill = new List<Bill>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Bill.AsQueryable();
                select = QuerySelect(select, pBill);
                bill = await select.ToListAsync();
            }
            return bill;
        }
        public static async Task<List<Bill>> SearchincludeUsuarioAsync(Bill pBill)
        {
            var bill = new List<Bill>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Bill.AsQueryable();
                select = QuerySelect(select, pBill).Include(b => b.Id_Usuario).AsQueryable();
                bill = await select.ToListAsync();
            }
            return bill;
        }
        public static async Task<List<Bill>> SearchincludeCustomerAsync(Bill pBill)
        {
            var bill = new List<Bill>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Bill.AsQueryable();
                select = QuerySelect(select, pBill).Include(b => b.Id_Customer).AsQueryable();
                bill = await select.ToListAsync();
            }
            return bill;
        }
        public static async Task<List<Bill>> SearchincludeProductsAsync(Bill pBill)
        {
            var bill = new List<Bill>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Bill.AsQueryable();
                select = QuerySelect(select, pBill).Include(b => b.Id_Products).AsQueryable();
                bill = await select.ToListAsync();
            }
            return bill;
        }
    }
}
