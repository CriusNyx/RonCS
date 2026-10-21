using System.Reflection;
using RonCS.AST;

namespace RonCS.Converters;

/// <summary>
/// Type converter for a method that returns a tuple.
/// </summary>
/// <param name="name">The name of the tuple type.</param>
/// <param name="method">The method to use to convert the object.</param>
public class TupleMethodConverter(string name, MethodInfo method) : TypeSerializerConverter
{
  /// <inheritdoc/>
  public RonElement ToAST(SerializationContext context, object source)
  {
    var result = method
      .Invoke(source, [])
      .AsNotNull<object[]>($"{source.GetType()}.${method.Name}");
    return new RonTupleStruct(
      new RonIdentifier(name),
      new RonTuple(result.Select(x => context.ToAST(x)).ToArray())
    );
  }
}

/// <summary>
/// Type converter for a property that returns a tuple.
/// </summary>
/// <param name="name">The name of property to convert.</param>
/// <param name="property">The PropertyInfo for the property.</param>
public class TuplePropertyConverter(string name, PropertyInfo property) : TypeSerializerConverter
{
  /// <inheritdoc/>
  public RonElement ToAST(SerializationContext context, object source)
  {
    var result = property
      .GetValue(source)
      .AsNotNull<object[]>($"{source.GetType()}.{property.Name}");
    return new RonTupleStruct(
      new RonIdentifier(name),
      new RonTuple(result.Select(x => context.ToAST(x)).ToArray())
    );
  }
}

/// <summary>
/// Type converter for a function that returns a tuple.
/// </summary>
/// <param name="name">The name of the tuple struct to convert.</param>
/// <param name="converter">The function that converts the object to a tuple.</param>
public class TupleFunctionConverter(string name, Func<object, object[]> converter)
  : TypeSerializerConverter
{
  /// <inheritdoc/>
  public RonElement ToAST(SerializationContext context, object source)
  {
    var result = converter.Invoke(source).AsNotNull<object[]>($"{source.GetType()}.${name}");
    return new RonTupleStruct(
      new RonIdentifier(name),
      new RonTuple(result.Select(x => context.ToAST(x)).ToArray())
    );
  }
}
