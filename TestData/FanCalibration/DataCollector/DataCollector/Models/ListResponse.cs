namespace DataCollector.Models;

/// <summary>
///     A shared wrapper for responses that consist of multiple items
/// </summary>
/// <param name="Items"></param>
/// <typeparam name="T"></typeparam>
public record ListResponse<T>(IEnumerable<T> Items)
{
    internal static ListResponse<T> FromSequence<TSource>(IEnumerable<TSource>? source, Func<TSource, T> projection)
    {
        var items = source?.Select(projection) ?? [];
        return new ListResponse<T>(items);
    }

    internal static ListResponse<T> FromSequence(IEnumerable<T>? source)
    {
        return new ListResponse<T>(source ?? []);
    }
}