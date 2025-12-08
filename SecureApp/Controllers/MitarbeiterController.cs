using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureApp.Data;
using SecureApp.Models;

namespace SecureApp.Controllers
{
    public class MitarbeiterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MitarbeiterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTE
        public async Task<IActionResult> Index()
        {
            var list = await _context.Mitarbeiter.ToListAsync();
            return View(list);
        }

        // DETAILS – Mitarbeiter + Abwesenheiten
        public async Task<IActionResult> Details(int id)
        {
            var mitarbeiter = await _context.Mitarbeiter
                .Include(m => m.Abwesenheiten)
                .ThenInclude(a => a.Mitarbeiter) // optional, meist nicht nötig
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mitarbeiter == null)
            {
                return NotFound();
            }

            return View(mitarbeiter);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mitarbeiter model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Mitarbeiter.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var mitarbeiter = await _context.Mitarbeiter.FindAsync(id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }

            return View(mitarbeiter);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mitarbeiter model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            // Passwortfelder aus Validierung rausnehmen
            ModelState.Remove(nameof(Mitarbeiter.Passwort));
            ModelState.Remove(nameof(Mitarbeiter.PasswortBestätigung));

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Mitarbeiter.Any(e => e.Id == model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // DELETE GET
        public async Task<IActionResult> Delete(int id)
        {
            var mitarbeiter = await _context.Mitarbeiter.FindAsync(id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }

            return View(mitarbeiter);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mitarbeiter = await _context.Mitarbeiter.FindAsync(id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }

            _context.Mitarbeiter.Remove(mitarbeiter);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
