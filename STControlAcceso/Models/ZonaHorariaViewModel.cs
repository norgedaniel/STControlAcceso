using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using STCA_WebApp.Extensions;
using STCA_WebApp.ModelsDTO;
using static STCA_WebApp.Extensions.ZonaHorariaExtension;

namespace STCA_WebApp.Models
{
    public class ZonaHorariaViewModel
    {
        public ZonaHorariaDTO[] Items { get; set; } = new ZonaHorariaDTO[0];

        public int PageNumberZeroBase = 0;

        public int PagesCount = 0;

        public List<SelectListItem> PageZiseOptions = PagingOptions.GetPageZiseOptions();

        public string LastSortField = string.Empty;

        public bool LastSortOrderDesc = false;

    }
}
