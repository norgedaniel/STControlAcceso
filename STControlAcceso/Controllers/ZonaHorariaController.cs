using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared;
using STCA_DataLib.Data;
using STCA_DataLib.Model;
using STCA_DataLib.Repositories;

namespace STCA_WebApp.Controllers
{
    public class ZonaHorariaController : Controller
    {
        private readonly ISCTA_UnitOfWork _unitOfWork;

        public ZonaHorariaController(ISCTA_UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: ZonaHoraria
        public async Task<IActionResult> Index(string sortOrder)
        {
            if (_unitOfWork.ZonaHorariaRepository == null)
                Problem("Repository set 'ISCTA_UnitOfWork.ZonaHorariaRepository' is null.");

            IEnumerable<ZonaHoraria> zonas_horarias = await _unitOfWork.ZonaHorariaRepository.GetAsync();

            //ViewData["NombreSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nombre_desc" : "";

            //switch (sortOrder)
            //{
            //    case "nombre_desc":
            //        zonas_horarias = zonas_horarias.OrderByDescending(s => s.Nombre);
            //        break;

            //    default:
            //        zonas_horarias = zonas_horarias.OrderBy(s => s.Nombre);
            //        break;

            //}

            return View(zonas_horarias);

            //return _context.ZonasHorarias != null ?
            //            View(await _context.ZonasHorarias.AsNoTracking().ToArrayAsync()) :
            //            Problem("Entity set 'MSSQL_STCA_DbContext.ZonasHorarias'  is null.");
        }

        // GET: ZonaHoraria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            ZonaHoraria zonaHoraria = await _unitOfWork.ZonaHorariaRepository.GetByIDAsync(id);

            if (zonaHoraria == null)
            {
                return NotFound();
            }

            return View(zonaHoraria);
        }

        // GET: ZonaHoraria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ZonaHoraria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] ZonaHoraria zonaHoraria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.ZonaHorariaRepository.InsertAsync(zonaHoraria);
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    if (ErrorCodeShared.GetMostInnerExceptionMessage(ex).Contains("UNIQUE"))
                    {
                        TempData["ErrorMes"] = "Este nombre de Zona Horaria ya existe.";
                        return View(zonaHoraria);
                    }
                    else
                        throw;
                }

            }
            return View(zonaHoraria);
        }

        // GET: ZonaHoraria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            ZonaHoraria zonaHoraria = await _unitOfWork.ZonaHorariaRepository.GetByIDAsync(id);

            if (zonaHoraria == null)
                return NotFound();

            return View(zonaHoraria);

        }

        // POST: ZonaHoraria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] ZonaHoraria zonaHoraria)
        {
            if (id != zonaHoraria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.ZonaHorariaRepository.Update(zonaHoraria);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!ZonaHorariaExists(zonaHoraria.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                catch (DbUpdateException ex)
                {
                    if (ErrorCodeShared.GetMostInnerExceptionMessage(ex).Contains("UNIQUE"))
                    {
                        TempData["ErrorMes"] = "Este nombre de Zona Horaria ya existe.";
                        return View(zonaHoraria);
                    }
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(zonaHoraria);
        }

        // GET: ZonaHoraria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            bool result = await _unitOfWork.ZonaHorariaRepository.DeleteAsync(id);
            if (!result)
                return NotFound();

            return View();
        }

        // POST: ZonaHoraria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool result = await _unitOfWork.ZonaHorariaRepository.DeleteAsync(id);
            if (!result)
                return NotFound();
 
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool ZonaHorariaExists(int id)
        //{
        //    return (_context.ZonasHorarias?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
