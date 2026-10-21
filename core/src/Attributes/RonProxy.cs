namespace RonCS;

/// <summary>
/// Indicates that a class should be converted to or from the proxy type for serialization.
/// </summary>
/// <param name="proxy">The type that should be used as an intermediate type when serializing and deserializing the element.</param>
/// <remarks>
/// The RonConvert algorithm is used by default to convert to and from the proxy type.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
public class RonProxyAttribute(Type proxy) : Attribute
{
  /// <summary>
  /// The type that provides the proxy for this type.
  /// </summary>
  public readonly Type Proxy = proxy;
}
