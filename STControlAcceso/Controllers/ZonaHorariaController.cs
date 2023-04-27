using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Shared;
using STCA_DataLib.Data;
using STCA_DataLib.Model;
using STCA_WebApp.Extensions;
using STCA_WebApp.Models;
using STCA_WebApp.Services;
using static STCA_WebApp.Extensions.ZonaHorariaExtension;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IActionResult> Index(string columorder)
        {
            /*
              cuando columorder tiene un valor valido, se conmuta OrderbyOption almacenado en TempData["OrderbyOption"].
              en caso contrario, OrderbyOption se queda con el valor que trae en TempData["OrderbyOption"]
             */

            if (columorder == null) columorder = string.Empty;

            // con esta asignación, desaparece TempData["OrderbyOption"], se hace null
            ZonaHorariaOrderbyOptions orderby = ZonaHorariaExtension.Parse(TempData["OrderbyOption"]) ?? ZonaHorariaOrderbyOptions.NOMBRE_ASC;

            if (columorder.Contains("nombre")) orderby = orderby.ConmutaOrderbyNombre();

            TempData["OrderbyOption"] = orderby;

            var items = await _STCA_DbService.GetZonasHorariasAsync(orderby);

            ZonaHorariaViewModel model = new ZonaHorariaViewModel
            {
                Items = items
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

            try
            {
                bool ok = await _STCA_DbService.CreateZonaHorariaAsync(item);
                if (!ok)
                    return BadRequest("No se pudo adicionar la nueva Zona Horaria.");

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                if (ex.ContainsString("UNIQUE"))
                    return BadRequest("La Zona Horaria a crear ya existe en la Base de Datos.");
                else
                    return RedirectToAction(nameof(Index));
            }

        }



        //POST: ZonaHorariaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string? itemsId)
        {
            if (!ModelState.IsValid || itemsId == null)
                return RedirectToAction(nameof(Index));

            foreach (string strId in itemsId.Split(','))
            {
                try
                {
                    if (int.TryParse(strId, out int id))
                    {
                        bool ok = await _STCA_DbService.DeleteZonaHorariaAsync(id);
                        //if (!ok)
                        //    return BadRequest("No se pudo adicionar la nueva Zona Horaria.");
                    }

                }
                catch (Exception)
                {
                    //if (ex.ContainsString("UNIQUE"))
                    //    return BadRequest("La Zona Horaria a crear ya existe en la Base de Datos.");
                    //else
                    //return RedirectToAction(nameof(Index));
                }

            }

            return RedirectToAction(nameof(Index));

        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (!ModelState.IsValid )
        //        return RedirectToAction(nameof(Index));

        //    bool ok = await _STCA_DbService.DeleteZonaHorariaAsync(id);
        //    //if (!ok)
        //    //    return BadRequest("No se pudo adicionar la nueva Zona Horaria.");

        //    return RedirectToAction(nameof(Index));

        //}


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

    }

}
