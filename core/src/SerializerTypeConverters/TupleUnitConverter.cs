using RonCS.AST;

namespace RonCS.Converters;

/// <summary>
/// Type converter for a class with no members.
/// </summary>
/// <param name="name">The name of the tuple struct.</param>
internal class TupleUnitConverter(string name) : TypeSerializerConverter
{
  public RonElement ToAST(SerializationContext context, object source)
  {
    return new RonUnitStruct(new RonIdentifier(name));
  }
}
