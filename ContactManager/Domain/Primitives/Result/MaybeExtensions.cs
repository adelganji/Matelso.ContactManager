namespace Domain.Primitives.Result;

public static class MaybeExtensions
{
    public static Result<T> ToResult<T>(this Maybe<T> maybe)
    {
        return  maybe.ErrorCode!=0?Result.Failed<T>(maybe.ErrorCode): Result.Success(maybe.Value);
    }

    public static T GetValueOrDefault<T>(this Maybe<T> maybe, T defaultValue = default(T))
    {
        return maybe.GetValueOrDefault(x => x, defaultValue);
    }

    public static K GetValueOrDefault<T, K>(this Maybe<T> maybe, Func<T, K> selector, K defaultValue = default(K))
    {
        return maybe.HasValue ? selector(maybe.Value) : defaultValue;
    }

    public static Maybe<T> Where<T>(this Maybe<T> maybe, Func<T, bool> predicate)
    {
        if (maybe.HasNoValue)
            return default(T);

        return predicate(maybe.Value) ? maybe : default(T);
    }

    public static Maybe<K> Select<T, K>(this Maybe<T> maybe, Func<T, K> selector)
    {
        return maybe.HasNoValue ? default(K) : selector(maybe.Value);
    }

    public static Maybe<K> Select<T, K>(this Maybe<T> maybe, Func<T, Maybe<K>> selector)
    {
        return maybe.HasNoValue ? default(K) : selector(maybe.Value);
    }

    public static void Execute<T>(this Maybe<T> maybe, Action<T> action)
    {
        if (maybe.HasNoValue)
            return;

        action(maybe.Value);
    }
}
