using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AluguelMaquinas.Data;
using AluguelMaquinas.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace AluguelMaquinas.Controllers
{
    public class AlugueisEquipamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlugueisEquipamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AlugueisEquipamentos
        public async Task<IActionResult> Index(int? id)
        {            
            if (id == null)
            {
                var applicationDbContext = _context.AluguelEquipamento.Include(a => a.Aluguel).Include(a => a.Equipamento).Include(a => a.Aluguel.Cliente);
                return View(await applicationDbContext.ToListAsync());
            } 
            else
            {
                var applicationDbContext = _context.AluguelEquipamento.Include(a => a.Aluguel).Include(a => a.Equipamento).Include(a => a.Aluguel.Cliente).Where(a => a.Aluguel.Id == id);
                ViewData["id"] = id;
                return View(await applicationDbContext.ToListAsync());
            }            
        }

        // GET: AlugueisEquipamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AluguelEquipamento == null)
            {
                return NotFound();
            }

            var aluguelEquipamento = await _context.AluguelEquipamento
                .Include(a => a.Aluguel)
                .Include(a => a.Equipamento)
                .Include(a => a.Aluguel.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluguelEquipamento == null)
            {
                return NotFound();
            }

            return View(aluguelEquipamento);
        }

        // GET: AlugueisEquipamentos/Create
        public IActionResult Create(int? al)
        {
            //ViewData["AluguelId"] = new SelectList(_context.Aluguel, "Id", "Id");
            if (al == null)
            {
                ViewData["AluguelId"] = new SelectList(
                    (from s in _context.Aluguel.Include(a => a.Cliente).ToList()
                     select new
                     {
                         Id = s.Id,
                         Nome = s.Cliente.Nome + " " + s.DataAluguel.ToString("dd/MM/yyyy")
                     })
                    , "Id", "Nome");
            } 
            else
            {
                ViewData["AluguelId"] = new SelectList(
                    (from s in _context.Aluguel.Where(a => a.Id == al).Include(a => a.Cliente).ToList()
                     select new
                     {
                         Id = s.Id,
                         Nome = s.Cliente.Nome + " " + s.DataAluguel.ToString("dd/MM/yyyy")
                     })
                    , "Id", "Nome");
            }
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "Id", "Nome");
            return View();
        }

        // POST: AlugueisEquipamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AluguelId,EquipamentoId")] AluguelEquipamento aluguelEquipamento)
        {
            if (ModelState.IsValid)
            {
                var eq = _context.Equipamento.Where(e => e.Id == aluguelEquipamento.EquipamentoId).First();
                aluguelEquipamento.ValorDia = eq.ValorDia;
                _context.Add(aluguelEquipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", new { al = aluguelEquipamento.AluguelId });
            }
            ViewData["AluguelId"] = new SelectList(_context.Aluguel, "Id", "Id", aluguelEquipamento.AluguelId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "Id", "Id", aluguelEquipamento.EquipamentoId);
            return View(aluguelEquipamento);
        }

        // GET: AlugueisEquipamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AluguelEquipamento == null)
            {
                return NotFound();
            }

            var aluguelEquipamento = await _context.AluguelEquipamento.FindAsync(id);
            if (aluguelEquipamento == null)
            {
                return NotFound();
            }
            ViewData["AluguelId"] = new SelectList(
                (from s in _context.Aluguel.Include(a => a.Cliente).ToList()
                 select new
                 {
                     Id = s.Id,
                     Nome = s.Cliente.Nome + " " + s.DataAluguel.ToString("dd/MM/yyyy")
                 })
                , "Id", "Nome");
            //ViewData["AluguelId"] = new SelectList(_context.Aluguel, "Id", "Id", aluguelEquipamento.AluguelId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "Id", "Nome", aluguelEquipamento.EquipamentoId);
            return View(aluguelEquipamento);
        }

        // POST: AlugueisEquipamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AluguelId,EquipamentoId")] AluguelEquipamento aluguelEquipamento)
        {
            if (id != aluguelEquipamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluguelEquipamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AluguelEquipamentoExists(aluguelEquipamento.Id))
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
            ViewData["AluguelId"] = new SelectList(_context.Aluguel, "Id", "Id", aluguelEquipamento.AluguelId);
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamento, "Id", "Id", aluguelEquipamento.EquipamentoId);
            return View(aluguelEquipamento);
        }

        // GET: AlugueisEquipamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AluguelEquipamento == null)
            {
                return NotFound();
            }

            var aluguelEquipamento = await _context.AluguelEquipamento
                .Include(a => a.Aluguel)
                .Include(a => a.Equipamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluguelEquipamento == null)
            {
                return NotFound();
            }

            return View(aluguelEquipamento);
        }

        // POST: AlugueisEquipamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AluguelEquipamento == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AluguelEquipamento'  is null.");
            }
            var aluguelEquipamento = await _context.AluguelEquipamento.FindAsync(id);
            if (aluguelEquipamento != null)
            {
                _context.AluguelEquipamento.Remove(aluguelEquipamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AluguelEquipamentoExists(int id)
        {
          return _context.AluguelEquipamento.Any(e => e.Id == id);
        }
    }
}
