using static STCA_WebApp.Extensions.ZonaHorariaExtension;

namespace STCA_WebApp.Extensions
{
    public class ZonaHorariaQueryOptions
    {
        public PagingOptions PagingOptions { get; set; } = new PagingOptions();

        public OrderbyOptions OrderbyOptions { get; set; } = OrderbyOptions.NOMBRE_ASC;

        public static ZonaHorariaQueryOptions Parse(Object? value)
        {
            try
            {
                return (ZonaHorariaQueryOptions)(value ?? new());
            }
            catch
            {
                return new();
            }

        }

    }

}
