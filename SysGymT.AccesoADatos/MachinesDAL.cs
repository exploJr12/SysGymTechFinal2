using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysGymT.EntidadesDeNegocio;

namespace SysGymT.AccesoADatos
{
    public class MachinesDAL
    {
        public static async Task<int> CreateAsync(Machines pMachines)
        {
            int result = 0;
            using (var bdcontexto = new BDContexto())
            {
                bdcontexto.Add(pMachines);
                result = await bdcontexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModifyAsync(Machines pMachines)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var machines = await bdContexto.Machines.FirstOrDefaultAsync(s => s.Id_Machines == pMachines.Id_Machines);
                machines.Machines_Name = pMachines.Machines_Name;
                bdContexto.Update(machines);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> DeleteAsync(Machines pMachines)
        {
            int result = 0; 
            using (var bdContexto = new BDContexto()) 
            {
                var machines = await bdContexto.Machines.FirstOrDefaultAsync(s => s.Id_Machines == pMachines.Id_Machines);
                bdContexto.Machines.Remove(machines);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<Machines> GetByIdAsync(Machines pMachines)
        {
            var machines = new Machines();
            using (var bdContexto = new BDContexto())
            {
                machines = await bdContexto.Machines.FirstOrDefaultAsync(s => s.Id_Machines == pMachines.Id_Machines);
            }
            return machines;
        }
        public static async Task<List<Machines>> GetAllAsync()
        {
            var machines = new List<Machines>();
            using (var bdContexto = new BDContexto())
            {
                machines = await bdContexto.Machines.ToListAsync();
            }
            return machines;
        }
        internal static IQueryable<Machines> QuerySelect(IQueryable<Machines> pQuery, Machines pMachines)
        {
            if (pMachines.Id_Machines > 0)
                pQuery = pQuery.Where(s => s.Id_Machines == pMachines.Id_Machines);
            if (!string.IsNullOrWhiteSpace(pMachines.Machines_Name))
                pQuery = pQuery.Where(s => s.Machines_Name.Contains(pMachines.Machines_Name));
            if (!string.IsNullOrWhiteSpace(pMachines.Brand))
                pQuery = pQuery.Where(s => s.Brand.Contains(pMachines.Brand));
            pQuery = pQuery.OrderByDescending(s => s.Brand).AsQueryable();
            if (pMachines.Top_Aux > 0)
                pQuery = pQuery.Take(pMachines.Top_Aux).AsQueryable();
            return pQuery;
        }

        // quite el estatic por que daba problemas
        public static async Task<List<Machines>> SearchASync(Machines pMachines)
        {
            var machines = new List<Machines>();
            using (var bdContext = new BDContexto())
            {
                var select = bdContext.Machines.AsQueryable();
                select = QuerySelect(select, pMachines);
                machines = await select.ToListAsync();
            }
            return machines;
        }
    }
}
