namespace Domain.Primitives.Result;

public static class ResultExtensions
{

    #region SuccessMethod
    public static Result OnSuccess(this Result result, Action action)
    {
        if (result.Succeed) action();
        return result;
    }
    public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
    {
        if (result.Succeed) action(result.Value);
        return result;
    }
    public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
    {
        return !result.Succeed ? Result.Failed<T>(result.Message) : Result.Success(func());
    }
    public static Result OnSuccess<T>(this Result<T> result, Func<Result> func)
    {
        return !result.Succeed ? result : func();
    }
    public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
    {
        return !result.Succeed ? Result.Failed<T>(result.Message) : func();
    }

    public static Result OnSuccess<T>(this Result<T> result, Func<T, Result<T>> func)
    {
        return !result.Succeed ? Result.Failed(result.Message) : func(result.Value);
    }

    public static Result<TK> OnSuccess<T, TK>(this Result<T> result, Func<T, TK> func)
    {
        return !result.Succeed ?Result.Failed<TK>(result.Message) :Result.Success(func(result.Value)) ;
    }        
    public static Result<TK> OnSuccess<T, TK>(this Result<T> result, Func<T, Result<TK>> func)
    {
        return !result.Succeed ? Result.Failed<TK>(result.Message) : func(result.Value);
    }
    public static Result<TK> OnSuccess<T, TK>(this Result<T> result, Func<Result<TK>> func)
    {
        return !result.Succeed ? Result.Failed<TK>(result.Message) : func();
    }
    #endregion

    #region FailedMethod
    public static Result OnFailure(this Result result, Action action)
    {
        if (!result.Succeed) action();
        return result;
    }
    public static Result<T> OnFailure<T>(this Result<T> result, Action action)
    {
        if (!result.Succeed) action();
        return result;
    }
    public static Result OnFailure(this Result result, Action<string> action)
    {
        if (!result.Succeed) action(result.Message);
        return result;
    }
    public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
    {
        if (!result.Succeed) action(result.Message);
        return result;
    }
    #endregion

    #region PredicateMethod
    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string message)
    {
        if (!result.Succeed)
            return Result.Failed<T>(result.Message);

        return !predicate(result.Value) ? Result.Failed<T>(message) : Result.Success(result.Value);
    }
    public static Result Ensure(this Result result, Func<bool> predicate, string message)
    {
        if (!result.Succeed)
            return Result.Failed(result.Message);

        return !predicate() ? Result.Failed(message) : Result.Success();
    }
    #endregion

    #region MapMethod

    public static Result<TK> Map<T, TK>(this Result<T> result, Func<T, TK> func)
    {
       return !result.Succeed ? Result.Failed<TK>(result.Message) : Result.Success(func(result.Value));
    }
    public static Result<T> Map<T>(this Result result, Func<T> func)
    {
        return !result.Succeed ? Result.Failed<T>(result.Message) : Result.Success(func());
    }
    public static Result Map<T>(this Result<T> result)
    {
        return !result.Succeed ? Result.Failed(result.Message) : Result.Success();
    }
    #endregion

    #region BothMethod

    public static T OnBoth<T>(this Result result, Func<Result,T> func)
    {
        return func(result);
    }
    public static T OnBoth<T>(this Result result, Func<T> func)
    {
        return func();
    }

    public static TK OnBoth<T, TK>(this Result<T> result, Func<T, TK> func)
    {
        return func(result.Value);
    }
    #endregion

}
