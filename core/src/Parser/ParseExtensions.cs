using RonCS.AST;
using Superpower;
using Superpower.Parsers;

namespace RonCS.AST;

/// <summary>
/// Extensions for Superpower parsers.
/// </summary>
internal static class ParseExtensions
{
  /// <summary>
  /// Try self, and then other. If both succeed then return the value of self.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <param name="self"></param>
  /// <param name="other"></param>
  /// <returns>The new composite parser.</returns>
  internal static TextParser<T> ThenIgnore<T, U>(this TextParser<T> self, TextParser<U> other)
  {
    return self.Then((value) => other.Value(value));
  }

  /// <summary>
  /// Parse self, then other, returning the result of self.
  /// </summary>
  /// <typeparam name="TKind"></typeparam>
  /// <typeparam name="T"></typeparam>
  /// <typeparam name="U"></typeparam>
  /// <param name="self"></param>
  /// <param name="other"></param>
  /// <returns>The new composite parser.</returns>
  internal static TokenListParser<TKind, T> ThenIgnore<TKind, T, U>(
    this TokenListParser<TKind, T> self,
    TokenListParser<TKind, U> other
  )
  {
    return self.Then((value) => other.Value(value));
  }

  /// <summary>
  /// Parse any number of element separated by the separator element.
  /// </summary>
  /// <typeparam name="TKind">The token kind.</typeparam>
  /// <typeparam name="Source">The type of the source parser.</typeparam>
  /// <typeparam name="Separator">The type of the separator parser.</typeparam>
  /// <param name="source">The source parser.</param>
  /// <param name="separator">The separator parser.</param>
  /// <returns>The new composite parser.</returns>
  internal static TokenListParser<TKind, IEnumerable<Source>> SeparatedBy<TKind, Source, Separator>(
    this TokenListParser<TKind, Source> source,
    TokenListParser<TKind, Separator> separator
  )
  {
    return from start in source
      from rest in separator.IgnoreThen(source).Try().Many()
      select start.ThenConcat(rest);
  }

  /// <summary>
  /// Parse elements between before and after.
  /// </summary>
  /// <typeparam name="TKind">The token kind.</typeparam>
  /// <typeparam name="Source">The type of the source parser.</typeparam>
  /// <typeparam name="Before">The type of the before parser.</typeparam>
  /// <typeparam name="After">The type of the after parser.</typeparam>
  /// <param name="source">The source parser.</param>
  /// <param name="before">The before parser.</param>
  /// <param name="after">The after parser.</param>
  /// <returns>The new composite parser.</returns>
  internal static TokenListParser<TKind, Source> Between<TKind, Source, Before, After>(
    this TokenListParser<TKind, Source> source,
    TokenListParser<TKind, Before> before,
    TokenListParser<TKind, After> after
  )
  {
    return before.IgnoreThen(source).ThenIgnore(after);
  }

  /// <summary>
  /// Parse an element between other.
  /// </summary>
  /// <typeparam name="Source">The type of the source parser.</typeparam>
  /// <typeparam name="Other">The type of the other parser.</typeparam>
  /// <param name="self"></param>
  /// <param name="other"></param>
  /// <returns>The new composite parser.</returns>
  internal static TextParser<Source> Between<Source, Other>(this TextParser<Source> self, TextParser<Other> other)
  {
    return self.Between(other, other);
  }

  /// <summary>
  /// Parse an element between other.
  /// </summary>
  /// <typeparam name="TKind">The token kind.</typeparam>
  /// <typeparam name="Source">The type of the source parser.</typeparam>
  /// <typeparam name="Other">The type of the other parser.</typeparam>
  /// <param name="source">The source parser.</param>
  /// <param name="other">The other parser.</param>
  /// <returns>The new composite parser.</returns>
  internal static TokenListParser<TKind, Source> Between<TKind, Source, Other>(
    this TokenListParser<TKind, Source> source,
    TokenListParser<TKind, Other> other
  )
  {
    return source.Between(other, other);
  }

  /// <summary>
  /// Parse any string matching the name of the provided enum.
  /// </summary>
  /// <typeparam name="EnumType">The type of the enum to parse.</typeparam>
  /// <returns>The new composite parser.</returns>
  internal static TextParser<EnumType> EnumParser<EnumType>()
    where EnumType : struct, Enum
  {
    return Parse.OneOf(
      Enum.GetValues<EnumType>()
        .Select(value => Span.EqualTo(value.ToString()).Value(value).Try())
        .ToArray()
    );
  }

  /// <summary>
  /// Return the result if it matches any of the provided source strings.
  /// </summary>
  /// <param name="source">The strings that can be parsed.</param>
  /// <returns>The parser for the strings.</returns>
  internal static TextParser<string> StringIn(params string[] source)
  {
    return Parse.OneOf(source.Select(source => Span.EqualTo(source).Try().AsString()).ToArray());
  }

  /// <summary>
  /// Convert the character parser to a string parser.
  /// </summary>
  /// <param name="charParser">The characters that can be parsed.</param>
  /// <param name="atLeastOnce">If true then require at least 1 character to succeed.</param>
  /// <returns>The new composite parser.</returns>
  internal static TextParser<string> AsString(
    this TextParser<char> charParser,
    bool atLeastOnce = false
  )
  {
    if (atLeastOnce)
    {
      return charParser.AtLeastOnce().Select(chars => new string(chars));
    }
    else
    {
      return charParser.Many().Select(chars => new string(chars));
    }
  }

  /// <summary>
  /// Parse multiple elements up to the provided value.
  /// </summary>
  /// <typeparam name="Source">The type of the source parser.</typeparam>
  /// <param name="source">The source parser.</param>
  /// <param name="value">The maximum number.</param>
  /// <returns></returns>
  internal static TextParser<IEnumerable<Source>> UpTo<Source>(this TextParser<Source> source, int value)
  {
    if (value == 1)
    {
      return source.Select(x => new Source[] { x } as IEnumerable<Source>);
    }
    else
      return from self in source
        from other in source.UpTo(value - 1).OptionalOrDefault([])
        select self.ThenConcat(other);
  }
}
