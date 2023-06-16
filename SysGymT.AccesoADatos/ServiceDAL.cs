using Microsoft.EntityFrameworkCore;
using SysGymT.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.AccesoADatos
{
    public class ServiceDAL
    {
            public static async Task<int> CreateAsync(Service pService)
            {
                int result = 0;
                using (var bdContexto = new BDContexto())
                {
                    bdContexto.Add(pService);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
            public static async Task<int> ModifyAsync(Service pService)
            {
                int result = 0;
                using (var bdContexto = new BDContexto())
                {
                    var service = await bdContexto.Services.FirstOrDefaultAsync(s => s.Id_Service == pService.Id_Service);
                    service.Name_Service = pService.Name_Service;
                    bdContexto.Update(service);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
            public static async Task<int> DeleteAsync(Service pService)
            {
                int result = 0;
                using (var bdContexto = new BDContexto())
                {
                    var service = await bdContexto.Services.FirstOrDefaultAsync(s => s.Id_Service == pService.Id_Service);
                    bdContexto.Services.Remove(service);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
            public static async Task<Service> GetByIdAsync(Service pService)
            {
                var service = new Service();
                using (var bdContexto = new BDContexto())
                {
                service = await bdContexto.Services.FirstOrDefaultAsync(s => s.Id_Service == pService.Id_Service);
                }
                return service;
            }
            public static async Task<List<Service>> GetAllAsync()
            {
                var services = new List<Service>();
                using (var bdContexto = new BDContexto())
                {
                services = await bdContexto.Services.ToListAsync();
                }
                return services;
            }
            internal static IQueryable<Service> QuerySelect(IQueryable<Service> pQuery, Service pService)
            {
                if (pService.Id_Service > 0)
                    pQuery = pQuery.Where(s => s.Id_Service == pService.Id_Service);
                if (!string.IsNullOrWhiteSpace(pService.Name_Service))
                    pQuery = pQuery.Where(s => s.Name_Service.Contains(pService.Name_Service));
            if (!string.IsNullOrWhiteSpace(pService.Descryption))
                pQuery = pQuery.Where(s => s.Descryption.Contains(pService.Descryption));
            pQuery = pQuery.OrderByDescending(s => s.Id_Service).AsQueryable();
                if ( pService.Top_Aux > 0)
                    pQuery = pQuery.Take(pService.Top_Aux).AsQueryable();
                return pQuery;
            }
            public static async Task<List<Service>> SearchAsync(Service pService)
            {
                var services = new List<Service>();
                using (var bdContexto = new BDContexto())
                {
                    var select = bdContexto.Services.AsQueryable();
                    select = QuerySelect(select, pService);
                    services = await select.ToListAsync();
                }
                return services;
            }
        }
    }
