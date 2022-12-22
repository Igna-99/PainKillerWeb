using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json;
using PainKillerWeb.Context;
using PainKillerWeb.Models.Main;
using PainKillerWeb.Models.Pivot;
using PainKillerWeb.Services;

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
            var painKillerDbContext =
                _context.personajes.Include(p => p.raza)
                .Include(x => x.habilidades).ThenInclude(x => x.Habilidad)
                .Include(x => x.hechizos).ThenInclude(x => x.Hechizo).ThenInclude(x => x.elemento);

            List<string> tipoCostes = new List<string>();
            tipoCostes.Add("VIDA");
            tipoCostes.Add("MANA");
            tipoCostes.Add("ENERGIA");

            ViewBag.tipoCostes = tipoCostes;

            return View(await painKillerDbContext.ToListAsync());
        }

        // GET: Personajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaje = await _context.personajes
                .Include(x => x.atributos).ThenInclude(x => x.atributo)
                .Include(x => x.habilidades).ThenInclude(x => x.Habilidad)
                .Include(x => x.raza)
                .Include(x => x.hechizos).ThenInclude(x => x.Hechizo)
                .Include(x => x.hechizos).ThenInclude(x => x.Hechizo).ThenInclude(x => x.elemento)
                .Include(x => x.hechizos).ThenInclude(x => x.Hechizo).ThenInclude(x => x.distancia)
                .Include(x => x.inventario).ThenInclude(x => x.Item)
                .FirstOrDefaultAsync(m => m.id == id);

            List<string> tipoCostes = new List<string>();
            tipoCostes.Add("VIDA");
            tipoCostes.Add("MANA");
            tipoCostes.Add("ENERGIA");

            ViewBag.tipoCostes = tipoCostes;

            if (personaje == null)
            {
                return NotFound();
            }

            return View(personaje);
        }
        // GET: Personajes/Details/5
        public async Task<IActionResult> Jugar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personaje = await _context.personajes
                .Include(x => x.atributos).ThenInclude(x => x.atributo)
                .Include(x => x.habilidades).ThenInclude(x => x.Habilidad)
                .Include(x => x.raza)
                .Include(x => x.hechizos).ThenInclude(x => x.Hechizo)
                .Include(x => x.hechizos).ThenInclude(x => x.Hechizo).ThenInclude(x => x.elemento)
                .Include(x => x.hechizos).ThenInclude(x => x.Hechizo).ThenInclude(x => x.distancia)
                .Include(x => x.inventario).ThenInclude(x => x.Item)
                .FirstOrDefaultAsync(m => m.id == id);


            List<string> tipoCostes = new List<string>();
            tipoCostes.Add("VIDA");
            tipoCostes.Add("MANA");
            tipoCostes.Add("ENERGIA");

            ViewBag.tipoCostes = tipoCostes;

            if (personaje == null)
            {
                return NotFound();
            }

            return View(personaje);
        }
        // GET: Personajes/Create
        //public IActionResult Create()
        //{
        //    ViewData["razaId"] = new SelectList(_context.raza, "id", "nombre");
        //    return View();
        //}

        ////POST: Personajes/Create
        ////To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("id,nombre,razaId,expActual")] Personaje personaje)
        //{
        //    var pj = await _context.personajes
        //        .Include(x => x.atributos).ThenInclude(x => x.atributo)
        //        .Include(x => x.raza)
        //        .FirstOrDefaultAsync(m => m.id == personaje.id);

        //    if (ModelState.IsValid)
        //    {

        //        _context.Add(personaje);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("CreateAll", "AtributosDePersonajes", personaje);
        //    }
        //    ViewData["razaId"] = new SelectList(_context.raza, "id", "nombre", personaje.razaId);
        //    return View(personaje);
        //}


        public IActionResult CreateConAtributos()
        {
            ViewBag.atributos = _context.atributos.ToList();
            ViewData["razaId"] = new SelectList(_context.raza, "id", "nombre");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConAtributos([Bind("id,nombre,razaId,expActual")] Personaje personaje, [Bind("atributoId,nivel")] List<AtributoDePersonaje> atributosDePersonaje)
        {

            //---------------- Inicializaciones ==>
            Raza razaPersonaje = await _context.raza.FirstOrDefaultAsync(x => x.id == personaje.razaId);
            CalculosXP cal = new CalculosXP();
            int xpGastada = cal.costeCreacionPJ(razaPersonaje, atributosDePersonaje);

            //---------------->

            if (ModelState.IsValid && xpGastada <= personaje.expActual)
            {
                personaje.expActual -= xpGastada;
                personaje.expGastada += xpGastada;

                _context.personajes.Add(personaje);
                await _context.SaveChangesAsync();

                int ultimoId = _context.personajes.ToList().Last().id;

                return RedirectToAction("SettearADP", "AtributosDePersonajes", new { ADPJSON = JsonConvert.SerializeObject(atributosDePersonaje), id = ultimoId });
            }

            ViewData["razaId"] = new SelectList(_context.raza, "id", "nombre", personaje.razaId);
            ViewBag.Atributos = _context.atributos.ToList();
            ViewBag.CosteXP = xpGastada;
            return View();

        }

        public async Task<IActionResult> CalcularStats(int id)
        {
            Personaje pers = _context.personajes.Where(x => x.id == id).Include(x => x.atributos).ThenInclude(x => x.atributo).FirstOrDefault();

            if (pers.id > 0)
            {
                //Agrega lso calculos para las stats correspondientes
                pers.vidaMax = (pers.atributos.Where(x => x.atributo.id == 1).First().nivel + pers.atributos.Where(x => x.atributo.id == 2).First().nivel) * 6;
                pers.manaMax = (pers.atributos.Where(x => x.atributo.id == 3).First().nivel + pers.atributos.Where(x => x.atributo.id == 4).First().nivel) * 6;
                pers.energiaMax = (pers.atributos.Where(x => x.atributo.id == 5).First().nivel + pers.atributos.Where(x => x.atributo.id == 6).First().nivel) * 6;

                pers.vidaAct = pers.vidaMax;
                pers.manaAct = pers.manaMax;
                pers.energiaAct = pers.energiaMax; 
                _context.Update(pers);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Details", "Personajes", new { id = pers.id });
        }

        public async Task<IActionResult> Descansar(int id)
        {
            Personaje pers = _context.personajes.Where(x => x.id == id).Include(x => x.atributos).ThenInclude(x => x.atributo).FirstOrDefault();

            if (pers != null)
            {
                pers.vidaAct = pers.vidaMax;
                pers.manaAct = pers.manaMax;
                pers.energiaAct = pers.energiaMax;
                _context.Update(pers);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Jugar", "Personajes", new { id = pers.id });
        }

        public async Task<IActionResult> DescansarDeAUno(int id, int numero)
        {
            Personaje pers = _context.personajes.Where(x => x.id == id).Include(x => x.atributos).ThenInclude(x => x.atributo).FirstOrDefault();
            if (pers != null)
            {
                switch (numero)
                {
                    case 1:
                        pers.vidaAct = pers.vidaMax;

                        break;
                    case 2:
                        pers.manaAct = pers.manaMax;

                        break;
                    case 3:
                        pers.energiaAct = pers.energiaMax;

                        break;
                }
                _context.Update(pers);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("Jugar", "Personajes", new { id = pers.id });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarDeAUno(int id, int num, int tipo)
        {
            Personaje personaje = await _context.personajes.Where(x => x.id == id).FirstOrDefaultAsync();

            if (personaje != null)
            {
                switch (tipo)
                {
                    case 1:
                        personaje.vidaAct = this.calculoDeEdicion(personaje.vidaAct, num, personaje.vidaMax);
                        break;
                    case 2:
                        personaje.manaAct = this.calculoDeEdicion(personaje.manaAct, num, personaje.manaMax);
                        break;
                    case 3:
                        personaje.energiaAct = this.calculoDeEdicion(personaje.energiaMax, num, personaje.energiaMax);
                        break;
                }


                _context.Update(personaje);
                await _context.SaveChangesAsync();
                return RedirectToAction("Jugar", new { id = personaje.id });
            }

            return RedirectToAction("index");

        }

        private int calculoDeEdicion(int stats, int num, int max) 
        {
            if ((stats + num) >= max)
            {
                stats = max;
            }
            else if ((stats + num) <= 0)
            {
                stats = 0;
            }
            else
            {
                stats += num;
            }

            return stats;

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
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,razaId,expActual,expGastada,vidaMax,manaMax,energiaMax,vidaAct,energiaAct,manaAct")] Personaje personaje)
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
                return RedirectToAction("Details", new { id = personaje.id });
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

            var personaje =
                await _context.personajes
                .Include(x => x.atributos).ThenInclude(x => x.atributo)
                .Include(x => x.habilidades).ThenInclude(x => x.Habilidad)
                .Include(x => x.raza).Include(x => x.hechizos).ThenInclude(x => x.Hechizo)
                .Include(x => x.inventario)
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
