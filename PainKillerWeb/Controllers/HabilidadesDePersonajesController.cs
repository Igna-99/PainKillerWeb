using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PainKillerWeb.Context;
using PainKillerWeb.Models.Main;
using PainKillerWeb.Models.Pivot;

namespace PainKillerWeb.Controllers
{
    public class HabilidadesDePersonajesController : Controller
    {
        private readonly PainKillerDbContext _context;

        public HabilidadesDePersonajesController(PainKillerDbContext context)
        {
            _context = context;
        }

        // GET: HabilidadesDePersonajes
        public async Task<IActionResult> Index()
        {
            var painKillerDbContext = _context.habilidadDePersonajes.Include(h => h.Habilidad).Include(h => h.Personaje);
            return View(await painKillerDbContext.ToListAsync());
        }

        // GET: HabilidadesDePersonajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habilidadDePersonaje = await _context.habilidadDePersonajes
                .Include(h => h.Habilidad)
                .Include(h => h.Personaje)
                .FirstOrDefaultAsync(m => m.id == id);
            if (habilidadDePersonaje == null)
            {
                return NotFound();
            }

            return View(habilidadDePersonaje);
        }

        // GET: HabilidadesDePersonajes/Create
        public IActionResult Create()
        {
            ViewData["HabilidadId"] = new SelectList(_context.habilidades, "id", "nombre");
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre");
            return View();
        }

        // POST: HabilidadesDePersonajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,personajeId,HabilidadId,Nivel")] HabilidadDePersonaje habilidadDePersonaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habilidadDePersonaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HabilidadId"] = new SelectList(_context.habilidades, "id", "nombre", habilidadDePersonaje.HabilidadId);
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre", habilidadDePersonaje.personajeId);
            return View(habilidadDePersonaje);
        }


        public IActionResult CreateFor(int id)
        {

            var personaje = _context.personajes.Find(id);

            if (personaje == null)
            {
                return NotFound();
            }

            ViewData["HabilidadId"] = new SelectList(_context.habilidades, "id", "nombre");
            ViewData["personajeId"] = id;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFor([Bind("personajeId,HabilidadId,Nivel")] HabilidadDePersonaje habilidadDePersonaje)
        {
            Personaje pj = _context.personajes
                .Include(x => x.habilidades).ThenInclude(x => x.Habilidad)
                .FirstOrDefault(m => m.id == habilidadDePersonaje.personajeId);


            if (ModelState.IsValid)
            {
                if (pj.expActual >= 2)
                {
                    if (!pj.habilidades.Any(x => x.HabilidadId == habilidadDePersonaje.HabilidadId))
                    {
                        pj.expActual -= 2;
                        pj.expGastada += 2;
                        _context.Update(pj);
                        _context.Add(habilidadDePersonaje);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Details", "Personajes", new { id = habilidadDePersonaje.personajeId });
                    }
                    else 
                    {
                        ViewBag.ErrorMessage = $"El Personaje '{pj.nombre}' ya tiene esa habilidad";
                    }
                }
                else  
                {
                    ViewBag.ErrorMessage = "no tienes suficiente experiencia para adquirir una habilidad";
                }

            }

            
            ViewData["HabilidadId"] = new SelectList(_context.habilidades, "id", "nombre");
            ViewData["personajeId"] = habilidadDePersonaje.personajeId;

            return View();
        }








        // GET: HabilidadesDePersonajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habilidadDePersonaje = await _context.habilidadDePersonajes.FindAsync(id);
            if (habilidadDePersonaje == null)
            {
                return NotFound();
            }
            ViewData["HabilidadId"] = new SelectList(_context.habilidades, "id", "nombre", habilidadDePersonaje.HabilidadId);
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre", habilidadDePersonaje.personajeId);
            return View(habilidadDePersonaje);
        }

        // POST: HabilidadesDePersonajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,personajeId,HabilidadId,Nivel")] HabilidadDePersonaje habilidadDePersonaje)
        {
            if (id != habilidadDePersonaje.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habilidadDePersonaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabilidadDePersonajeExists(habilidadDePersonaje.id))
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
            ViewData["HabilidadId"] = new SelectList(_context.habilidades, "id", "nombre", habilidadDePersonaje.HabilidadId);
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre", habilidadDePersonaje.personajeId);
            return View(habilidadDePersonaje);
        }

        // GET: HabilidadesDePersonajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habilidadDePersonaje = await _context.habilidadDePersonajes
                .Include(h => h.Habilidad)
                .Include(h => h.Personaje)
                .FirstOrDefaultAsync(m => m.id == id);
            if (habilidadDePersonaje == null)
            {
                return NotFound();
            }

            return View(habilidadDePersonaje);
        }

        // POST: HabilidadesDePersonajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habilidadDePersonaje = await _context.habilidadDePersonajes.FindAsync(id);
            _context.habilidadDePersonajes.Remove(habilidadDePersonaje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabilidadDePersonajeExists(int id)
        {
            return _context.habilidadDePersonajes.Any(e => e.id == id);
        }
    }
}
