namespace Insurance_agency.Models.ModelView
{
    public class PaginationSearch<T>
    {
        public int TotalItem {  get; set; }
        public int PageNumber {  get; set; }
        public int PageSize { get; set; }
        public string? SearchKeyword {  get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalPage => (int)Math.Ceiling((double)TotalItem / PageSize); 
    }
}
