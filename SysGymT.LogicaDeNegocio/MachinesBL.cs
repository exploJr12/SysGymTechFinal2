using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysGymT.AccesoADatos;
using SysGymT.EntidadesDeNegocio;

namespace SysGymT.LogicaDeNegocio
{
    public class MachinesBL
    {
        public async Task<int> CreateAsync(Machines pMachines)
        {
            return await MachinesDAL.CreateASync(pMachines);
        }
        public async Task<int> ModifyAsync(Machines pMachines)
        {
            return await MachinesDAL.ModifyAsync(pMachines);
        }
        public async Task<int> DeleteAsync(Machines pMachines)
        {
            return await MachinesDAL.DeleteAsync(pMachines);
        }
        public async Task<Machines> GetByIdAsync(Machines pMachines)
        {
            return await MachinesDAL.GetByIdAsync(pMachines);
        }
        public async Task<List<Machines>> GetAllAsync(Machines pMachines)
        {
            //return await MachinesDAL.GetAllAsync();
            return await MachinesDAL.GetAllAsync();
        }
        public async Task<List<Machines>> SearchAsync(Machines pMachines)
        {
            return await MachinesDAL.SearchAsync(pMachines);
            //return await MachinesDAL.SearchAsync(pMachines);
        }
        public async Task<List<Machines>> SearchIncludeUsuarioAsync(Machines pMachines)
        {
            return await MachinesDAL.SearchIncludeUsuarioAsync(pMachines);
        }
    }
}
