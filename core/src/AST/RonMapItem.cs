using RonCS.Exceptions;

namespace RonCS.AST;

/// <summary>
/// AST element for an element in a ron map.
/// In most cases key should be a ron string.
/// </summary>
/// <param name="key">The key for the map item.</param>
/// <param name="value">The value of the map item.</param>
[Serializable]
public class RonMapItem(RonElement? key, RonElement? value) : RonElement
{
  /// <summary>
  /// The key for the map item.
  /// </summary>
  public RonElement? Key = key;

  /// <summary>
  /// The value of the map item.
  /// </summary>
  public RonElement? Value = value;

  /// <inheritdoc/>
  public override string RonPrint(RonPrintOptions options)
  {
    switch (options.mode)
    {
      case RonPrintMode.Compact:
        return Key?.RonPrint(options) + ":" + Value?.RonPrint(options);
      case RonPrintMode.Pretty:
        return Key?.RonPrint(options) + ": " + Value?.RonPrint(options);
      default:
        throw options.mode.AsEnumException();
    }
  }
}
