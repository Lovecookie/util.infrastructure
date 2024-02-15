namespace Util.Infrastructure;


public class TOptional
{
    public static TOptional<TValue> To<TValue>(TValue val)
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
}

public class TOptional<TValue>
{
    /// <summary>
    /// member
    /// </summary>
    public TValue? Value { get; private set; }

    public string ErrorMessage { get; private set; }

    public TOptional()
    {
        ErrorMessage = "unknown error.";
    }

    public TOptional(string msg)
    {
        ErrorMessage = msg;
    }

    public TOptional(TValue value)
    {
        Value = value;
        ErrorMessage = string.Empty;
    }

    public override bool Equals(object? obj)
    {
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

    public bool HasValue => Value != null;

    public bool Equals(TOptional<TValue> other)
    {
        if (HasValue && other.HasValue)
        {
            return Equals(Value, other.Value);
        }

        return HasValue == other.HasValue;
    }

    /// <summary>
    /// static
    /// </summary>
    public static TOptional<TValue> Unknown => new TOptional<TValue>();
}