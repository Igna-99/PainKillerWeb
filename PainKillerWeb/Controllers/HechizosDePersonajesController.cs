using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PainKillerWeb.Context;
using PainKillerWeb.Models.Pivot;

namespace PainKillerWeb.Controllers
{
    public class HechizosDePersonajesController : Controller
    {
        private readonly PainKillerDbContext _context;

        public HechizosDePersonajesController(PainKillerDbContext context)
        {
            _context = context;
        }

        // GET: HechizosDePersonajes
        public async Task<IActionResult> Index()
        {
            var painKillerDbContext = _context.hechizosDePersonajes.Include(h => h.Hechizo).Include(h => h.Personaje);
            return View(await painKillerDbContext.ToListAsync());
        }

        // GET: HechizosDePersonajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hechizoDePersonaje = await _context.hechizosDePersonajes
                .Include(h => h.Hechizo)
                .Include(h => h.Personaje)
                .FirstOrDefaultAsync(m => m.id == id);
            if (hechizoDePersonaje == null)
            {
                return NotFound();
            }

            return View(hechizoDePersonaje);
        }

        // GET: HechizosDePersonajes/Create
        public IActionResult Create()
        {
            ViewData["HechizoId"] = new SelectList(_context.hechizos, "id", "nombre");
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre");
            return View();
        }

        // POST: HechizosDePersonajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,personajeId,HechizoId")] HechizoDePersonaje hechizoDePersonaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hechizoDePersonaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HechizoId"] = new SelectList(_context.hechizos, "id", "nombre", hechizoDePersonaje.HechizoId);
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre", hechizoDePersonaje.personajeId);
            return View(hechizoDePersonaje);
        }

        // GET: HechizosDePersonajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hechizoDePersonaje = await _context.hechizosDePersonajes.FindAsync(id);
            if (hechizoDePersonaje == null)
            {
                return NotFound();
            }
            ViewData["HechizoId"] = new SelectList(_context.hechizos, "id", "efecto", hechizoDePersonaje.HechizoId);
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre", hechizoDePersonaje.personajeId);
            return View(hechizoDePersonaje);
        }

        // POST: HechizosDePersonajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,personajeId,HechizoId")] HechizoDePersonaje hechizoDePersonaje)
        {
            if (id != hechizoDePersonaje.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hechizoDePersonaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HechizoDePersonajeExists(hechizoDePersonaje.id))
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
            ViewData["HechizoId"] = new SelectList(_context.hechizos, "id", "efecto", hechizoDePersonaje.HechizoId);
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre", hechizoDePersonaje.personajeId);
            return View(hechizoDePersonaje);
        }

        // GET: HechizosDePersonajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hechizoDePersonaje = await _context.hechizosDePersonajes
                .Include(h => h.Hechizo)
                .Include(h => h.Personaje)
                .FirstOrDefaultAsync(m => m.id == id);
            if (hechizoDePersonaje == null)
            {
                return NotFound();
            }

            return View(hechizoDePersonaje);
        }

        // POST: HechizosDePersonajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hechizoDePersonaje = await _context.hechizosDePersonajes.FindAsync(id);
            _context.hechizosDePersonajes.Remove(hechizoDePersonaje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HechizoDePersonajeExists(int id)
        {
            return _context.hechizosDePersonajes.Any(e => e.id == id);
        }
    }
}
