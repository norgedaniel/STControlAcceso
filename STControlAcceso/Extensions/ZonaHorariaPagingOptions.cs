using Microsoft.IdentityModel.Tokens;
using static STCA_WebApp.Extensions.ZonaHorariaExtension;
using Newtonsoft.Json;

namespace STCA_WebApp.Extensions
{
    public class ZonaHorariaPagingOptions
    {
        public enum OrderbyOptionValues { NOMBRE_ASC, NOMBRE_DESC };

        public PagingOptions PagingOptions { get; set; } = new();

        public OrderbyOptionValues OrderbyOption { get; set; } = OrderbyOptionValues.NOMBRE_ASC;


        public void ConmutaOrderbyNombre()
        {
            switch (OrderbyOption)
            {
                case OrderbyOptionValues.NOMBRE_ASC:
                    OrderbyOption = OrderbyOptionValues.NOMBRE_DESC;
                    break;
                case OrderbyOptionValues.NOMBRE_DESC:
                    OrderbyOption = OrderbyOptionValues.NOMBRE_ASC;
                    break;
                default:
                    break;
            }
        }
 
        public static ZonaHorariaPagingOptions Deserialize(Object? value)
        {
            if (value == null)
                return new();


            string? tempOption = string.Empty;

            if (value != null && value.GetType() == Type.GetType("System.String"))
                tempOption = value.ToString();

            try
            {
                return JsonConvert.DeserializeObject<ZonaHorariaPagingOptions>(tempOption ?? "") ?? new();
            }
            catch
            {
                return new();
            }

        }

    }

}
