using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using STCA_DataLib.Data;
using STCA_DataLib.Model;
using STCA_WebApp.Models;
using STCA_WebApp.Services;

namespace STCA_WebApp.Controllers
{
    public class ZonaHorariaController : Controller
    {
        private readonly ISTCA_DbService _STCA_DbService;

        public ZonaHorariaController(ISTCA_DbService service)
        {
            _STCA_DbService = service;
        }

        // GET: ZonaHorariaController
        public async Task<IActionResult> Index()
        {
            var arreglo = await _STCA_DbService.GetZonasHorariasAsync();

            ZonaHorariaViewModel model = new ZonaHorariaViewModel
            {
                ZonasHorarias = arreglo
            };

            return View(model);

        }


        // POST: ZonaHorariaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZonaHoraria item)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            bool ok = await _STCA_DbService.CreateZonaHorariaAsync(item);
            if (!ok)
                return BadRequest("No se pudo adicionar la nueva Zona Horaria.");

            return RedirectToAction(nameof(Index));
        }



        // GET: ZonaHorariaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ZonaHorariaController/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: ZonaHorariaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ZonaHorariaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ZonaHorariaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ZonaHorariaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
