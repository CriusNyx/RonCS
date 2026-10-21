namespace RonCS.AST;

/// <summary>
/// Builder functions for RON AST's.
/// </summary>
public static class RonBuilder
{
  /// <summary>
  /// Create a some value.
  /// </summary>
  /// <param name="value">The value fo the some.</param>
  /// <returns>A RonSome AST Element.</returns>
  public static RonSome Some(RonElement value)
  {
    return new(value);
  }


  /// <summary>
  /// A RonNone ast Element.
  /// </summary>
  public static RonNone None => new();

  /// <summary>
  /// Create a tuple with the specified child values.
  /// </summary>
  /// <param name="values">The elements of the tuple.</param>
  /// <returns>A RonTuple AST element.</returns>
  public static RonTuple Tuple(params RonElement[] values)
  {
    return new RonTuple(values);
  }

  /// <summary>
  /// Create an identifier with the specified name.
  /// </summary>
  /// <param name="name">The name of the identifier.</param>
  /// <returns>A RonIdentifier AST element.</returns>
  public static RonIdentifier Ident(string name)
  {
    return new RonIdentifier(name);
  }

  /// <summary>
  /// Create a raw identifier with the specified name.
  /// </summary>
  /// <param name="name">The name of the identifier.</param>
  /// <returns>A RonRawIdentifier AST element.</returns>
  public static RonRawIdentifier RawIdent(string name)
  {
    return new RonRawIdentifier(name);
  }

  /// <summary>
  /// Create a name value with a specified name and value.
  /// </summary>
  /// <param name="name">The name of the pair.</param>
  /// <param name="value">The value of the pair.</param>
  /// <returns>A RonNamedValue AST element.</returns>
  public static RonNamedValue NamedValue(RonElement name, RonElement value)
  {
    return new RonNamedValue(name, value);
  }

  /// <summary>
  /// Create a named value with the specified name and value.
  /// </summary>
  /// <param name="name">The name of the pair.</param>
  /// <param name="value">The value of the pair.</param>
  /// <returns>A RonNamedValue AST element.</returns>
  public static RonNamedValue NamedValue(string name, RonElement value)
  {
    return new RonNamedValue(Ident(name), value);
  }

  /// <summary>
  /// Create a struct with a name and no body.
  /// </summary>
  /// <param name="name">The name of the struct.</param>
  /// <returns>RonUnitStruct AST element.</returns>
  public static RonUnitStruct UnitStruct(string name)
  {
    return new RonUnitStruct(Ident(name));
  }

  /// <summary>
  /// Create a ron boolean with the specified value.
  /// </summary>
  /// <param name="value">The value of the boolean.</param>
  /// <returns>A RonBool AST element.</returns>
  public static RonBool Bool(bool value)
  {
    return new RonBool(value);
  }

  /// <summary>
  /// Create a tuple struct with the specified name and elements.
  /// </summary>
  /// <param name="name">The name of the struct.</param>
  /// <param name="body">The values of the struct.</param>
  /// <returns>A RonTupleStruct AST element.</returns>
  public static RonTupleStruct TupleStruct(string name, params RonElement[] body)
  {
    return new RonTupleStruct(Ident(name), Tuple(body));
  }

  /// <summary>
  /// Create a named value struct with the specified name, and body.
  /// </summary>
  /// <param name="name">The name of the struct.</param>
  /// <param name="body">The values of the struct body. These should be RonNamedValue elements.</param>
  /// <returns>A RonNameValueStruct AST element.</returns>
  public static RonNamedValueStruct NamedValueStruct(string name, params RonElement[] body)
  {
    return new RonNamedValueStruct(Ident(name), body);
  }

  /// <summary>
  /// Create a map item with the specified key and value.
  /// </summary>
  /// <param name="key">The key of the map item.</param>
  /// <param name="value">The value of the map item.s</param>
  /// <returns>A RonMapItem AST element.</returns>
  public static RonMapItem MapItem(RonElement key, RonElement value)
  {
    return new RonMapItem(key, value);
  }

  /// <summary>
  /// Create a map with the specified values.
  /// </summary>
  /// <param name="values">The values of the RonMap. These should be RonMapItem's.</param>
  /// <returns>A RonMap AST element.</returns>
  public static RonMap Map(params RonElement[] values)
  {
    return new RonMap(values);
  }

  /// <summary>
  /// Create a list with the specified values.
  /// </summary>
  /// <param name="values">The values of the list.</param>
  /// <returns>A RonList AST element.</returns>
  public static RonList List(params RonElement[] values)
  {
    return new RonList(values);
  }

  /// <summary>
  /// Create a range with the specified values.
  /// </summary>
  /// <param name="left">The left hand side of the operator.</param>
  /// <param name="op">The operator.</param>
  /// <param name="right">The right hand side of the operator.</param>
  /// <returns>A RonRange AST element.</returns>
  public static RonRange Range(RonElement? left, RonRangeOperator op, RonElement? right)
  {
    return new RonRange(left, op, right);
  }

  /// <summary>
  /// Create a document with the specified element.
  /// </summary>
  /// <param name="value">The value of the document.</param>
  /// <returns>A RonDocument AST element.</returns>
  public static RonDocument Doc(RonElement value)
  {
    return new RonDocument(value);
  }

}
