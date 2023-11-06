using Common.ViewModels.Pagination;

namespace Common.ViewModels.Extensions
{
    public static class EnumerableExtensions
    {
        public static List<(int Index, T Item)> WithIndex<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select((item, index) => (index, item)).ToList();
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var metadata = new Metadata
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(source.Count() / (double)pageSize),
                TotalCount = source.Count()
            };
            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PagedList<T>(items, metadata);
        }
    }
}
