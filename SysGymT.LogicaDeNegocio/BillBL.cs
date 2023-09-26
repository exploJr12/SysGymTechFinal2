using SysGymT.AccesoADatos;
using SysGymT.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.LogicaDeNegocio
{
    public class BillBL
    {
        public async Task<int> CreateAsync(Bill pBill)
        {
            return await BillDAL.CreateAsync(pBill);
        }
        public async Task<int> ModifyAsync(Bill pBill)
        {
            return await BillDAL.ModifyAsync(pBill);
        }
        public async Task<int> DeleteAsync(Bill pBill)
        {
            return await BillDAL.DeleteAsync(pBill);
        }
        public async Task<Bill> GetByIdAsync(Bill pBill)
        {
            return await BillDAL.GetByIdAsync(pBill);
        }
        public async Task<List<Bill>> GetAllAsync(Bill pBill)
        {
            //return await MachinesDAL.GetAllAsync();
            return await BillDAL.GetAllAsync();
        }
        public async Task<List<Bill>> SearchAsync(Bill pBill)
        {
            return await BillDAL.SearchAsync(pBill);
        }
        public async Task<List<Bill>> SearchIncludeUsuarioAsync(Bill pBill)
        {
            return await BillDAL.SearchincludeUsuarioAsync(pBill);
        }
        public async Task<List<Bill>> SearchIncludeCustomerAsync(Bill pBill)
        {
            return await BillDAL.SearchincludeCustomerAsync(pBill);
        }
        public async Task<List<Bill>> SearchIncludeProductsAsync(Bill pBill)
        {
            return await BillDAL.SearchincludeProductsAsync(pBill);
        }
    }
}
