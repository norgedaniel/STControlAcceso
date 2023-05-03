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

namespace STCA_WebApp.Controllers
{
    [BindProperties]
    public class ZonaHorariaController : Controller
    {
        private readonly ISTCA_DbService _STCA_DbService;

        public ZonaHorariaController(ISTCA_DbService service)
        {
            _STCA_DbService = service;
        }

        [HttpPost]
        public async Task<IActionResult> RefrescaForm(PagingOptions pagingOptions)
        {
            return View("Index", await GetModel(pagingOptions));
        }


        // GET: ZonaHorariaController
        public async Task<IActionResult> Index(string getOption)
        {
            return View("Index", await GetModel(null, getOption));
        }

        private async Task<ZonaHorariaViewModel> GetModel(PagingOptions? pagingOptions = null, string getOption = "")
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

            if (getOption == null)
                getOption = string.Empty;
            else
                getOption = getOption.ToUpper();

            string strOpcionesPaginado = "INI,ANT,SIG,FIN";


            // con esta asignación, desaparece TempData["ZonaHorariaPagingOptions"], se hace null
            var tempData = TempData["ZonaHorariaPagingOptions"];
            ZonaHorariaPagingOptions zonaHorariaPagingOptions = ZonaHorariaPagingOptions.Deserialize(tempData);
            if (pagingOptions != null)
                zonaHorariaPagingOptions.PagingOptions = pagingOptions;


            if (!getOption.IsNullOrEmpty())
                if (strOpcionesPaginado.Contains(getOption))
                {
                    // se solicita un paginado
                    switch (getOption)
                    {
                        case "INI":
                            zonaHorariaPagingOptions.PagingOptions.PageNumberZeroBase = 0;
                            zonaHorariaPagingOptions.PagingOptions.PageZise = 3;
                            break;

                        case "ANT":
                            zonaHorariaPagingOptions.PagingOptions.PageNumberZeroBase--;
                            break;

                        case "SIG":
                            zonaHorariaPagingOptions.PagingOptions.PageNumberZeroBase++;
                            break;

                        case "FIN":
                            zonaHorariaPagingOptions.PagingOptions.PageNumberZeroBase = zonaHorariaPagingOptions.PagingOptions.PagesCount;
                            zonaHorariaPagingOptions.PagingOptions.PageZise = 30;
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    // se puede estar solicitando un ordenamiento
                    if (getOption == "NOMBRE") zonaHorariaPagingOptions.ConmutaOrderbyNombre();
                }


            // la actualización de TempData debe hacerse después de correr _STCA_DbService.GetZonasHorariasAsync
            // pues dentro de ese llamado puede actualizarse los datos del paginado
            ZonaHorariaListDTO zonaHorariaListDTO = await _STCA_DbService.GetZonasHorariasAsync(zonaHorariaPagingOptions);

            // actualizando zonaHorariaPagingOptions y guardándola en el diccionaro para mantener memoria en el controlador
            zonaHorariaPagingOptions.PagingOptions = zonaHorariaListDTO.PagingOptions;
            TempData["ZonaHorariaPagingOptions"] = JsonConvert.SerializeObject(zonaHorariaPagingOptions);

            // seteando valores para la vista
            ViewBag.pageZiseOptions = PagingOptions.GetPageZiseOptions(zonaHorariaListDTO.PagingOptions.PageZise);
            ViewBag.PagingOption = zonaHorariaListDTO.PagingOptions;

            // retornando un nuevo modelo
            return new()
            {
                Items = zonaHorariaListDTO.Items
            };

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
