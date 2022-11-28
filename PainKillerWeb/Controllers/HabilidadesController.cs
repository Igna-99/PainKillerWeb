using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PainKillerWeb.Context;
using PainKillerWeb.Models.Main;

namespace PainKillerWeb.Controllers
{
    public class HabilidadesController : Controller
    {
        private readonly PainKillerDbContext _context;

        public HabilidadesController(PainKillerDbContext context)
        {
            _context = context;
        }

        // GET: Habilidades
        public async Task<IActionResult> Index()
        {
            var painKillerDbContext = _context.habilidades.Include(h => h.atributo);
            return View(await painKillerDbContext.ToListAsync());
        }

        // GET: Habilidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habilidad = await _context.habilidades
                .Include(h => h.atributo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (habilidad == null)
            {
                return NotFound();
            }

            return View(habilidad);
        }

        // GET: Habilidades/Create
        public IActionResult Create()
        {
            ViewData["atributoId"] = new SelectList(_context.atributos, "id", "nombre");
            return View();
        }

        // POST: Habilidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,atributoId")] Habilidad habilidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habilidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["atributoId"] = new SelectList(_context.atributos, "id", "nombre", habilidad.atributoId);
            return View(habilidad);
        }

        // GET: Habilidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habilidad = await _context.habilidades.FindAsync(id);
            if (habilidad == null)
            {
                return NotFound();
            }
            ViewData["atributoId"] = new SelectList(_context.atributos, "id", "nombre", habilidad.atributoId);
            return View(habilidad);
        }

        // POST: Habilidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,atributoId")] Habilidad habilidad)
        {
            if (id != habilidad.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habilidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabilidadExists(habilidad.id))
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
            ViewData["atributoId"] = new SelectList(_context.atributos, "id", "nombre", habilidad.atributoId);
            return View(habilidad);
        }

        // GET: Habilidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habilidad = await _context.habilidades
                .Include(h => h.atributo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (habilidad == null)
            {
                return NotFound();
            }

            return View(habilidad);
        }

        // POST: Habilidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habilidad = await _context.habilidades.FindAsync(id);
            _context.habilidades.Remove(habilidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabilidadExists(int id)
        {
            return _context.habilidades.Any(e => e.id == id);
        }
    }
}
