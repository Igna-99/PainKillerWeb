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
    public class AtributosController : Controller
    {
        private readonly PainKillerDbContext _context;

        public AtributosController(PainKillerDbContext context)
        {
            _context = context;
        }

        // GET: Atributos
        public async Task<IActionResult> Index()
        {
            return View(await _context.atributos.ToListAsync());
        }

        // GET: Atributos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atributo = await _context.atributos
                .FirstOrDefaultAsync(m => m.id == id);
            if (atributo == null)
            {
                return NotFound();
            }

            return View(atributo);
        }

        // GET: Atributos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Atributos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre")] Atributo atributo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atributo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(atributo);
        }

        // GET: Atributos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atributo = await _context.atributos.FindAsync(id);
            if (atributo == null)
            {
                return NotFound();
            }
            return View(atributo);
        }

        // POST: Atributos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre")] Atributo atributo)
        {
            if (id != atributo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atributo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtributoExists(atributo.id))
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
            return View(atributo);
        }

        // GET: Atributos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atributo = await _context.atributos
                .FirstOrDefaultAsync(m => m.id == id);
            if (atributo == null)
            {
                return NotFound();
            }

            return View(atributo);
        }

        // POST: Atributos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var atributo = await _context.atributos.FindAsync(id);
            _context.atributos.Remove(atributo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtributoExists(int id)
        {
            return _context.atributos.Any(e => e.id == id);
        }
    }
}
