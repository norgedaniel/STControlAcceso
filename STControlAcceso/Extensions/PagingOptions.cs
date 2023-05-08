using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Collections.Generic;

namespace STCA_WebApp.Extensions
{
    public class PagingOptions
    {
        public const int DEFAULT_PAGE_SIZE = 10;

        public static List<SelectListItem> GetPageZiseOptions(int DefaultValue = DEFAULT_PAGE_SIZE)
        {
            List<SelectListItem> lista = new()
            {
                new SelectListItem { Value = "3", Text = "3" },
                new SelectListItem { Value = "10", Text = "10" },
                new SelectListItem { Value = "50", Text = "50" }
            };

            foreach (var item in lista)
            {
                if (item.Value == DefaultValue.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }

            return lista;

        }
    }
}
