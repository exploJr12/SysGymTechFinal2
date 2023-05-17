using Microsoft.AspNetCore.Authentication.Cookies;
using SysGymT.EntidadesDeNegocio;
using SysGymT.LogicaDeNegocio;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SysGymT.UI.AppWebAspCore.Controllers

{     
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        // GET: UserController
        UserBL userBL = new UserBL();
        RolBL rolBL = new RolBL();
        // GET: UsuarioController
        public async Task<IActionResult> Index(User pUser = null)
        {
            if (pUser == null)
                pUser = new User();
            if (pUser.Top_Aux == 0)
                pUser.Top_Aux = 10;
            else if (pUser.Top_Aux == -1)
                pUser.Top_Aux = 0;
            var taskBuscar = userBL.SearchIncluedeRolesAsync(pUser);
            var taskObtenerTodosRoles = rolBL.GetAllAsync();
            var usuarios = await taskBuscar;
            ViewBag.Top = pUser.Top_Aux;
            ViewBag.Roles = await taskObtenerTodosRoles;
            return View(usuarios);
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await userBL.GetByIdAsync(new User { Id_User = id });
            user.Rol = await rolBL.GetByIdAsync(new Rol { Id_Rol = user.Id_Rol });
            return View(user);
        }

        // GET: UserController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.rol = await rolBL.GetAllAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User pUser)
        {
            try
            {
                int result = await userBL.CreateAsync(pUser);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.rol = await rolBL.GetAllAsync();
                return View(pUser);
            }
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(User pUser)
        {
            var taskObtenerPorId = userBL.GetByIdAsync(pUser);
            var taskObtenerTodosRoles = rolBL.GetAllAsync();
            var user = await taskObtenerPorId;
            ViewBag.rol = await taskObtenerTodosRoles;
            ViewBag.Error = "";
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User pUsuario)
        {
            try
            {
                int result = await userBL.ModifyAsync(pUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Roles = await rolBL.GetAllAsync();
                return View(pUsuario);
            }
        }

        // GET: UsuarioController/Delete/5
        public async Task<IActionResult> Delete(User pUsuario)
        {
            var usuario = await userBL.GetByIdAsync(pUsuario);
            usuario.Rol = await rolBL.GetByIdAsync(new Rol { Id_Rol = usuario.Id_Rol });
            ViewBag.Error = "";
            return View(usuario);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, User pUsuario)
        {
            try
            {
                int result = await userBL.DeleteAsync(pUsuario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var usuario = await userBL.GetByIdAsync(pUsuario);
                if (usuario == null)
                    usuario = new User();
                if (usuario.Id_Rol > 0)
                    usuario.Rol = await rolBL.GetByIdAsync(new Rol { Id_Rol = usuario.Id_Rol });
                return View(usuario);
            }
        }
        // GET: UsuarioController/Create
        [AllowAnonymous]
        public async Task<IActionResult> Login(string ReturnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Url = ReturnUrl;
            ViewBag.Error = "";
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(User pUsuario, string pReturnUrl = null)
        {
            try
            {
                var user = await userBL.LoginAsync(pUsuario);
                if (user != null && user.Id_Rol > 0 && pUsuario.Login == user.Login)
                {
                    user.Rol = await rolBL.GetByIdAsync(new Rol { Id_Rol = user.Id_Rol });
                    var claims = new[] { new Claim(ClaimTypes.Name, user.Login), new Claim(ClaimTypes.Role, user.Rol.Name) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                }
                else
                    throw new Exception("Credenciales incorrectas");
                if (!string.IsNullOrWhiteSpace(pReturnUrl))
                    return Redirect(pReturnUrl);
                else
                    return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                ViewBag.Url = pReturnUrl;
                ViewBag.Error = ex.Message;
                return View(new User { Login = pUsuario.Login });
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> CerrarSesion(string ReturnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Usuario");
        }
        // GET: UsuarioController/Create
        public async Task<IActionResult> CambiarPassword()
        {

            var usuarios = await userBL.SearchAsync(new User { Login = User.Identity.Name, Top_Aux = 1 });
            var usuarioActual = usuarios.FirstOrDefault();
            ViewBag.Error = "";
            return View(usuarioActual);
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarPassword(User pUsuario, string pPasswordAnt)
        {
            try
            {
                int result = await userBL.ChangesPasswordAsync(pUsuario, pPasswordAnt);
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Usuario");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var usuarios = await userBL.SearchAsync(new User { Login = User.Identity.Name, Top_Aux = 1 });
                var usuarioActual = usuarios.FirstOrDefault();
                return View(usuarioActual);
            }
        }
    }
}
