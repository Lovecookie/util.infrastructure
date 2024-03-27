using Microsoft.AspNetCore.Routing.Template;

namespace Util.Infrastructure;

public class TOptional
{
    public static TOptional<TValue> Success<TValue>(TValue val)
    {
        return new TOptional<TValue>(val);
    }
    public static TOptional<TValue> Unknown<TValue>()
    {
        return new TOptional<TValue>("Unknown failed.");
    }

    public static TOptional<TValue> DbFailed<TValue>()
    {
        return new TOptional<TValue>("DB failed.");
    }

    public static TOptional<TValue> Empty<TValue>()
    {
        return new TOptional<TValue>("Not found entities.");
    }

    public static TOptional<TValue> Error<TValue>(string msg)
    {
        return new TOptional<TValue>(msg);
    }

    public static TOptional<TValue> Error<TValue>(Int32 code)
    {
		return new TOptional<TValue>(code);
	}
}

public class TOptional<TValue>
{
	public TValue? Value { get; init; }

	public bool HasValue { get; init; }

	public string Message { get; init; }

    public Int32? Code { get; init; }
    

    public TOptional()
    {
        HasValue = false;
        Message = "unknown error.";
    }

    public TOptional(string msg)
    {   
        HasValue = false;
        Message = msg;
    }

    public TOptional(Int32 code)
    {
        HasValue = false;
		Code = code;
		Message = "";
    }

    public TOptional(TValue value)
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

        if (obj is TOptional<TValue>)
        {
            return Equals((TOptional<TValue>)obj);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public bool Equals(TOptional<TValue> other)
    {
        if (HasValue && other.HasValue)
        {
            return Equals(Value, other.Value);
        }

        return HasValue == other.HasValue;
    }  
}