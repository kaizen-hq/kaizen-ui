using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Kaizen.UI.Components
{
    public record OrderBy<TSource, TKey>(string Name, bool Descending, Expression<Func<TSource, TKey>> SortFunc);

    public record OrderByState<TSource, TKey>
    {
        public List<OrderBy<TSource, TKey>> OrderBy { get; set; } = new();
    
        [NotNull]
        public Action<OrderBy<TSource, TKey>, bool>? UpdateOrderBy { get; set; }
    }
}
