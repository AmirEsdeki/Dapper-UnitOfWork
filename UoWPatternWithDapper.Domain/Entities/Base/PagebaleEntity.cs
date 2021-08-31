using System.Collections.Generic;

namespace UoWPatternWithDapper.Domain.Entities.Base
{
    public class PagebaleEntity<T> where T : BaseEntity
    {
        public PagebaleEntity(long page, long limit, string sortColumn)
        {
            Page = page;
            Limit = limit;
            SortColumn = sortColumn;
        }
        public IEnumerable<T> Items { get; set; }
        public long Count { get; set; }
        public string SortColumn { get; set; }
        public long Page { get; set; }
        public long Limit { get; set; }
    }
}
