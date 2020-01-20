using Chinook.Domain.Converters;
using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.ApiModels;

namespace Chinook.Domain.Extensions
{
    public static class ConvertExtensions
    {
        public static IAsyncEnumerable<TTarget> ConvertAll<TSource, TTarget>(
            this IAsyncEnumerable<IConvertModel<TSource, TTarget>> values) 
            => values.Select(value => value.Convert());
    }
}