using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Kaizen.UI.Components
{
    public record OrderBy<TSource, TKey>(string Name, bool Descending, Expression<Func<TSource, TKey>> SortFunc);

    public record GridState<TSource, TKey>
    {
        public List<OrderBy<TSource, TKey>> OrderBy { get; set; } = new();
        public Dictionary<int, string> ColumnAlignments { get; set; } = new();
        public int ColumnCounter { get; set; } = 0;

        [NotNull]
        public Action<OrderBy<TSource, TKey>, bool>? UpdateOrderBy { get; set; }

        [NotNull]
        public Action<int, string>? RegisterAlignment { get; set; }
    }

    // Backwards compatibility alias
    public record OrderByState<TSource, TKey> : GridState<TSource, TKey> { }
}
