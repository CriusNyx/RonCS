namespace RonCS.AST;

/// <summary>
/// AST element representing a ron none.
/// By default this represents a null in C#.
/// </summary>
[Serializable]
public class RonNone : RonElement
{
  /// <inheritdoc/>
  public override string RonPrint(RonPrintOptions options)
  {
    return "None";
  }
}
