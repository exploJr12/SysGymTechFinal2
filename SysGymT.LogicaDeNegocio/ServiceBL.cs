using SysGymT.AccesoADatos;
using SysGymT.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.LogicaDeNegocio
{
    public class ServiceBL
    {
        public async Task<int> CreateAsync(Service pService)
        {
            return await ServiceDAL.CreateAsync(pService);
        }
        public async Task<int> ModifyAsync(Service pService)
        {
            return await ServiceDAL.ModifyAsync(pService);
        }
        public async Task<int> DeleteAsync(Service pService)
        {
            return await ServiceDAL.DeleteAsync(pService);
        }
        public async Task<Service> GetByIdAsync(Service pService)
        {
            return await ServiceDAL.GetByIdAsync(pService);
        }
        public async Task<List<Service>> GetAllAsync()
        {
            return await ServiceDAL.GetAllAsync();
        }
        public async Task<List<Service>> SearchAsync(Service pService)
        {
            return await ServiceDAL.SearchAsync(pService);
        }
    }
}
