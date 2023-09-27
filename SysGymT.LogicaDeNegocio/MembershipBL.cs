using SysGymT.AccesoADatos;
using SysGymT.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.LogicaDeNegocio
{
    public class MembershipBL
    {
        public async Task<int> CreateAsync(Membership pMemberhip)
        {
            return await MembershipDAL.CreateAsync(pMemberhip);
        }
        public async Task<int> ModifyAsync(Membership pMemberhip)
        {
            return await MembershipDAL.ModifyAsync(pMemberhip);
        }
        public async Task<int> DeleteAsync(Membership pMemberhip)
        {
            return await MembershipDAL.DeleteAsync(pMemberhip);
        }
        public async Task<Membership> GetByIdAsync(Membership pMemberhip)
        {
            return await MembershipDAL.GetByIdAsync(pMemberhip);
        }
        public async Task<List<Membership>> GetAllAsync()
        {
            return await MembershipDAL.GetAllAsync();
        }
        public async Task<List<Membership>> SearchAsync(Membership pMemberhip)
        {
            return await MembershipDAL.SearchAsync(pMemberhip);
        }
    }
}
