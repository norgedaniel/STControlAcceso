using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using STCA_DataLib.Data;
using STCA_DataLib.Model;

namespace STCA_WebApp.Controllers
{
    public class RangoTiempoController : Controller
    {
        private readonly MSSQL_STCA_DbContext _context;

        public RangoTiempoController(MSSQL_STCA_DbContext context)
        {
            _context = context;
        }

        // GET: RangoTiempo
        public async Task<IActionResult> Index()
        {
            return _context.RangosTiempos != null ?
                        View(await _context.RangosTiempos.AsNoTracking().ToArrayAsync()) :
                        Problem("Entity set 'MSSQL_STCA_DbContext.RangosTiempos'  is null.");
        }

        // GET: RangoTiempo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RangosTiempos == null)
            {
                return NotFound();
            }

            var rangoTiempo = await _context.RangosTiempos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rangoTiempo == null)
            {
                return NotFound();
            }

            return View(rangoTiempo);
        }

        // GET: RangoTiempo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RangoTiempo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DiaSemana,HoraInicial,HoraFinal")] RangoTiempo rangoTiempo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rangoTiempo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rangoTiempo);
        }

        // GET: RangoTiempo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RangosTiempos == null)
            {
                return NotFound();
            }

            var rangoTiempo = await _context.RangosTiempos.FindAsync(id);
            if (rangoTiempo == null)
            {
                return NotFound();
            }
            return View(rangoTiempo);
        }

        // POST: RangoTiempo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DiaSemana,HoraInicial,HoraFinal")] RangoTiempo rangoTiempo)
        {
            if (id != rangoTiempo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rangoTiempo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RangoTiempoExists(rangoTiempo.Id))
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
            return View(rangoTiempo);
        }

        // GET: RangoTiempo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RangosTiempos == null)
            {
                return NotFound();
            }

            var rangoTiempo = await _context.RangosTiempos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rangoTiempo == null)
            {
                return NotFound();
            }

            return View(rangoTiempo);
        }

        // POST: RangoTiempo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RangosTiempos == null)
            {
                return Problem("Entity set 'MSSQL_STCA_DbContext.RangosTiempos'  is null.");
            }
            var rangoTiempo = await _context.RangosTiempos.FindAsync(id);
            if (rangoTiempo != null)
            {
                _context.RangosTiempos.Remove(rangoTiempo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RangoTiempoExists(int id)
        {
            return (_context.RangosTiempos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
