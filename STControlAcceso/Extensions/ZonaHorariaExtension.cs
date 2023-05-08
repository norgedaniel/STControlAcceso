using STCA_DataLib.Model;
using STCA_WebApp.ModelsDTO;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using static STCA_WebApp.Models.ZonaHorariaViewModel;

namespace STCA_WebApp.Extensions
{
    public static class ZonaHorariaExtension
    {
        public static IQueryable<ZonaHorariaDTO> MapZonaHorariaToDto(this IQueryable<ZonaHoraria> zonasHorarias)
        {
            return zonasHorarias.Select(zona => new ZonaHorariaDTO
            {
                Id = zona.Id,
                Nombre = zona.Nombre
            });
        }

        public static IQueryable<ZonaHorariaDTO> Ordenar(this IQueryable<ZonaHorariaDTO> zonasHorarias, string SortFieldName, bool SortOrderDescending)
        {
            if (SortOrderDescending)
                switch (SortFieldName)
                {
                    default:
                        return zonasHorarias.OrderByDescending(x => x.Nombre);
                }
            else
                switch (SortFieldName)
                {
                    default:
                        return zonasHorarias.OrderBy(x => x.Nombre);
                }

        }

        public static IQueryable<ZonaHorariaDTO> Pagina(this IQueryable<ZonaHorariaDTO> zonasHorarias,
                                                        ref int currentPageNumberZeroBase, ref int pageZise, ref int pagesCount,
                                                        string pagingActionRequest = "")
        {
            /* Workflow:
                1- Calcular y retornar la cantidad_paginas tomando en cuenta la cantidad de items y el tamaño_pagina
                2- si numero_pagina > cantidad_paginas-1 => numero_pagina = cantidad_paginas-1
                3- hacer seek y tomar la cantidad de items según tamaño_pagina
                4- si cantidad_rec encontrados es cero => decrementar numero_pagina => ir a 3
                   el lazo de ir a 3 termina cdo numero_pagina = 0
                5- Retornar cantidad_paginas y numero_pagina
             */

            switch (pagingActionRequest)
            {
                case "<<":
                    currentPageNumberZeroBase = 0;
                    break;

                case "<":
                    currentPageNumberZeroBase--;
                    break;

                case ">":
                    currentPageNumberZeroBase++;
                    break;

                case ">>":
                    currentPageNumberZeroBase = pagesCount - 1;
                    break;

                default:
                    break;
            }

            uint cantItems = (uint)zonasHorarias.Count();
            if (cantItems <= 0)
                return zonasHorarias;

            currentPageNumberZeroBase = int.Max(currentPageNumberZeroBase, 0);

            if (pageZise < 1) pageZise = PagingOptions.DEFAULT_PAGE_SIZE;

            pagesCount = (int)(cantItems / pageZise);

            if (cantItems % pageZise > 0)
                pagesCount++;

            //2 - si numero_pagina > cantidad_paginas-1 => numero_pagina = cantidad_paginas-1
            currentPageNumberZeroBase = int.Min(currentPageNumberZeroBase, pagesCount - 1);

            int cantidadRetorno;
            IQueryable<ZonaHorariaDTO> zonasHorariasRetorno;
            do
            {
                //3 - hacer seek y tomar la cantidad de items según tamaño_pagina
                int iSkip = currentPageNumberZeroBase * pageZise;
                zonasHorariasRetorno = zonasHorarias.Skip(iSkip).Take(pageZise);

                //4 - si cantidad_rec encontrados es cero => decrementar numero_pagina => ir a 3
                //       el lazo de ir a 3 termina cdo numero_pagina = 0
                cantidadRetorno = zonasHorariasRetorno.Count();
                if (cantidadRetorno == 0)
                    currentPageNumberZeroBase--;

            } while (cantidadRetorno == 0 && currentPageNumberZeroBase > 0);

            return zonasHorariasRetorno;

        }

    }

}
