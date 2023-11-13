using SysGymT.EntidadesDeNegocio;
using SysGymT.AccesoADatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.LogicaDeNegocio
{
    public class CustomerBL
    {
        public async Task<int> CreateAsync(Customer pCustomer)
        {
            return await CustomerDAL.CreateAsync(pCustomer);
        }
        public async Task<int> ModifyAsync(Customer pCustomer)
        {
            return await CustomerDAL.ModifyAsync(pCustomer);
        }
        public async Task<int> DeleteAsync(Customer pCustomer)
        {
            return await CustomerDAL.DeleteAsync(pCustomer);
        }
        public async Task<Customer> GetByIdAsync(Customer pCustomer)
        {
            return await CustomerDAL.GetByIdAsync(pCustomer);
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            return await CustomerDAL.GetAllAsync();
        }
        public async Task<List<Customer>> SearchAsync(Customer pCustomer)
        {
            return await CustomerDAL.SearchAsync(pCustomer);
            //return await CustomerDAL.SearchAsync(pCustomer);
        }
    }
}
