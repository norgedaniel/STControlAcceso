using STCA_WebApp.Extensions;
using STCA_WebApp.ModelsDTO;

namespace STCA_WebApp.DTO
{
    public class ZonaHorariaListDTO
    {
        public ZonaHorariaDTO[] Items { get; set; } = new ZonaHorariaDTO[0];

        public PagingOptions PagingOptions { get; set; } = new();

    }
}
