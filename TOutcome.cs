using Microsoft.AspNetCore.Routing.Template;

namespace Util.Infrastructure;

public class TOutcome
{
    public static TOutcome<TValue> Success<TValue>(TValue val)
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

    public static TOutcome<TValue> Error<TValue>(string msg)
    {
        return new TOutcome<TValue>(msg);
    }

    public static TOutcome<TValue> Error<TValue>(Int32 code)
    {
		return new TOutcome<TValue>(code);
	}
}

public class TOutcome<TValue>
{
	public TValue? Value { get; init; }

	public bool HasValue { get; init; }

	public string Message { get; init; }

    public Int32? Code { get; init; }
    

    public TOutcome()
    {
        HasValue = false;
        Message = "unknown error.";
    }

    public TOutcome(string msg)
    {   
        HasValue = false;
        Message = msg;
    }

    public TOutcome(Int32 code)
    {
        HasValue = false;
		Code = code;
		Message = "";
    }

    public TOutcome(TValue value)
    {   
        Value = value;
		HasValue = true;
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
        if (HasValue && other.HasValue)
        {
            return Equals(Value, other.Value);
        }

        return HasValue == other.HasValue;
    }  
}