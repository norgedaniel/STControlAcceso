namespace STCA_WebApp.Extensions
{
    public class PagingOptions
    {
        public const int DEFAULT_PAGE_SIZE = 10;

        public int PageZise { get; set; } = DEFAULT_PAGE_SIZE;

        public int PageNumberZeroBase { get; set; } = 0;

        public int PagesCount { get; set; } = 0;

    }
}
