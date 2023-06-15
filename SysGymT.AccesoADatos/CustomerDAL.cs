using Microsoft.EntityFrameworkCore;
using SysGymT.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysGymT.AccesoADatos
{
    public class CustomerDAL
    {
            public static async Task<int> CreateAsync(Customer pCustomer)
            {
                int result = 0;
                using (var bdContexto = new BDContexto())
                {
                    bdContexto.Add(pCustomer);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
            public static async Task<int> ModifyAsync(Customer pCustomer)
            {
                int result = 0;
                using (var bdContexto = new BDContexto())
                {
                    var customer = await bdContexto.Customers.FirstOrDefaultAsync(s => s.Id_Customer == pCustomer.Id_Customer);
                    customer.Name_Customer = pCustomer.Name_Customer;
                    bdContexto.Update(customer);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
            public static async Task<int> DeleteAsync(Customer pCustomer)
            {
                int result = 0;
                using (var bdContexto = new BDContexto())
                {
                    var customer = await bdContexto.Customers.FirstOrDefaultAsync(s => s.Id_Customer == pCustomer.Id_Customer);
                    bdContexto.Customers.Remove(customer);
                    result = await bdContexto.SaveChangesAsync();
                }
                return result;
            }
            public static async Task<Customer> GetByIdAsync(Customer pCustomer)
            {
                var customer = new Customer();
                using (var bdContexto = new BDContexto())
                {
                    customer = await bdContexto.Customers.FirstOrDefaultAsync(s => s.Id_Customer == pCustomer.Id_Customer);
                }
                return customer;
            }
            public static async Task<List<Customer>> GetAllAsync()
            {
                var customers = new List<Customer>();
                using (var bdContexto = new BDContexto())
                {
                    customers = await bdContexto.Customers.ToListAsync();
                }
                return customers;
            }
            internal static IQueryable<Customer> QuerySelect(IQueryable<Customer> pQuery, Customer pCustomer)
            {
                if (pCustomer.Id_Customer > 0)
                    pQuery = pQuery.Where(s => s.Id_Customer == pCustomer.Id_Customer);
                if (!string.IsNullOrWhiteSpace(pCustomer.Name_Customer))
                    pQuery = pQuery.Where(s => s.Name_Customer.Contains(pCustomer.Name_Customer));
                if (!string.IsNullOrWhiteSpace(pCustomer.Last_Name))
                    pQuery = pQuery.Where(s => s.Last_Name.Contains(pCustomer.Last_Name));
                if (!string.IsNullOrWhiteSpace(pCustomer.Gender))
                    pQuery = pQuery.Where(s => s.Gender.Contains(pCustomer.Gender));
                if (pCustomer.Age > 0)
                    pQuery = pQuery.Where(s => s.Age == pCustomer.Age);
                if (pCustomer.DUI > 0)
                    pQuery = pQuery.Where(s => s.DUI == pCustomer.DUI);
                if (pCustomer.Weight > 0)
                    pQuery = pQuery.Where(s => s.Weight == pCustomer.Weight);
                if (pCustomer.Telephone > 0)
                    pQuery = pQuery.Where(s => s.Telephone == pCustomer.Telephone);
                if (pCustomer.Height > 0)
                    pQuery = pQuery.Where(s => s.Height == pCustomer.Height);
                pQuery = pQuery.OrderByDescending(s => s.Id_Customer).AsQueryable();
                if (pCustomer.Top_Aux > 0)
                    pQuery = pQuery.Take(pCustomer.Top_Aux).AsQueryable();
                return pQuery;
            }
            public static async Task<List<Customer>> SearchAsync(Customer pCustomer)
            {
                var customers = new List<Customer>();
                using (var bdContexto = new BDContexto())
                {
                    var select = bdContexto.Customers.AsQueryable();
                    select = QuerySelect(select, pCustomer);
                    customers = await select.ToListAsync();
                }
                return customers;
            }
        }
    }
