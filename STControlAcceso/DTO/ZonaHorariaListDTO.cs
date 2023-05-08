using STCA_WebApp.Extensions;
using STCA_WebApp.ModelsDTO;

namespace STCA_WebApp.DTO
{
    public class ZonaHorariaListDTO
    {
        public ZonaHorariaDTO[] Items { get; set; } = new ZonaHorariaDTO[0];

        public int PageNumberZeroBase = 0;
        public int PageZise = PagingOptions.DEFAULT_PAGE_SIZE;
        public int PagesCount = 0;

    }
}
