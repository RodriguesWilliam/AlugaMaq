using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AluguelMaquinas.Data;
using AluguelMaquinas.Models;
using static NuGet.Packaging.PackagingConstants;

namespace AluguelMaquinas.Controllers
{
    public class AlugueisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlugueisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alugueis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Aluguel.Include(a => a.Cliente).Include(b => b.AluguelEquipamentos);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Alugueis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Aluguel == null)
            {
                return NotFound();
            }

            var aluguel = await _context.Aluguel
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluguel == null)
            {
                return NotFound();
            }

            return View(aluguel);
        }

        // GET: Alugueis/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
            return View();
        }

        // POST: Alugueis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EnderecoAluguel,CepAluguel,MunicipioAluguel,DataAluguel,DiasAluguel,ClienteId")] Aluguel aluguel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluguel);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "AlugueisEquipamentos", new { Id = aluguel.Id });
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Id", aluguel.ClienteId);
            return View(aluguel);
        }

        // GET: Alugueis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Aluguel == null)
            {
                return NotFound();
            }

            var aluguel = await _context.Aluguel.FindAsync(id);
            if (aluguel == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", aluguel.ClienteId);
            return View(aluguel);
        }

        // POST: Alugueis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EnderecoAluguel,CepAluguel,MunicipioAluguel,DataAluguel,DiasAluguel,ClienteId")] Aluguel aluguel)
        {
            if (id != aluguel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluguel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AluguelExists(aluguel.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Id", aluguel.ClienteId);
            return View(aluguel);
        }

        // GET: Alugueis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Aluguel == null)
            {
                return NotFound();
            }

            var aluguel = await _context.Aluguel
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluguel == null)
            {
                return NotFound();
            }

            return View(aluguel);
        }

        // POST: Alugueis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Aluguel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Aluguel'  is null.");
            }
            var aluguel = await _context.Aluguel.FindAsync(id);
            if (aluguel != null)
            {
                _context.Aluguel.Remove(aluguel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AluguelExists(int id)
        {
            return _context.Aluguel.Any(e => e.Id == id);
        }
    }
}
