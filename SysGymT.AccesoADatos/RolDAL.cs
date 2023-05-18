using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysGymT.EntidadesDeNegocio;

namespace SysGymT.AccesoADatos
{
    public class RolDAL
    {
        public static async Task<int> CreateAsync(Rol pRol)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pRol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModifyAsync(Rol pRol)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var rol = await bdContexto.Rol.FirstOrDefaultAsync(s => s.Id_Rol == pRol.Id_Rol);
                rol.Name = pRol.Name;
                bdContexto.Update(rol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> DeleteAsync(Rol pRol)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var rol = await bdContexto.Rol.FirstOrDefaultAsync(s => s.Id_Rol == pRol.Id_Rol);
                bdContexto.Rol.Remove(rol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Rol> GetByIdAsync(Rol pRol)
        {
            var rol = new Rol();
            using (var bdContexto = new BDContexto())
            {
                rol = await bdContexto.Rol.FirstOrDefaultAsync(s => s.Id_Rol == pRol.Id_Rol);
            }
            return rol;
        }
        public static async Task<List<Rol>> GetAllAsync()
        {
            var roles = new List<Rol>();
            using (var bdContexto = new BDContexto())
            {
                roles = await bdContexto.Rol.ToListAsync();
            }
            return roles;
        }
        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, Rol pRol)
        {
            if (pRol.Id_Rol > 0)
                pQuery = pQuery.Where(s => s.Id_Rol == pRol.Id_Rol);
            if (!string.IsNullOrWhiteSpace(pRol.Name))
                pQuery = pQuery.Where(s => s.Name.Contains(pRol.Name));
            pQuery = pQuery.OrderByDescending(s => s.Id_Rol).AsQueryable();
            if (pRol.Top_Aux > 0)
                pQuery = pQuery.Take(pRol.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Rol>> SearchAsync(Rol pRol)
        {
            var rols = new List<Rol>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Rol.AsQueryable();
                select = QuerySelect(select, pRol);
                //roles = await select.ToListAsync();
                rols = await select.ToListAsync();
            }
            return rols;
        }
    }
}
