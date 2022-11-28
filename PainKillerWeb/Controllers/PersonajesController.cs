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
    public class PersonajesController : Controller
    {
        private readonly PainKillerDbContext _context;

        public PersonajesController(PainKillerDbContext context)
        {
            _context = context;
        }

        // GET: Personajes
        public async Task<IActionResult> Index()
        {
            var painKillerDbContext = _context.personajes.Include(p => p.raza);
            return View(await painKillerDbContext.ToListAsync());
        }

        // GET: Personajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaje = await _context.personajes.Include(x => x.atributos).ThenInclude(x => x.atributo).Include(x => x.raza)
                .FirstOrDefaultAsync(m => m.id == id);
            if (personaje == null)
            {
                return NotFound();
            }

            return View(personaje);
        }

        // GET: Personajes/Create
        public IActionResult Create()
        {
            ViewData["razaId"] = new SelectList(_context.raza, "id", "nombre");
            return View();
        }

        // POST: Personajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,razaId,expActual,vidaMax,manaMax,energiaMax")] Personaje personaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personaje);
                await _context.SaveChangesAsync();
                return RedirectToAction("CreateAll", "AtributosDePersonajes", personaje);
            }
            ViewData["razaId"] = new SelectList(_context.raza, "id", "nombre", personaje.razaId);
            return View(personaje);
        }

        // GET: Personajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaje = await _context.personajes.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }
            ViewData["razaId"] = new SelectList(_context.raza, "id", "nombre", personaje.razaId);
            return View(personaje);
        }

        // POST: Personajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,razaId,expActual,expGastada,vidaMax,manaMax,energiaMax")] Personaje personaje)
        {
            if (id != personaje.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonajeExists(personaje.id))
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
            ViewData["razaId"] = new SelectList(_context.raza, "id", "nombre", personaje.razaId);
            return View(personaje);
        }

        // GET: Personajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaje = await _context.personajes.Include(x => x.atributos).ThenInclude(x => x.atributo).Include(x => x.raza)
                .FirstOrDefaultAsync(m => m.id == id);
            if (personaje == null)
            {
                return NotFound();
            }

            return View(personaje);
        }

        // POST: Personajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personaje = await _context.personajes.FindAsync(id);
            _context.personajes.Remove(personaje);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonajeExists(int id)
        {
            return _context.personajes.Any(e => e.id == id);
        }
    }
}
