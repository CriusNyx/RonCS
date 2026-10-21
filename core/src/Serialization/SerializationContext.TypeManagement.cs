using RonCS.Converters;

namespace RonCS;

public partial class SerializationContext
{
  /// <summary>
  /// Register a new known type
  /// </summary>
  /// <param name="type">The type to register.</param>
  public void RegisterType(Type type)
  {
    RonTypes.Add(type);
  }

  /// <summary>
  /// Register a custom type converter for the type.
  /// </summary>
  /// <param name="type">The type to use this converter for.</param>
  /// <param name="converter">The converter to use.</param>
  public void RegisterTypeConverter(Type type, TypeSerializerConverter converter)
  {
    converterCache[type] = converter;
  }

  /// <summary>
  /// Register type to be serialized as a list.
  /// </summary>
  /// <param name="type">The type to serialize/deserialize as a list.</param>
  public void RegisterListType(Type type)
  {
    converterCache[type] = new ListConverter();
  }

  /// <summary>
  /// Register type to be serialized as a map.
  /// </summary>
  /// <param name="type">The type to serialize/deserialize as a map.</param>
  public void RegisterMapType(Type type)
  {
    converterCache[type] = new DictionaryConverter();
  }

  /// <summary>
  /// Register converter to serialize a type as a tuple.
  /// </summary>
  /// <typeparam name="Target">The target type to serialize as a tuple.</typeparam>
  /// <param name="converter">The converter to use to serialize the element as a tuple.</param>
  public void RegisterTupleConverter<Target>(Func<Target, object[]> converter)
  {
    RegisterTypeConverter(
      typeof(Target),
      new TupleFunctionConverter(typeof(Target).Name, (o) => converter((Target)o))
    );
  }

  /// <summary>
  /// Register a custom proxy for the source type.
  /// </summary>
  /// <param name="sourceType">The source type for the proxy.</param>
  /// <param name="proxyType">The proxy type to use for serialization/deserialization.</param>
  public void RegisterProxyType(Type sourceType, Type proxyType)
  {
    RegisterTypeConverter(
      sourceType,
      new ProxyConverter(
        proxyType,
        TypeSerializerConverter.CreateTypeConverterForTypeWithFields(proxyType, sourceType.Name)
      )
    );

    proxyTypes[sourceType] = proxyType;
  }
}
