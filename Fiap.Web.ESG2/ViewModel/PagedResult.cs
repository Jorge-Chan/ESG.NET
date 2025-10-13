using System.Collections.Generic;

namespace Fiap.Web.ESG2.ViewModels
{
    public class PagedResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)System.Math.Ceiling((double)TotalItems / PageSize);
        public IEnumerable<T> Items { get; set; } = System.Linq.Enumerable.Empty<T>();
    }
}
