using STCA_DataLib.Model;

namespace STCA_WebApp.Services
{
    public interface ISTCA_DbService
    {
        Task<ZonaHoraria[]> GetZonasHorariasAsync();

        Task<bool> CreateZonaHorariaAsync(ZonaHoraria item);

    }
}
