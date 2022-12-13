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
    public class HechizosController : Controller
    {

        private readonly PainKillerDbContext _context;

        public HechizosController(PainKillerDbContext context)
        {
            _context = context;
        }

        // GET: Hechizos
        public async Task<IActionResult> Index()
        {
            var painKillerDbContext = _context.hechizos.Include(h => h.distancia).Include(h => h.elemento);
            List<string> tipoCostes = new List<string>();
            tipoCostes.Add("VIDA");
            tipoCostes.Add("MANA");
            tipoCostes.Add("ENERGIA");

            ViewBag.tipoCostes = tipoCostes;

            return View(await painKillerDbContext.ToListAsync());
        }

        // GET: Hechizos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hechizo = await _context.hechizos
                .Include(h => h.distancia)
                .Include(h => h.elemento)
                .FirstOrDefaultAsync(m => m.id == id);
            if (hechizo == null)
            {
                return NotFound();
            }

            return View(hechizo);
        }

        // GET: Hechizos/Create
        public IActionResult Create()
        {
            ViewData["distanciaId"] = new SelectList(_context.distancias, "id", "nombre");
            ViewData["elementoId"] = new SelectList(_context.elementos, "id", "nombre");
            return View();
        }

        // POST: Hechizos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,duracion,costeExp,costeUso,tipoCoste,efecto,tiempo,distanciaId,elementoId")] Hechizo hechizo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hechizo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["distanciaId"] = new SelectList(_context.distancias, "id", "nombre", hechizo.distanciaId);
            ViewData["elementoId"] = new SelectList(_context.elementos, "id", "nombre", hechizo.elementoId);
            return View(hechizo);
        }

        // GET: Hechizos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hechizo = await _context.hechizos.FindAsync(id);
            if (hechizo == null)
            {
                return NotFound();
            }
            ViewData["distanciaId"] = new SelectList(_context.distancias, "id", "nombre", hechizo.distanciaId);
            ViewData["elementoId"] = new SelectList(_context.elementos, "id", "nombre", hechizo.elementoId);
            return View(hechizo);
        }

        // POST: Hechizos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,duracion,costeExp,costeUso,tipoCoste,efecto,tiempo,distanciaId,elementoId")] Hechizo hechizo)
        {
            if (id != hechizo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hechizo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HechizoExists(hechizo.id))
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
            ViewData["distanciaId"] = new SelectList(_context.distancias, "id", "nombre", hechizo.distanciaId);
            ViewData["elementoId"] = new SelectList(_context.elementos, "id", "nombre", hechizo.elementoId);
            return View(hechizo);
        }

        // GET: Hechizos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hechizo = await _context.hechizos
                .Include(h => h.distancia)
                .Include(h => h.elemento)
                .FirstOrDefaultAsync(m => m.id == id);
            if (hechizo == null)
            {
                return NotFound();
            }

            return View(hechizo);
        }

        // POST: Hechizos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hechizo = await _context.hechizos.FindAsync(id);
            _context.hechizos.Remove(hechizo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HechizoExists(int id)
        {
            return _context.hechizos.Any(e => e.id == id);
        }
    }
}
