using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysGymT.AccesoADatos;
using SysGymT.EntidadesDeNegocio;

namespace SysGymT.LogicaDeNegocio
{
    public class ProductsBL
    {
        public async Task<int> CreateAsync(Products pProduct)
        {
            return await ProductsDAL.CreateAsync(pProduct);
        }
        public async Task<int> ModifyAsync(Products pProduct)
        {
            return await ProductsDAL.ModifyAsync(pProduct);
        }
        public async Task<int> DeleteAsync(Products pProduct)
        {
            return await ProductsDAL.DeleteAsync(pProduct);
        }
        public async Task<Products> GetByIdAsync(Products pProduct)
        {
            return await ProductsDAL.GetByIdAsync(pProduct);
        }
        public async Task<List<Products>> GetallAsync()
        {
            return await ProductsDAL.GetallAsync();
        }
        public async Task<List<Products>> SearchASync(Products pProduct)
        {
            //return await ProductsDAL.SearchASync(pProduct);
            return await ProductsDAL.SearchASync(pProduct);
        }
    }
}
