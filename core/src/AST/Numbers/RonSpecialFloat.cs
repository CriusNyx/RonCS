namespace RonCS.AST;

/// <summary>
/// Type of a special float.
/// </summary>
public enum SpecialFloatType
{
  /// <summary>
  /// Infinity
  /// </summary>
  inf,

  /// <summary>
  /// Not a number
  /// </summary>
  NaN,
}

/// <summary>
/// AST element for a special floating point number.
/// </summary>
/// <remarks>
/// Sign is not needed since this AST element only represents the value portion of the number.
/// </remarks>
public class RonSpecialFloat : RonFloatNumber
{
  /// <summary>
  /// The special type of the float.
  /// </summary>
  public SpecialFloatType? specialType;

  /// <summary>
  /// Create a new special float.
  /// </summary>
  /// <param name="specialType">The type of the special float.</param>
  public RonSpecialFloat(SpecialFloatType? specialType)
  {
    this.specialType = specialType;
  }

  /// <inheritdoc/>
  public override string Serialize()
  {
    return specialType.NotNull(nameof(specialType)).ToString().NotNull(nameof(specialType));
  }

  /// <inheritdoc/>
  public override string ValueString()
  {
    return specialType?.ToString() ?? "";
  }
}
