namespace RonCS.AST;

/// <summary>
/// AST element for the value of a literal string.
/// </summary>
/// <param name="value"></param>
public class RonStringLit(string value) : StringContent
{
  /// <summary>
  /// The value of the string literal.
  /// </summary>
  public string value = value;

  /// <inheritdoc/>
  public string EvaluateString()
  {
    return value;
  }

  /// <inheritdoc/>
  public string Serialize()
  {
    return value;
  }
}
