using Microsoft.EntityFrameworkCore;
using SysGymT.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.AccesoADatos
{
    public class MembershipDAL
    {
        public static async Task<int> CreateAsync(Membership pMembership)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pMembership);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModifyAsync(Membership pMembership)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var membership = await bdContexto.Membership.FirstOrDefaultAsync(m => m.Id_Membership == pMembership.Id_Membership);
                membership.Name_Mem = pMembership.Name_Mem;
                membership.Description = pMembership.Description;
                membership.Cost = pMembership.Cost;
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> DeleteAsync(Membership pMembership)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var membership = await bdContexto.Membership.FirstOrDefaultAsync(m => m.Id_Membership == pMembership.Id_Membership);
                bdContexto.Membership.Remove(membership);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Membership> GetByIdAsync(Membership pMembership)
        {
            var membership = new Membership();
            using (var bdContexto = new BDContexto())
            {
                membership = await bdContexto.Membership.FirstOrDefaultAsync(m => m.Id_Membership == pMembership.Id_Membership);
            }
            return membership;
        }
        public static async Task<List<Membership>> GetAllAsync()
        {
            var memberships = new List<Membership>();
            using (var bdContexto = new BDContexto())
            {
                memberships = await bdContexto.Membership.ToListAsync();
            }
            return memberships;
        }
        internal static IQueryable<Membership> QuerySelect(IQueryable<Membership> pQuery, Membership pMembership)
        {
            if (pMembership.Id_Membership > 0)
                pQuery = pQuery.Where(m => m.Id_Membership == pMembership.Id_Membership);
            if (!string.IsNullOrWhiteSpace(pMembership.Name_Mem))
                pQuery = pQuery.Where(m => m.Name_Mem.Contains(pMembership.Name_Mem));
            if (!string.IsNullOrWhiteSpace(pMembership.Description))
                pQuery = pQuery.Where(m => m.Description.Contains(pMembership.Description));
            if (pMembership.Cost > 0M)
                pQuery = pQuery.Where(b => b.Cost == pMembership.Cost);
            return pQuery;
        }
        public static async Task<List<Membership>> SearchAsync(Membership pMembership)
        {
            var memberships = new List<Membership>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Membership.AsQueryable();
                select = QuerySelect(select, pMembership);
                memberships = await select.ToListAsync();
            }
            return memberships;
        }

    }
}
