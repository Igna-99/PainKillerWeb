using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PainKillerWeb.Context;
using PainKillerWeb.Models.Main;
using PainKillerWeb.Models.Pivot;
using PainKillerWeb.Services;

namespace PainKillerWeb.Controllers
{
    public class AtributosDePersonajesController : Controller
    {
        private readonly PainKillerDbContext _context;

        public AtributosDePersonajesController(PainKillerDbContext context)
        {
            _context = context;
        }

        // GET: AtributosDePersonajes
        public async Task<IActionResult> Index()
        {
            var painKillerDbContext = _context.atributosDePersonajes.Include(a => a.atributo).Include(a => a.personaje);
            return View(await painKillerDbContext.ToListAsync());
        }

        // GET: AtributosDePersonajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atributoDePersonaje = await _context.atributosDePersonajes
                .Include(a => a.atributo)
                .Include(a => a.personaje)
                .FirstOrDefaultAsync(m => m.id == id);
            if (atributoDePersonaje == null)
            {
                return NotFound();
            }

            return View(atributoDePersonaje);
        }

        // GET: AtributosDePersonajes/Create
        public IActionResult Create()
        {
            ViewData["atributoId"] = new SelectList(_context.atributos, "id", "nombre");
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre");
            return View();
        }

        // POST: AtributosDePersonajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,personajeId,atributoId,nivel")] AtributoDePersonaje atributoDePersonaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atributoDePersonaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["atributoId"] = new SelectList(_context.atributos, "id", "nombre", atributoDePersonaje.atributoId);
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre", atributoDePersonaje.personajeId);
            return View(atributoDePersonaje);
        }

        //public IActionResult CreateAll(Personaje personaje)
        //{
        //    ViewBag.atributos = _context.atributos.ToList();
        //    ViewBag.personajeId = personaje.id;

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateAll([Bind("personajeId,atributoId,nivel")] List<AtributoDePersonaje> atributosDePersonaje)
        //{
        //    Personaje pers = await _context.personajes
        //        .Include(x => x.atributos).ThenInclude(x => x.atributo)
        //        .Include(x => x.raza)
        //        .FirstOrDefaultAsync(m => m.id == atributosDePersonaje[0].personajeId);

        //    CalculosXP cal = new CalculosXP();
        //    int xpGastada = cal.costeCreacionPJ(pers.raza, atributosDePersonaje);
        //    if (ModelState.IsValid && !pers.atributos.Any() && xpGastada <= pers.expActual)
        //    {
        //        foreach (var item in atributosDePersonaje)
        //        {
        //            item.nivel += 2;
        //            _context.Add(item);
        //        }
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("CalcularStats", "Personajes", new { id = atributosDePersonaje.FirstOrDefault().personajeId });
        //    }

        //    ViewBag.Atributos = _context.atributos.ToList();
        //    ViewBag.personajeId = atributosDePersonaje[0].personajeId;
        //    ViewBag.CosteXP = xpGastada;
        //    return View();
        //}


        public async Task<IActionResult> SettearADP(String ADPJSON, int id)
        {
            List<AtributoDePersonaje> atributosDePersonaje = JsonConvert.DeserializeObject<List<AtributoDePersonaje>>(ADPJSON);

            foreach (var item in atributosDePersonaje)
            {
                item.nivel += 2;
                item.personajeId = id;
            }

            if (ModelState.IsValid)
            {
                foreach (var item in atributosDePersonaje)
                {
                    _context.atributosDePersonajes.Add(item);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction("CalcularStats", "Personajes", new { id = id });


            }
            return View();


        }



        // GET: AtributosDePersonajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atributoDePersonaje = await _context.atributosDePersonajes.FindAsync(id);
            if (atributoDePersonaje == null)
            {
                return NotFound();
            }
            ViewData["atributoId"] = new SelectList(_context.atributos, "id", "nombre", atributoDePersonaje.atributoId);
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre", atributoDePersonaje.personajeId);
            return View(atributoDePersonaje);
        }

        // POST: AtributosDePersonajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,personajeId,atributoId,nivel")] AtributoDePersonaje atributoDePersonaje)
        {
            if (id != atributoDePersonaje.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atributoDePersonaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtributoDePersonajeExists(atributoDePersonaje.id))
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
            ViewData["atributoId"] = new SelectList(_context.atributos, "id", "nombre", atributoDePersonaje.atributoId);
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre", atributoDePersonaje.personajeId);
            return View(atributoDePersonaje);
        }

        //public async Task<IActionResult> EditAll(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var personaje = await _context.personajes.Include(x => x.atributos).ThenInclude(x => x.atributo)
        //    .FirstOrDefaultAsync(m => m.id == id);
        //    if (personaje == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewBag.atributos = _context.atributosDePersonajes.Where(x => x.personajeId == id).Include(x => x.atributo).ToList();
        //    ViewData["personajeId"] = id;

        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditAll(int id, [Bind("personajeId,atributoId,nivel")] List<AtributoDePersonaje> atributosDePersonaje)
        //{
        //    if (id != atributosDePersonaje.FirstOrDefault().personajeId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            foreach (var atributoDePersonaje in atributosDePersonaje)
        //            {
        //                _context.Update(atributoDePersonaje);

        //            }
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction("CalcularStats", "Personajes", new { id = atributosDePersonaje.FirstOrDefault().personajeId });
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AtributoDePersonajeExists(atributosDePersonaje.FirstOrDefault().personajeId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction();
        //    }
        //    ViewBag.atributos = _context.atributosDePersonajes.Where(x => x.personajeId == id).Include(x => x.atributo).ToList();
        //    ViewData["personajeId"] = atributosDePersonaje.FirstOrDefault().personajeId;
        //    return View();
        //}

        // GET: AtributosDePersonajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atributoDePersonaje = await _context.atributosDePersonajes
                .Include(a => a.atributo)
                .Include(a => a.personaje)
                .FirstOrDefaultAsync(m => m.id == id);
            if (atributoDePersonaje == null)
            {
                return NotFound();
            }

            return View(atributoDePersonaje);
        }

        // POST: AtributosDePersonajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var atributoDePersonaje = await _context.atributosDePersonajes.FindAsync(id);
            _context.atributosDePersonajes.Remove(atributoDePersonaje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> LevelUp(int? id)
        {
            var atributoPersonaje = await _context.atributosDePersonajes.Include(p => p.personaje).ThenInclude(r => r.raza).Include(h => h.atributo).Where(x => x.id == id).FirstOrDefaultAsync();

            int costo = 5;
            if (atributoPersonaje.atributoId == atributoPersonaje.personaje.raza.idAtributoRelevante || atributoPersonaje.atributoId == atributoPersonaje.personaje.raza.idAtributoRelevante2)
            {
                costo = 4;
            }
            else if (atributoPersonaje.atributoId == atributoPersonaje.personaje.raza.idAtributoPesimo)
            {
                costo = 6;
            }
            int costoSubida = (atributoPersonaje.nivel + 1) * costo;

            if (id == null)
            {
                return NotFound();
            }

            if (atributoPersonaje == null)
            {
                return NotFound();
            }

            ViewBag.posible = costoSubida <= atributoPersonaje.personaje.expActual;
            ViewBag.costo = costoSubida;

            return View(atributoPersonaje);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LevelUp(int id, [Bind("id,personajeId,atributoId,nivel")] AtributoDePersonaje atributoPersonaje)
        {
            Personaje pj = await _context.personajes.Include(x => x.raza).Where(x => x.id == atributoPersonaje.personajeId).FirstOrDefaultAsync();

            int costo = 5;
            if (atributoPersonaje.atributoId == pj.raza.idAtributoRelevante || atributoPersonaje.atributoId == pj.raza.idAtributoRelevante2)
            {
                costo = 4;
            }
            else if (atributoPersonaje.atributoId == pj.raza.idAtributoPesimo)
            {
                costo = 6;
            }

            if (id != atributoPersonaje.id)
            {
                return NotFound();
            }
            int costoSubida = (atributoPersonaje.nivel) * costo;
            if (ModelState.IsValid && costoSubida <= pj.expActual)
            {
                try
                {
                    pj.expActual -= costoSubida;
                    pj.expGastada += costoSubida;
                    _context.Update(pj);
                    _context.Update(atributoPersonaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtributoDePersonajeExists(atributoPersonaje.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CalcularStats", "Personajes", new { id = atributoPersonaje.personajeId });
            }


            return View(atributoPersonaje);
        }


        private bool AtributoDePersonajeExists(int id)
        {
            return _context.atributosDePersonajes.Any(e => e.id == id);
        }
    }
}
