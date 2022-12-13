using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PainKillerWeb.Context;
using PainKillerWeb.Models.Main;
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


        public IActionResult CreateFor(int id)
        {

            var personaje = _context.personajes.Find(id);

            if (personaje == null)
            {
                return NotFound();
            }

            ViewBag.HechizoId = new SelectList(_context.hechizos, "id", "nombre", "costeExp");
            ViewData["personajeId"] = id;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFor([Bind("personajeId,HechizoId")] HechizoDePersonaje hechizoDePersonaje)
        {
            Personaje pj = _context.personajes
                .Include(x => x.hechizos).ThenInclude(x => x.Hechizo)
                .FirstOrDefault(m => m.id == hechizoDePersonaje.personajeId);
            Hechizo hechizo = _context.hechizos
                .FirstOrDefault(x => x.id == hechizoDePersonaje.HechizoId);

            if (ModelState.IsValid)
            {
                if (pj.expActual >= hechizo.costeExp)
                {
                    if (!pj.hechizos.Any(x => x.HechizoId == hechizoDePersonaje.HechizoId))
                    {
                        pj.expActual -= hechizo.costeExp;
                        pj.expGastada += hechizo.costeExp;
                        _context.Update(pj);
                        _context.Add(hechizoDePersonaje);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Details", "Personajes", new { id = hechizoDePersonaje.personajeId });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = $"El Personaje '{pj.nombre}' ya tiene ese hechizo";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "no tienes suficiente experiencia para adquirir una hechizo";
                }

            }

            ViewBag.HechizoId = new SelectList(_context.hechizos, "id", "nombre", "costeExp");
            ViewData["personajeId"] = hechizoDePersonaje.personajeId;

            return View();
        }
        public async Task<IActionResult> useHechizo(int id)
        {
            HechizoDePersonaje hDP = _context.hechizosDePersonajes.Where(x => x.id == id).Include(x => x.Personaje).Include(x => x.Hechizo).FirstOrDefault();
            Personaje pers = hDP.Personaje;

            List<string> tipoCostes = new List<string>();
            tipoCostes.Add("VIDA");
            tipoCostes.Add("MANA");
            tipoCostes.Add("ENERGIA");

            switch (hDP.Hechizo.tipoCoste)
            {
                case 1:
                    if (hDP != null && pers.vidaAct >= hDP.Hechizo.costeUso)
                    {
                        pers.vidaAct -= hDP.Hechizo.costeUso;
                        _context.Update(pers);
                        await _context.SaveChangesAsync();
                    }
                    break;
                case 2:
                    if (hDP != null && pers.manaAct >= hDP.Hechizo.costeUso)
                    {
                        pers.manaAct -= hDP.Hechizo.costeUso;
                        _context.Update(pers);
                        await _context.SaveChangesAsync();
                    }
                    break;
                case 3:
                    if (hDP != null && pers.energiaAct >= hDP.Hechizo.costeUso)
                    {
                        pers.energiaAct -= hDP.Hechizo.costeUso;
                        _context.Update(pers);
                        await _context.SaveChangesAsync();
                    }
                    break;
            }

            return RedirectToAction("Jugar", "Personajes", new { id = pers.id });
        }
        [HttpPost, ActionName("DeleteInModal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteInModal(int id)
        {
            var hechizoDePersonaje = await _context.hechizosDePersonajes.FindAsync(id);
            _context.hechizosDePersonajes.Remove(hechizoDePersonaje);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Personajes", new { id = hechizoDePersonaje.personajeId });
        }
        private bool HechizoDePersonajeExists(int id)
        {
            return _context.hechizosDePersonajes.Any(e => e.id == id);
        }
    }
}
