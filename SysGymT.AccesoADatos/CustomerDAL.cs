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
                    customer.Membership = pCustomer.Membership;
                    customer.Name_Customer = pCustomer.Name_Customer;
                    customer.Last_Name = pCustomer.Last_Name;
                    customer.DUI = pCustomer.DUI;
                    customer.Age = pCustomer.Age;
                    customer.Gender = pCustomer.Gender;
                    customer.Telephone = pCustomer.Telephone;
                    customer.Height = pCustomer.Height;
                    customer.Weight = pCustomer.Weight;
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
                if (!string.IsNullOrWhiteSpace(pCustomer.DUI))
                    pQuery = pQuery.Where(s => s.DUI.Contains(pCustomer.DUI));
            if (pCustomer.Weight > 0)
                      pQuery = pQuery.Where(s => Math.Abs((double)s.Weight - (double)pCustomer.Weight) < double.Epsilon);
                if (pCustomer.Telephone > 0)
                    pQuery = pQuery.Where(s => s.Telephone == pCustomer.Telephone);
                if (pCustomer.Height > 0)
                    pQuery = pQuery.Where(s => Math.Abs((double)s.Height - (double)pCustomer.Height) < double.Epsilon);
                if (!string.IsNullOrWhiteSpace(pCustomer.Membership))
                     pQuery = pQuery.Where(s => s.Membership.Contains(pCustomer.Membership));
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
                    //customers = await select.ToListAsync();
            }
            return customers;
            }
        }
    }
