using STCA_DataLib.Model;
using STCA_WebApp.DTO;
using STCA_WebApp.Extensions;
using STCA_WebApp.ModelsDTO;
using static STCA_WebApp.Extensions.ZonaHorariaExtension;

namespace STCA_WebApp.Services
{
    public interface ISTCA_DbService
    {
        Task<ZonaHorariaListDTO> GetZonasHorariasAsync(ZonaHorariaPagingOptions zonaHorariaQueryOptions);

        Task<bool> CreateZonaHorariaAsync(ZonaHoraria item);

        Task<bool> DeleteZonaHorariaAsync(int id);

        Task<RangoTiempo[]> GetRangosTiempoAsync();

    }
}
