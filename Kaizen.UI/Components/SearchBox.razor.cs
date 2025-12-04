using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Kaizen.UI.Components
{
    public partial class SearchBox<TItem>
    {
        // Cache PropertyInfo for each type to avoid repeated reflection
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _stringPropertiesCache = new();

        /// <summary>
        /// Filters a collection of items based on a search term across string fields.
        /// If no fields are specified, automatically searches all public string properties.
        /// </summary>
        /// <typeparam name="T">The type of items to filter</typeparam>
        /// <param name="items">The source collection to filter</param>
        /// <param name="searchText">The search term to match against</param>
        /// <param name="fields">Functions that select string fields to search within. If empty, searches all public string properties.</param>
        /// <returns>A filtered list containing items where any field matches the search term (case-insensitive)</returns>
        public static List<T> Filter<T>(
            IEnumerable<T> items,
            string searchText,
            params Func<T, string>[] fields)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return [.. items];

            // If no explicit fields provided, use reflection to get all string properties
            if (fields.Length == 0)
            {
                var properties = GetStringProperties<T>();
                return [.. items.Where(item =>
                    properties.Any(prop =>
                    {
                        var value = prop.GetValue(item) as string;
                        return value?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false;
                    })
                )];
            }

            // Use explicitly provided fields
            return [.. items.Where(item =>
                fields.Any(field =>
                {
                    var value = field(item);
                    return value?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false;
                })
            )];
        }

        /// <summary>
        /// Gets all public string properties for a type, using cache for performance.
        /// </summary>
        private static PropertyInfo[] GetStringProperties<T>()
        {
            var type = typeof(T);
            return _stringPropertiesCache.GetOrAdd(type, t =>
                t.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                 .Where(p => p.PropertyType == typeof(string) && p.CanRead)
                 .ToArray()
            );
        }
    }
}
