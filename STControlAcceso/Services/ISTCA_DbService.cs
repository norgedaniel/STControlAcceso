using STCA_DataLib.Model;
using STCA_WebApp.ModelsDTO;
using static STCA_WebApp.Extensions.ZonaHorariaExtension;

namespace STCA_WebApp.Services
{
    public interface ISTCA_DbService
    {
        Task<ZonaHorariaListDTO[]> GetZonasHorariasAsync(ZonaHorariaOrderbyOptions? orderbyOptions = ZonaHorariaOrderbyOptions.NOMBRE_ASC);

        Task<bool> CreateZonaHorariaAsync(ZonaHoraria item);

        Task<bool> DeleteZonaHorariaAsync(int id);

    }
}
