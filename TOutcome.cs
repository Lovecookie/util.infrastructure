using Microsoft.AspNetCore.Routing.Template;

namespace Util.Infrastructure;

public class TOutcome
{
    public static TOutcome<TValue> Ok<TValue>(TValue val)
    {
        return new TOutcome<TValue>(val);
    }
    public static TOutcome<TValue> Unknown<TValue>()
    {
        return new TOutcome<TValue>("Unknown failed.");
    }

    public static TOutcome<TValue> DbFailed<TValue>()
    {
        return new TOutcome<TValue>("DB failed.");
    }

    public static TOutcome<TValue> Empty<TValue>()
    {
        return new TOutcome<TValue>("Not found entities.");
    }

    public static TOutcome<TValue> Err<TValue>(string msg)
    {
        return new TOutcome<TValue>(msg);
    }

    public static TOutcome<TValue> Err<TValue>(Int32 code)
    {
		return new TOutcome<TValue>(code);
	}
}

public class TOutcome<TValue>
{
	public TValue? Value { get; init; }

	public bool Success { get; init; }

	public string Message { get; init; }

    public Int32? Code { get; init; }
    

    public TOutcome()
    {
        Success = false;
        Message = "unknown error.";
    }

    public TOutcome(string msg)
    {   
        Success = false;
        Message = msg;
    }

    public TOutcome(Int32 code)
    {
        Success = false;
		Code = code;
		Message = "";
    }

    public TOutcome(TValue value)
    {   
        Value = value;
		Success = true;
		Message = string.Empty;
    }

    public override bool Equals(object? obj)
    {
        if(obj == null)
        {
            return false;
        }

        if (obj is TOutcome<TValue>)
        {
            return Equals((TOutcome<TValue>)obj);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public bool Equals(TOutcome<TValue> other)
    {
        if (Success && other.Success)
        {
            return Equals(Value, other.Value);
        }

        return Success == other.Success;
    }  
}