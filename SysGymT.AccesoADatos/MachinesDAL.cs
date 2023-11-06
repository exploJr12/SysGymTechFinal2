using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysGymT.EntidadesDeNegocio;

namespace SysGymT.AccesoADatos
{
    public class MachinesDAL
    {
        #region CRUD
        public static async Task<int> CreateASync(Machines pMachines)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bdContexto.Add(pMachines);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> ModifyAsync(Machines pMachines)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var existingMachines = await bdContexto.Machines.FirstOrDefaultAsync(m => m.Id_Machines == pMachines.Id_Machines);
                if (existingMachines != null)
                {
                    existingMachines.Id_Usuario = pMachines.Id_Usuario;
                    existingMachines.Machines_Name = pMachines.Machines_Name;
                    existingMachines.Brand = pMachines.Brand;
                    existingMachines.Serial_Number = pMachines.Serial_Number;
                    existingMachines.Status = pMachines.Status;
                    existingMachines.Acquisition_Date = pMachines.Acquisition_Date;
                    existingMachines.Maintenance_Date = pMachines.Maintenance_Date;
                    existingMachines.Next_Maintenance_Date = pMachines.Next_Maintenance_Date;

                    bdContexto.Update(existingMachines);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("La máquina no existe");
                }
            }
            return result;
        }
        public static async Task<int> DeleteAsync(Machines pMachines)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var existingMachines = await bdContexto.Machines.FirstOrDefaultAsync(m => m.Id_Machines == pMachines.Id_Machines);
                if (existingMachines != null)
                {
                    bdContexto.Machines.Remove(existingMachines);
                    result = await bdContexto.SaveChangesAsync();
                }
            }
            return result;
        }

        public static async Task<Machines> GetByIdAsync(Machines pMachines)
        {
            var machines = new Machines();
            using (var bdContexto = new BDContexto())
            {
                machines = await bdContexto.Machines.FirstOrDefaultAsync(m => m.Id_Machines == pMachines.Id_Machines);
            }
            return machines;
        }
        public static async Task<List<Machines>> GetAllAsync()
        {
            var machines = new List<Machines>();
            using (var bdContexto = new BDContexto())
            {
                machines = await bdContexto.Machines.Include(s => s.Usuario).AsQueryable().ToListAsync();
            }
            return machines;
        }

        internal static IQueryable<Machines> QuerySelect(IQueryable<Machines> pQuery, Machines pMachines)
        {
            if (pMachines.Id_Machines > 0)
                pQuery = pQuery.Where(m => m.Id_Machines == pMachines.Id_Machines);
            if (!string.IsNullOrWhiteSpace(pMachines.Machines_Name))
                pQuery = pQuery.Where(m => m.Machines_Name.Contains(pMachines.Machines_Name));
            if (!string.IsNullOrWhiteSpace(pMachines.Brand))
                pQuery = pQuery.Where(m => m.Brand.Contains(pMachines.Brand));
            if (!string.IsNullOrWhiteSpace(pMachines.Serial_Number))
                pQuery = pQuery.Where(m => m.Serial_Number.Contains(pMachines.Serial_Number));
            if (pMachines.Status)
                pQuery = pQuery.Where(m => m.Status == pMachines.Status);
            if (pMachines.Acquisition_Date.HasValue)
            {
                DateTime fechaInicial = pMachines.Acquisition_Date.Value.Date;
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(m => m.Acquisition_Date >= fechaInicial && m.Acquisition_Date <= fechaFinal);
            }
            if (pMachines.Maintenance_Date.HasValue)
            {
                DateTime fechaInicial = pMachines.Maintenance_Date.Value.Date;
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(m => m.Maintenance_Date >= fechaInicial && m.Maintenance_Date <= fechaFinal);
            }
            if (pMachines.Next_Maintenance_Date.HasValue)
            {
                DateTime fechaInicial = pMachines.Next_Maintenance_Date.Value.Date;
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(m => m.Next_Maintenance_Date >= fechaInicial && m.Next_Maintenance_Date <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(m => m.Id_Machines).AsQueryable();
            if (pMachines.Top_Aux > 0)
                pQuery = pQuery.Take(pMachines.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<Machines>> SearchAsync(Machines pMachines)
        {
            var machine = new List<Machines>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Machines.AsQueryable();
                select = QuerySelect(select, pMachines);
                machine = await select.ToListAsync();
                //machine = await select.ToListAsync();
            }
            return machine;
        }

        #endregion
        public static async Task<List<Machines>> SearchIncludeUsuarioAsync(Machines pMachines)
        {
            var machines = new List<Machines>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.Machines.AsQueryable();
                select = QuerySelect( select, pMachines).Include(s => s.Usuario).AsQueryable();
                machines = await select.ToListAsync();
            }
            return machines;
        }

    }
}