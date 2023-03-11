using System.Diagnostics;

namespace Domain.Primitives.Result;

public class Result
{
    private static readonly Result SucceesResult=new Result(true,null);
    public bool Succeed { get; set; }
    public string Message { get; set; }
    //public int Count{ get; set; }
    public long ErrorCode { get; set; }
    protected Result(bool succeeded, string message,int errorCode=0)
    {
        Succeed = succeeded;
        Message = message;
        ErrorCode = errorCode;
    }

    [DebuggerStepThrough]
    public static Result Success()
    {
        return SucceesResult;
    }
    [DebuggerStepThrough]
    public static Result<T> Success<T>(T value)
    {
        return new Result<T>(value,true,string.Empty);
    }
    [DebuggerStepThrough]
    public static Result<T> Success<T>(T value,string message)
    {
        return new Result<T>(value, true, message);
    }
    [DebuggerStepThrough]
    public static Result<T> Success<T>(T value, int count)
    {
        return new Result<T>(value, true, string.Empty,count);
    }
    [DebuggerStepThrough]
    public static Result Failed(string message)
    {
        return new Result(false,message);
    }
    [DebuggerStepThrough]
    public static Result Failed(int errorCode)
    {
        return new Result(false,null,errorCode);
    }
    public static Result FailedWithError(int errorCode,string message)
    {
        return new Result(false, message,errorCode);
    }
    [DebuggerStepThrough]
    public static Result<T> Failed<T>(string message)
    {
        return new Result<T>(default(T),false,message);
    }
    [DebuggerStepThrough]
    public static Result<T> Failed<T>(int errorCode)
    {
        return new Result<T>(default(T), false,string.Empty,errorCode:errorCode);
    }
    [DebuggerStepThrough]
    public static Result<T> Failed<T>(T value,string message)
    {
        return new Result<T>(value, false, message);
    }
    [DebuggerStepThrough]
    public static Result<T> Failed<T>(T value, int errorCode)
    {
        return new Result<T>(value, false, string.Empty,errorCode);
    }
    [DebuggerStepThrough]
    public static Result Combain(string seperator, params Result[] results)
    {
        var failedResult = results.Where(x => !x.Succeed).ToList();
        if (!failedResult.Any())
            return Success();
        var error = string.Join(seperator, failedResult.Select(x => x.Message).ToArray());
        return Failed(error);
    }
    [DebuggerStepThrough]
    public static Result Combain(params Result[] results)
    {
        return Combain(", ", results);
    }
    [DebuggerStepThrough]
    public static Result Combain<T>(params Result<T>[] results)
    {
        var unTyped = results.Select(x => (Result) x).ToArray();
        return Combain(", ", unTyped);
    }
    [DebuggerStepThrough]
    public static Result Combine<T>(string seperator, params Result<T>[] results)
    {
        var unTyped = results.Select(x => (Result)x).ToList();
        return Combain(seperator, unTyped.ToArray());
    }
    [DebuggerStepThrough]
    public static Result FirstFailureOrSuccess(params Result[] results)
    {
        foreach (Result result in results)
        {
            if (!result.Succeed)
                return Failed(result.Message);
        }

        return Success();
    }


    public override string ToString()
    {
        return Succeed
            ? "Succeeded"
            : $"Failed : {Message}";
    }

    
}


public class Result<T> : Result
{
    protected internal Result(T value, bool succeeded, string message,long errorCode=0) : base(succeeded, message)
    {
        Value = value;
        ErrorCode = errorCode;
    }
    
    public T Value
    {
        [DebuggerStepThrough]
        get;
    }
    public bool HasValue
    {
        [DebuggerStepThrough]
        get { return Value != null ? true : false; }
    }
}
