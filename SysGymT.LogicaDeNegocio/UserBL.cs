using SysGymT.AccesoADatos;
using SysGymT.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.LogicaDeNegocio
{
    public class UserBL
    {
        public async Task<int> CreateAsync(User pUser)
        {
            return await UserDAL.CreateAsync(pUser);
        }
        public async Task<int> ModifyAsync(User pUser)
        {
            return await UserDAL.ModifyAsync(pUser);
        }
        public async Task<int> DeleteAsync(User pUser)
        {
            return await UserDAL.DeleteAsync(pUser);
        }
        public async Task<User> GetByIdAsync(User pUser)
        {
            return await UserDAL.GetByIdAsync(pUser);
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await UserDAL.GetAllAsync();
        }
        public async Task<List<User>> SearchAsync(User pUser)
        {
            return await UserDAL.SearchAsync(pUser);
        }
        public async Task<User> LoginAsync(User pUser)
        {
            return await UserDAL.LoginAsync(pUser);
        }
        public async Task<int> ChangesPasswordAsync(User pUser, string pPasswordAnt)
        {
            return await UserDAL.ChangesPasswordAsync(pUser, pPasswordAnt);
        }
        public async Task<List<User>> SearchIncluedeRolesAsync(User pUser)
        {
            return await UserDAL.SearchIncludeRolesAsync(pUser);
        }
    }
}
