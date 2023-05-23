using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Shared;
using STCA_DataLib.Data;
using STCA_DataLib.Model;
using STCA_WebApp.DTO;
using STCA_WebApp.Extensions;
using STCA_WebApp.Models;
using STCA_WebApp.Services;
using static STCA_WebApp.Extensions.ZonaHorariaExtension;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http.Features;
using System.Net.NetworkInformation;
using Azure.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace STCA_WebApp.Controllers
{
    public class ZonaHorariaController_BACKUP : Controller
    {
        private readonly ISTCA_DbService _STCA_DbService;

        public ZonaHorariaController_BACKUP(ISTCA_DbService service)
        {
            _STCA_DbService = service;
        }

        [HttpPost]
        public async Task<IActionResult> RefrescaForm(string request, int pageZise)
        {
            return await GetModel(request, pageZise);
        }


        // GET: ZonaHorariaController
        public async Task<IActionResult> Index()
        {
            return await GetModel();
        }

        private async Task<IActionResult> GetModel(string request = "", int newPageZise = 0)
        {
            /*
             * getOptions indica una acción de ordenamiento o paginado, acometida por el usuario para realizar la búsqueda de los datos soicitados.
             * Para una acción de ordenamiento tendrá el nombre del atributo o columna presionada.
             * Para una acción de paginado tendrá: #INI, #ANT, #SIG, #FIN
             * 
             * Cuando tiene un valor valido de ordenamiento, se conmuta OrderbyOption almacenado en TempData["ZonaHorariaPagingOptions"], según la columna presionada.
             * En caso contrario, OrderbyOption se queda con el valor que trae en TempData["ZonaHorariaPagingOptions"]. 
             * 
             */

            // los valores almacenados en TempData son eliminados luego de su primera lectura  
            string sortField = (string)(TempData["LastSortField"] ?? "NOMBRE");
            bool sortOrderDesc = (bool)(TempData["LastSortOrderDesc"] ?? false);

            int pageNumberZeroBase = (int)(TempData["PageNumberZeroBase"] ?? 0);

            int pageZise;
            if (newPageZise > 0)
                pageZise = newPageZise;
            else
                pageZise = (int)(TempData["PageZise"] ?? PagingOptions.DEFAULT_PAGE_SIZE);

            int pagesCount = (int)(TempData["PagesCount"] ?? 0);

            string pagingActionRequest = string.Empty;

            if (!request.IsNullOrEmpty())
            {
                request = request.Trim().ToUpper();

                if (request == "<<" || request == "<" || request == ">" || request == ">>")
                    pagingActionRequest = request;
                else
                {
                    sortField = request;
                    sortOrderDesc = !sortOrderDesc;
                }

            }

            // la actualización de TempData debe hacerse después de correr _STCA_DbService.GetZonasHorariasAsync
            // pues dentro de ese llamado puede actualizarse los datos del paginado
            ZonaHorariaListDTO zonaHorariaListDTO = await _STCA_DbService.GetZonasHorariasAsync(pageNumberZeroBase, pageZise, pagesCount, pagingActionRequest,
                                                                                                sortField, sortOrderDesc);

            // actualizando TempData para mantener memoria en el controlador
            TempData["LastSortField"] = sortField;
            TempData["LastSortOrderDesc"] = sortOrderDesc;

            TempData["PageNumberZeroBase"] = zonaHorariaListDTO.PageNumberZeroBase;
            TempData["PageZise"] = zonaHorariaListDTO.PageZise;
            TempData["PagesCount"] = zonaHorariaListDTO.PagesCount;

            // retornando modelo a la vista
            return View("Index", new ZonaHorariaViewModel
            {
                Items = zonaHorariaListDTO.Items,
                PageNumberZeroBase = zonaHorariaListDTO.PageNumberZeroBase,
                PagesCount = zonaHorariaListDTO.PagesCount,
                PageZiseOptions = PagingOptions.GetPageZiseOptions(zonaHorariaListDTO.PageZise),
                LastSortField = sortField,
                LastSortOrderDesc = sortOrderDesc
            });

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
