using STCA_DataLib.Model;
using STCA_WebApp.ModelsDTO;

namespace STCA_WebApp.Extensions
{
    public static class ZonaHorariaExtension
    {
        public enum ZonaHorariaOrderbyOptions { NOMBRE_ASC, NOMBRE_DESC };

        public static ZonaHorariaOrderbyOptions ConmutaOrderbyNombre(this ZonaHorariaOrderbyOptions option)
        {
            return (option == ZonaHorariaOrderbyOptions.NOMBRE_ASC ? ZonaHorariaOrderbyOptions.NOMBRE_DESC : ZonaHorariaOrderbyOptions.NOMBRE_ASC);
        }

        public static ZonaHorariaOrderbyOptions? Parse(Object? value)
        {
            if (value == null)
                return null;

            try
            {
                return (ZonaHorariaOrderbyOptions)(value);
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

        public static IQueryable<ZonaHorariaListDTO> OrderBy(this IQueryable<ZonaHorariaListDTO> zonasHorarias, ZonaHorariaOrderbyOptions? orderbyOptions)
        {
            switch (orderbyOptions ?? ZonaHorariaOrderbyOptions.NOMBRE_ASC)
            {
                case ZonaHorariaOrderbyOptions.NOMBRE_ASC:
                    return zonasHorarias.OrderBy(x => x.Nombre);

                case ZonaHorariaOrderbyOptions.NOMBRE_DESC:
                    return zonasHorarias.OrderByDescending(x => x.Nombre);

                default:
                    return zonasHorarias.OrderBy(x => x.Nombre);

            }
        }

    }
}
