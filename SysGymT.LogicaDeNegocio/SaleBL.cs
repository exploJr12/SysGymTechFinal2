using SysGymT.AccesoADatos;
using SysGymT.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.LogicaDeNegocio
{
    public class SaleBL
    {
        public async Task<int> CreateAsync(Sale pSale)
        {
            return await SaleDAL.CreateAsync(pSale);
        }

        public async Task<int> ModifyAsync(Sale pSale)
        {
            return await SaleDAL.ModifyAsync(pSale);
        }

        public async Task<int> DeleteAsync(Sale pSale)
        {
            return await SaleDAL.DeleteAsync(pSale);
        }

        public async Task<Sale> GetByIdAsync(Sale pSale)
        {
            return await SaleDAL.GetByIdAsync(pSale);
        }

        public async Task<List<Sale>> GetAllAsync(Sale pSale)
        {
            return await SaleDAL.GetAllAsync();
            //return await SaleDAL.GetAllAsync();
        }   
        public async Task<List<Sale>> SearchIncludeProductsAsync(Sale pSale)
        {
            return await SaleDAL.SearchIncludeProductsAsync(pSale);
        }
    }
}