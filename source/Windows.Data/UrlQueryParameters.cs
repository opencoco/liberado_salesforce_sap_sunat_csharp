namespace ACME.Data
{
    public class UrlQueryParameters
    {
        const int maxPageSize = 50;
        private int _pageSize = 20;
        public string? TextSearch { get; set; } = "";
        public string? Filter { get; set; } = "";
        public int? ParentId { get; set; } = (int?)null;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
