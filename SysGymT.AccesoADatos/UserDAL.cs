using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SysGymT.EntidadesDeNegocio;
using System.Security.Cryptography;

namespace SysGymT.AccesoADatos
{
    public class UserDAL
    {
        private static void EncriptarMD5(User pUser)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(pUser.Password));
                var strEncriptar = "";
                for (int i = 0; i < result.Length; i++)
                    strEncriptar += result[i].ToString("x2").ToLower();
                pUser.Password = strEncriptar;
            }
        }
        private static async Task<bool> ExistLogin(User pUser, BDContexto pDbContext)
        {
            bool result = false;
            var loginUsuarioExiste = await pDbContext.User.FirstOrDefaultAsync(s => s.Login == pUser.Login && s.Id_User != pUser.Id_User);
            if (loginUsuarioExiste != null && loginUsuarioExiste.Id_User > 0 && loginUsuarioExiste.Login == pUser.Login)
                result = true;
            return result;
        }
        #region CRUD
        public static async Task<int> CreateAsync(User pUser)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bool existeLogin = await ExistLogin(pUser, bdContexto);
                if (existeLogin == false)
                {
                    pUser.Register_Date = DateTime.Now;
                    EncriptarMD5(pUser);
                    bdContexto.Add(pUser);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya existe");
            }
            return result;
        }
        public static async Task<int> ModifyAsync(User pUser)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                bool existeLogin = await ExistLogin(pUser, bdContexto);
                if (existeLogin == false)
                {
                    var user = await bdContexto.User.FirstOrDefaultAsync(s => s.Id_User == pUser.Id_User);
                    user.Id_Rol = pUser.Id_Rol;
                    user.Name = pUser.Name;
                    user.Last_Name = pUser.Last_Name;
                    user.Login = pUser.Login;
                    user.Estatus = pUser.Estatus;
                    bdContexto.Update(user);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("Login ya existe");
            }
            return result;
        }
        public static async Task<int> DeleteAsync(User pUser)
        {
            int result = 0;
            using (var bdContexto = new BDContexto())
            {
                var user = await bdContexto.User.FirstOrDefaultAsync(s => s.Id_User == pUser.Id_User);
                bdContexto.User.Remove(user);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<User> GetByIdAsync(User pUser)
        {
            var user = new User();
            using (var bdContexto = new BDContexto())
            {
                user = await bdContexto.User.FirstOrDefaultAsync(s => s.Id_User == pUser.Id_User);
            }
            return user;
        }
        public static async Task<List<User>> GetAllAsync()
        {
            var user = new List<User>();
            using (var bdContexto = new BDContexto())
            {
                user = await bdContexto.User.ToListAsync();
            }
            return user;
        }
        internal static IQueryable<User> QuerySelect(IQueryable<User> pQuery, User pUser)
        {
            if (pUser.Id_User > 0)
                pQuery = pQuery.Where(s => s.Id_User == pUser.Id_User);
            if (pUser.Id_Rol > 0)
                pQuery = pQuery.Where(s => s.Id_Rol == pUser.Id_Rol);
            if (!string.IsNullOrWhiteSpace(pUser.Name))
                pQuery = pQuery.Where(s => s.Name.Contains(pUser.Name));
            if (!string.IsNullOrWhiteSpace(pUser.Last_Name))
                pQuery = pQuery.Where(s => s.Last_Name.Contains(pUser.Last_Name));
            if (!string.IsNullOrWhiteSpace(pUser.Login))
                pQuery = pQuery.Where(s => s.Login.Contains(pUser.Login));
            if (pUser.Estatus > 0)
                pQuery = pQuery.Where(s => s.Estatus == pUser.Estatus);
            if (pUser.Register_Date.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pUser.Register_Date.Year, pUser.Register_Date.Month, pUser.Register_Date.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.Register_Date >= fechaInicial && s.Register_Date <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.Id_User).AsQueryable();
            if (pUser.Top_Aux > 0)
                pQuery = pQuery.Take(pUser.Top_Aux).AsQueryable();
            return pQuery;
        }
        public static async Task<List<User>> SearchAsync(User pUser)
        {
            var user = new List<User>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.User.AsQueryable();
                select = QuerySelect(select, pUser);
                user = await select.ToListAsync();
            }
            return user;
        }
        #endregion
        public static async Task<List<User>> SearchIncludeRolesAsync(User pUser)
        {
            var user = new List<User>();
            using (var bdContexto = new BDContexto())
            {
                var select = bdContexto.User.AsQueryable();
                select = QuerySelect(select, pUser).Include(s => s.Rol).AsQueryable();
                user = await select.ToListAsync();
            }
            return user;
        }
        public static async Task<User> LoginAsync(User pUser)
        {
            var user = new User();
            using (var bdContexto = new BDContexto())
            {
                EncriptarMD5(pUser);
                user = await bdContexto.User.FirstOrDefaultAsync(s => s.Login == pUser.Login &&
                s.Password == pUser.Password && s.Estatus == (byte)Estatus_User.ACTIVO);
            }
            return user;
        }
        public static async Task<int> ChangesPasswordAsync(User pUser, string pPasswordAnt)
        {
            int result = 0;
            var usuarioPassAnt = new User { Password = pPasswordAnt };
            EncriptarMD5(usuarioPassAnt);
            using (var bdContexto = new BDContexto())
            {
                var user = await bdContexto.User.FirstOrDefaultAsync(s => s.Id_User == pUser.Id_User);
                if (usuarioPassAnt.Password == user.Password)
                {
                    EncriptarMD5(pUser);
                    user.Password = pUser.Password;
                    bdContexto.Update(user);
                    result = await bdContexto.SaveChangesAsync();
                }
                else
                    throw new Exception("El password actual es incorrecto");
            }
            return result;
        }
    }
}
