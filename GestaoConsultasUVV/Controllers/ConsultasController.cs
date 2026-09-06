using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using GestaoConsultasUVV.Data;
using GestaoConsultasUVV.Models;

namespace GestaoConsultasUVV.Controllers
{
    [Authorize] // Só usuários logados acessam qualquer ação deste controller
    public class ConsultasController : Controller
    {
        private readonly AppDbContext _context;

        public ConsultasController(AppDbContext context)
        {
            _context = context;
        }

        // Pega o Id do usuário que está logado
        private int UsuarioLogadoId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        // GET: Consultas -> lista só as consultas do usuário logado
        public async Task<IActionResult> Index()
        {
            int usuarioId = UsuarioLogadoId();
            var consultas = await _context.Consultas
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();
            return View(consultas);
        }

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId());
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // GET: Consultas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consultas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Especialidade,DataHora,Descricao")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                consulta.UsuarioId = UsuarioLogadoId();
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId());
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // POST: Consultas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Especialidade,DataHora,Descricao")] Consulta consulta)
        {
            if (id != consulta.Id) return NotFound();

            // Confere se a consulta é mesmo do usuário logado
            var original = await _context.Consultas
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId());
            if (original == null) return NotFound();

            if (ModelState.IsValid)
            {
                consulta.UsuarioId = UsuarioLogadoId();
                _context.Update(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId());
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId());
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}