using STCA_DataLib.Model;
using STCA_WebApp.ModelsDTO;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace STCA_WebApp.Extensions
{
    public static class ZonaHorariaExtension
    {
        public enum OrderbyOptions { NOMBRE_ASC, NOMBRE_DESC };

        public static OrderbyOptions ConmutaOrderbyNombre(this OrderbyOptions option)
        {
            return (option == OrderbyOptions.NOMBRE_ASC ? OrderbyOptions.NOMBRE_DESC : OrderbyOptions.NOMBRE_ASC);
        }

        public static OrderbyOptions? Parse(Object? value)
        {
            if (value == null)
                return null;

            try
            {
                return (OrderbyOptions)(value);
            }
            catch
            {
                return null;
            }
        }

        public static IQueryable<ZonaHorariaListDTO> MapZonaHorariaToDto(this IQueryable<ZonaHoraria> zonasHorarias)
        {
            return zonasHorarias.Select(zona => new ZonaHorariaListDTO
            {
                Id = zona.Id,
                Nombre = zona.Nombre
            });
        }

        public static IQueryable<ZonaHorariaListDTO> Ordenar(this IQueryable<ZonaHorariaListDTO> zonasHorarias, OrderbyOptions? orderbyOptions)
        {
            switch (orderbyOptions ?? OrderbyOptions.NOMBRE_ASC)
            {
                case OrderbyOptions.NOMBRE_ASC:
                    return zonasHorarias.OrderBy(x => x.Nombre);

                case OrderbyOptions.NOMBRE_DESC:
                    return zonasHorarias.OrderByDescending(x => x.Nombre);

                default:
                    return zonasHorarias.OrderBy(x => x.Nombre);

            }
        }

        public static IQueryable<ZonaHorariaListDTO> Pagina(this IQueryable<ZonaHorariaListDTO> zonasHorarias, out int cantPaginas,
                                                                ref int numPaginaBaseCero,
                                                                int longPagina = 10)
        {
            /* Workflow:
                1- Calcular y retornar la cantidad_paginas tomando en cuenta la cantidad de items y el tamaño_pagina
                2- si numero_pagina > cantidad_paginas-1 => numero_pagina = cantidad_paginas-1
                3- hacer seek y tomar la cantidad de items según tamaño_pagina
                4- si cantidad_rec encontrados es cero => decrementar numero_pagina => ir a 3
                   el lazo de ir a 3 termina cdo numero_pagina = 0
                5- Retornar cantidad_paginas y numero_pagina
             */

            cantPaginas = 0;
            uint cantItems = (uint)zonasHorarias.Count();
            if (cantItems <= 0)
                return zonasHorarias;


            numPaginaBaseCero = int.Max(numPaginaBaseCero, 0);

            longPagina = int.Max(longPagina, 10);

            cantPaginas = (int)(cantItems / longPagina);
            if (cantItems % longPagina > 0)
                cantPaginas++;

            //2 - si numero_pagina > cantidad_paginas-1 => numero_pagina = cantidad_paginas-1
            numPaginaBaseCero = int.Min(numPaginaBaseCero, cantPaginas - 1);

            int cantidadRetorno;
            IQueryable<ZonaHorariaListDTO> zonasHorariasRetorno;
            do
            {
                //3 - hacer seek y tomar la cantidad de items según tamaño_pagina
                int iSkip = numPaginaBaseCero * longPagina;
                zonasHorarias.Skip(iSkip);
                zonasHorariasRetorno = zonasHorarias.Take(longPagina);

                //4 - si cantidad_rec encontrados es cero => decrementar numero_pagina => ir a 3
                //       el lazo de ir a 3 termina cdo numero_pagina = 0
                cantidadRetorno = zonasHorariasRetorno.Count();
                if (cantidadRetorno == 0)
                    numPaginaBaseCero--;

            } while (cantidadRetorno == 0 && numPaginaBaseCero > 0);

            return zonasHorariasRetorno;

        }

    }

}
