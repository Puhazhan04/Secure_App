using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecureApp.Data;
using SecureApp.Models;

namespace SecureApp.Controllers
{
    public class AbwesenheitController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AbwesenheitController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ---------------------------------------------
        // INDEX – Übersicht aller Abwesenheiten
        // ---------------------------------------------
        public async Task<IActionResult> Index()
        {
            var list = await _context.Abwesenheiten
                .Include(a => a.Mitarbeiter)
                .OrderBy(a => a.Von)
                .ToListAsync();

            return View(list);
        }

        // ---------------------------------------------
        // HEUTE – wer ist heute abwesend? (nur Datum!)
        // ---------------------------------------------
        public async Task<IActionResult> Heute()
        {
            var heute = DateTime.Today;          // z.B. 2024-12-07 00:00:00
            var morgen = heute.AddDays(1);       // 2024-12-08 00:00:00

            var list = await _context.Abwesenheiten
                .Include(a => a.Mitarbeiter)
                .Where(a =>
                    a.Von < morgen &&           // Beginn heute oder davor
                    a.Bis >= heute              // Ende heute oder danach
                )
                .OrderBy(a => a.Mitarbeiter!.Nachname)
                .ThenBy(a => a.Mitarbeiter!.Vorname)
                .ToListAsync();

            // gleiche Tabelle / View wie Index
            return View("Index", list);
        }

        // ---------------------------------------------
        // CREATE – GET
        // ---------------------------------------------
        public IActionResult Create()
        {
            ViewBag.MitarbeiterListe = new SelectList(
                _context.Mitarbeiter
                    .OrderBy(m => m.Nachname)
                    .Select(m => new
                    {
                        m.Id,
                        Name = m.Vorname + " " + m.Nachname
                    }),
                "Id",
                "Name"
            );

            return View();
        }

        // ---------------------------------------------
        // CREATE – POST
        // ---------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Abwesenheit model)
        {
            if (model.Bis < model.Von)
            {
                ModelState.AddModelError("Bis", "Bis-Datum darf nicht vor dem Von-Datum liegen.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.MitarbeiterListe = new SelectList(
                    _context.Mitarbeiter
                        .OrderBy(m => m.Nachname)
                        .Select(m => new { m.Id, Name = m.Vorname + " " + m.Nachname }),
                    "Id",
                    "Name",
                    model.MitarbeiterId
                );

                return View(model);
            }

            _context.Abwesenheiten.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ---------------------------------------------
        // EDIT – GET
        // ---------------------------------------------
        public async Task<IActionResult> Edit(int id)
        {
            var abw = await _context.Abwesenheiten.FindAsync(id);
            if (abw == null) return NotFound();

            ViewBag.MitarbeiterListe = new SelectList(
                _context.Mitarbeiter
                    .OrderBy(m => m.Nachname)
                    .Select(m => new { m.Id, Name = m.Vorname + " " + m.Nachname }),
                "Id",
                "Name",
                abw.MitarbeiterId
            );

            return View(abw);
        }

        // ---------------------------------------------
        // EDIT – POST
        // ---------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Abwesenheit model)
        {
            if (id != model.Id)
                return NotFound();

            if (model.Bis < model.Von)
            {
                ModelState.AddModelError("Bis", "Bis-Datum darf nicht vor dem Von-Datum liegen.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.MitarbeiterListe = new SelectList(
                    _context.Mitarbeiter
                        .OrderBy(m => m.Nachname)
                        .Select(m => new { m.Id, Name = m.Vorname + " " + m.Nachname }),
                    "Id",
                    "Name",
                    model.MitarbeiterId
                );

                return View(model);
            }

            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ---------------------------------------------
        // DELETE – GET
        // ---------------------------------------------
        public async Task<IActionResult> Delete(int id)
        {
            var abw = await _context.Abwesenheiten
                .Include(a => a.Mitarbeiter)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (abw == null) return NotFound();

            return View(abw);
        }

        // ---------------------------------------------
        // DELETE – POST
        // ---------------------------------------------
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abw = await _context.Abwesenheiten.FindAsync(id);
            if (abw == null) return NotFound();

            _context.Abwesenheiten.Remove(abw);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
