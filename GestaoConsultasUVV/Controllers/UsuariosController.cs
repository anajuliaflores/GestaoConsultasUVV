using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using GestaoConsultasUVV.Data;
using GestaoConsultasUVV.Models;

namespace GestaoConsultasUVV.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // ---------- CADASTRO ----------
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastro(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            // Verifica se o e-mail já existe
            bool emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email);
            if (emailExiste)
            {
                ModelState.AddModelError("Email", "Este e-mail já está cadastrado.");
                return View(usuario);
            }

            usuario.DataCadastro = DateTime.Now;
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        // ---------- LOGIN ----------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string senha)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);

            if (usuario == null)
            {
                ViewBag.Erro = "E-mail ou senha inválidos.";
                return View();
            }

            // Cria o "crachá" do usuário logado
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome)
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);

            return RedirectToAction("Index", "Consultas");
        }

        // ---------- LOGOUT ----------
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }
    }
}