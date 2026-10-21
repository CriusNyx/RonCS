namespace RonCS.AST;

/// <summary>
/// Common interface for all AST nodes that represent a number.
/// </summary>
public interface INumberValue
{
  /// <summary>
  /// String representing the value of the number.
  /// Used for C# type conversion.
  /// </summary>
  /// <returns>The string value of the number.</returns>
  public string ValueString();

  /// <summary>
  /// Convert the value to a number.
  /// </summary>
  /// <param name="hint">A type hint for what type the number will be assigned to.</param>
  /// <returns>The value of the number as a C# value.</returns>
  public object EvaluateNumber(Type? hint);

  internal Type? CSType();
}
