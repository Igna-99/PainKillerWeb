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
    public class ItemsDePersonajesController : Controller
    {
        private readonly PainKillerDbContext _context;

        public ItemsDePersonajesController(PainKillerDbContext context)
        {
            _context = context;
        }

        // GET: ItemsDePersonajes
        public async Task<IActionResult> Index()
        {
            var painKillerDbContext = _context.itemsDePersonajes.Include(i => i.Item).Include(i => i.Personaje);
            return View(await painKillerDbContext.ToListAsync());
        }

        // GET: ItemsDePersonajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemDePersonaje = await _context.itemsDePersonajes
                .Include(i => i.Item)
                .Include(i => i.Personaje)
                .FirstOrDefaultAsync(m => m.id == id);
            if (itemDePersonaje == null)
            {
                return NotFound();
            }

            return View(itemDePersonaje);
        }

        // GET: ItemsDePersonajes/Create
        public IActionResult Create()
        {
            ViewData["itemId"] = new SelectList(_context.items, "id", "nombre");
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre");
            return View();
        }

        // POST: ItemsDePersonajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,personajeId,itemId,cantidad,descripcion")] ItemDePersonaje itemDePersonaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemDePersonaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["itemId"] = new SelectList(_context.items, "id", "nombre", itemDePersonaje.itemId);
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre", itemDePersonaje.personajeId);
            return View(itemDePersonaje);
        }
        public IActionResult CreateFor(int id)
        {

            var personaje = _context.personajes.Find(id);

            if (personaje == null)
            {
                return NotFound();
            }

            ViewData["ItemId"] = new SelectList(_context.items, "id", "nombre");
            ViewData["personajeId"] = id;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFor([Bind("personajeId, itemId, cantidad, descripcion")] ItemDePersonaje itemDePersonaje)
        {
            Personaje pj = _context.personajes
                .Include(x => x.inventario).ThenInclude(x => x.Item)
                .FirstOrDefault(m => m.id == itemDePersonaje.personajeId);


            if (ModelState.IsValid)
            {
                _context.Update(pj);
                _context.Add(itemDePersonaje);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Personajes", new { id = itemDePersonaje.personajeId });
            }


            ViewData["ItemId"] = new SelectList(_context.items, "id", "nombre");
            ViewData["personajeId"] = itemDePersonaje.personajeId;

            return View();
        }


        [HttpPost, ActionName("DeleteInModal")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteInModal(int id)
        {
            var itemsDePersonajes = await _context.itemsDePersonajes.FindAsync(id);
            _context.itemsDePersonajes.Remove(itemsDePersonajes);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Personajes", new { id = itemsDePersonajes.personajeId });
        }

        // GET: ItemsDePersonajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemDePersonaje = await _context.itemsDePersonajes.FindAsync(id);
            if (itemDePersonaje == null)
            {
                return NotFound();
            }
            ViewData["itemId"] = new SelectList(_context.items, "id", "nombre", itemDePersonaje.itemId);
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre", itemDePersonaje.personajeId);
            return View(itemDePersonaje);
        }

        // POST: ItemsDePersonajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,personajeId,itemId,cantidad,descripcion")] ItemDePersonaje itemDePersonaje)
        {
            if (id != itemDePersonaje.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemDePersonaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemDePersonajeExists(itemDePersonaje.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Personajes", new { id = itemDePersonaje.personajeId });

            }
            ViewData["itemId"] = new SelectList(_context.items, "id", "nombre", itemDePersonaje.itemId);
            ViewData["personajeId"] = new SelectList(_context.personajes, "id", "nombre", itemDePersonaje.personajeId);
            return View(itemDePersonaje);
        }

        // GET: ItemsDePersonajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemDePersonaje = await _context.itemsDePersonajes
                .Include(i => i.Item)
                .Include(i => i.Personaje)
                .FirstOrDefaultAsync(m => m.id == id);
            if (itemDePersonaje == null)
            {
                return NotFound();
            }

            return View(itemDePersonaje);
        }

        // POST: ItemsDePersonajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemDePersonaje = await _context.itemsDePersonajes.FindAsync(id);
            _context.itemsDePersonajes.Remove(itemDePersonaje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemDePersonajeExists(int id)
        {
            return _context.itemsDePersonajes.Any(e => e.id == id);
        }
    }
}
