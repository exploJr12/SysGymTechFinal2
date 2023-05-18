using SysGymT.AccesoADatos;
using SysGymT.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.LogicaDeNegocio
{
    public class RolBL
    {
        public async Task<int> CreateAsync(Rol pRol)
        {
            return await RolDAL.CreateAsync(pRol);
        }
        public async Task<int> ModifyAsync(Rol pRol)
        {
            return await RolDAL.ModifyAsync(pRol);
        }
        public async Task<int> DeleteAsync(Rol pRol)
        {
            return await RolDAL.DeleteAsync(pRol);
        }
        public async Task<Rol> GetByIdAsync(Rol pRol)
        {
            return await RolDAL.GetByIdAsync(pRol);
        }
        public async Task<List<Rol>> GetAllAsync()
        {
            return await RolDAL.GetAllAsync();
        }
        public async Task<List<Rol>> SearchAsync(Rol pRol)
        {
            //return await RolDAL.SearchAsync(pRol);
            return await RolDAL.SearchAsync(pRol);
        }
    }
}
