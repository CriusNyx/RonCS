namespace RonCS.Exceptions;

/// <summary>
/// Exception for when an enum case was unknown.
/// </summary>
/// <typeparam name="T">The type of the enum that was not known.</typeparam>
/// <param name="value"></param>
public class UnknownEnumException<T>(T? value) : Exception
{
  /// <inheritdoc/>
  public override string Message => $"Unexpected enum of type {value}";
}

/// <summary>
/// Exception for when an enum case was unknown.
/// </summary>
public class UnknownEnumException
{
  internal static UnknownEnumException<T> Create<T>(T? value)
  {
    return new UnknownEnumException<T>(value);
  }
}

internal static class ExceptionExtensions
{
  /// <summary>
  /// Creates an enum exception for the value.
  /// </summary>
  /// <typeparam name="T">The type of the enum that was not known.</typeparam>
  /// <param name="value">The value that was encountered.</param>
  /// <returns>A new UnknownEnumException.</returns>
  public static UnknownEnumException<T> AsEnumException<T>(this T value)
    where T : Enum
  {
    return UnknownEnumException.Create(value);
  }
}
