using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SysGymT.EntidadesDeNegocio;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SysGymT.AccesoADatos
{
    public class BDContexto : DbContext
    {


        public DbSet<Rol> Rol { get; set; }
        public DbSet<User> User { get; set; }
        //public DbSet<Machines> Machines { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=EXPLOJR5\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
