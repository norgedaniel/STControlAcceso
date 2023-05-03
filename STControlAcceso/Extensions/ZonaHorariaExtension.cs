using STCA_DataLib.Model;
using STCA_WebApp.ModelsDTO;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using static STCA_WebApp.Extensions.ZonaHorariaPagingOptions;
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

        public static IQueryable<ZonaHorariaDTO> Ordenar(this IQueryable<ZonaHorariaDTO> zonasHorarias, OrderbyOptionValues? orderbyOptions)
        {
            switch (orderbyOptions ?? OrderbyOptionValues.NOMBRE_ASC)
            {
                case OrderbyOptionValues.NOMBRE_ASC:
                    return zonasHorarias.OrderBy(x => x.Nombre);

                case OrderbyOptionValues.NOMBRE_DESC:
                    return zonasHorarias.OrderByDescending(x => x.Nombre);

                default:
                    return zonasHorarias.OrderBy(x => x.Nombre);

            }
        }

        public static IQueryable<ZonaHorariaDTO> Pagina(this IQueryable<ZonaHorariaDTO> zonasHorarias, ref PagingOptions pagingOptions)
        {
            /* Workflow:
                1- Calcular y retornar la cantidad_paginas tomando en cuenta la cantidad de items y el tamaño_pagina
                2- si numero_pagina > cantidad_paginas-1 => numero_pagina = cantidad_paginas-1
                3- hacer seek y tomar la cantidad de items según tamaño_pagina
                4- si cantidad_rec encontrados es cero => decrementar numero_pagina => ir a 3
                   el lazo de ir a 3 termina cdo numero_pagina = 0
                5- Retornar cantidad_paginas y numero_pagina
             */

            uint cantItems = (uint)zonasHorarias.Count();
            if (cantItems <= 0)
                return zonasHorarias;

            pagingOptions.PageNumberZeroBase = int.Max(pagingOptions.PageNumberZeroBase, 0);

            if (pagingOptions.PageZise < 1) pagingOptions.PageZise = PagingOptions.DEFAULT_PAGE_SIZE;

            pagingOptions.PagesCount = (int)(cantItems / pagingOptions.PageZise);

            if (cantItems % pagingOptions.PageZise > 0)
                pagingOptions.PagesCount++;

            //2 - si numero_pagina > cantidad_paginas-1 => numero_pagina = cantidad_paginas-1
            pagingOptions.PageNumberZeroBase = int.Min(pagingOptions.PageNumberZeroBase, pagingOptions.PagesCount - 1);

            int cantidadRetorno;
            IQueryable<ZonaHorariaDTO> zonasHorariasRetorno;
            do
            {
                //3 - hacer seek y tomar la cantidad de items según tamaño_pagina
                int iSkip = pagingOptions.PageNumberZeroBase * pagingOptions.PageZise;
                zonasHorariasRetorno = zonasHorarias.Skip(iSkip).Take(pagingOptions.PageZise);

                //4 - si cantidad_rec encontrados es cero => decrementar numero_pagina => ir a 3
                //       el lazo de ir a 3 termina cdo numero_pagina = 0
                cantidadRetorno = zonasHorariasRetorno.Count();
                if (cantidadRetorno == 0)
                    pagingOptions.PageNumberZeroBase--;

            } while (cantidadRetorno == 0 && pagingOptions.PageNumberZeroBase > 0);

            return zonasHorariasRetorno;

        }

    }

}
