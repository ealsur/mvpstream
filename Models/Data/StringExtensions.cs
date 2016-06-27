using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MVPStream.Extensions
{
  public static class StringsExtensions
  {
    private static readonly HashSet<StringsExtensions.RegexReplacement> UnaccentRules = new HashSet<StringsExtensions.RegexReplacement>();
    private static readonly Regex UrlCleanRegEx = new Regex("([,¿+:´\"!¡%\\.\\?\\*])|(&quot;)|([\\$@=\\#&;\\\\<>\\{\\}|^~\\[\\]`\\/])", RegexOptions.Compiled);
    private static readonly Regex WordsSpliter = new Regex(string.Format("([A-Z{0}]+[a-z{1}\\d]*)|[_\\s]", (object) "ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞ", (object) "ßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿ"), RegexOptions.Compiled);
    public const string UppercaseAccentedCharacters = "ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝÞ";
    public const string LowercaseAccentedCharacters = "ßàáâãäåæçèéêëìíîïðñòóôõöøùúûüýþÿ";

    static StringsExtensions()
    {
      StringsExtensions.AddUnaccent("([ÀÁÂÃÄÅÆ])", "A");
      StringsExtensions.AddUnaccent("([Ç])", "C");
      StringsExtensions.AddUnaccent("([ÈÉÊË])", "E");
      StringsExtensions.AddUnaccent("([ÌÍÎÏ])", "I");
      StringsExtensions.AddUnaccent("([Ð])", "D");
      StringsExtensions.AddUnaccent("([Ñ])", "N");
      StringsExtensions.AddUnaccent("([ÒÓÔÕÖØ])", "O");
      StringsExtensions.AddUnaccent("([ÙÚÛÜ])", "U");
      StringsExtensions.AddUnaccent("([Ý])", "Y");
      StringsExtensions.AddUnaccent("([Þ])", "T");
      StringsExtensions.AddUnaccent("([ß])", "s");
      StringsExtensions.AddUnaccent("([àáâãäåæ])", "a");
      StringsExtensions.AddUnaccent("([ç])", "c");
      StringsExtensions.AddUnaccent("([èéêë])", "e");
      StringsExtensions.AddUnaccent("([ìíîï])", "i");
      StringsExtensions.AddUnaccent("([ð])", "e");
      StringsExtensions.AddUnaccent("([ñ])", "n");
      StringsExtensions.AddUnaccent("([òóôõöø])", "o");
      StringsExtensions.AddUnaccent("([ùúûü])", "u");
      StringsExtensions.AddUnaccent("([ý])", "y");
      StringsExtensions.AddUnaccent("([þ])", "t");
      StringsExtensions.AddUnaccent("([ÿ])", "y");
      StringsExtensions.AddUnaccent("([–])", "-");
      StringsExtensions.AddUnaccent("([!])", "");
    }

    public static string StripHtml(this string target)
    {
        //Regular expression for html tags
        Regex StripHTMLExpression = new Regex("<\\S[^><]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        return StripHTMLExpression.Replace(target, string.Empty);
    }

    public static string EllipsisWhenLongerThan(this string source, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            return string.Empty;
        }
        if (source.Length <= maxLength || maxLength <= 3)
        {
            return source;
        }
        return source.Substring(0, maxLength - 3) + "...";
    }

    private static void AddUnaccent(string rule, string replacement)
    {
      StringsExtensions.UnaccentRules.Add(new StringsExtensions.RegexReplacement(new Regex(rule, RegexOptions.Compiled), replacement));
    }

    public static string Titleize(this string word)
    {
      return Regex.Replace(StringsExtensions.Humanize(StringsExtensions.Underscore(word)), "\\b([a-z])", (MatchEvaluator) (match => match.Captures[0].Value.ToUpper()));
    }

    public static string Humanize(this string lowercaseAndUnderscoredWord)
    {
      return StringsExtensions.Capitalize(Regex.Replace(lowercaseAndUnderscoredWord, "_", " "));
    }

    public static string Pascalize(this string lowercaseAndUnderscoredWord)
    {
      return Regex.Replace(lowercaseAndUnderscoredWord, "(?:^|_)(.)", (MatchEvaluator) (match => match.Groups[1].Value.ToUpper()));
    }

    public static string Camelize(this string lowercaseAndUnderscoredWord)
    {
      return StringsExtensions.Uncapitalize(StringsExtensions.Pascalize(lowercaseAndUnderscoredWord));
    }

    public static string Underscore(this string pascalCasedWord)
    {
      return Regex.Replace(Regex.Replace(Regex.Replace(pascalCasedWord, "([A-Z]+)([A-Z][a-z])", "$1_$2"), "([a-z\\d])([A-Z])", "$1_$2"), "[-\\s]", "_").ToLower();
    }

    public static string Capitalize(this string word)
    {
      return word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
    }

    public static string Uncapitalize(this string word)
    {
      return word.Substring(0, 1).ToLower() + word.Substring(1);
    }

    public static string Dasherize(this string underscoredWord)
    {
      return underscoredWord.Replace('_', '-').Replace(' ', '-');
    }

    public static string Unaccent(this string word)
    {
      return Enumerable.Aggregate<StringsExtensions.RegexReplacement, string>((IEnumerable<StringsExtensions.RegexReplacement>) StringsExtensions.UnaccentRules, word, (Func<string, StringsExtensions.RegexReplacement, string>) ((current, rule) => rule.Regex.Replace(current, rule.Replacement)));
    }

    public static string ToUrl(this string urlWord)
    {
      urlWord = urlWord.Trim().Replace("..", ".").Replace("./", "/").Replace("-&-", "-").Replace("?.", ".");
      urlWord = StringsExtensions.UrlCleanRegEx.Replace(urlWord, "");
      return StringsExtensions.Dasherize(StringsExtensions.Unaccent(urlWord)).ToLowerInvariant();
    }

    public static IEnumerable<string> SplitWords(this string composedPascalCaseWords)
    {
      foreach (Match match in StringsExtensions.WordsSpliter.Matches(composedPascalCaseWords))
        yield return match.Value;
    }

    public static int SafeParse(this string source, int defaultValue)
    {
      if (string.IsNullOrEmpty(source))
      {
        return defaultValue;
      }
      else
      {
        int result;
        return int.TryParse(source, out result) ? result : defaultValue;
      }
    }

    public static bool IsIn(this string source, IEnumerable<string> values)
    {
      return Enumerable.Contains<string>(values, source);
    }

    public static string Sanitize(this string source)
    {
      if (string.IsNullOrEmpty(source))
        return source;
      StringBuilder stringBuilder = new StringBuilder(source.Length);
      foreach (char ch in Enumerable.Where<char>((IEnumerable<char>) source, (Func<char, bool>) (c => StringsExtensions.IsLegalXmlChar((int) c))))
        stringBuilder.Append(ch);
      return ((object) stringBuilder).ToString();
    }

    private static bool IsLegalXmlChar(int character)
    {
      return character == 9 || character == 10 || character == 13 || (character >= 32 && character <= 55295 || character >= 57344 && character <= 65533) || character >= 65536 && character <= 1114111;
    }
    
    public static string TruncateAt(this string source, int maxLength)
    {
      if (source == null)
        return (string) null;
      if (maxLength <= 0)
        return "";
      int length = source.Length;
      if (maxLength > length)
        return source;
      else
        return source.Substring(0, maxLength);
    }

    public static IEnumerable<string> ToWordsList(this string source)
    {
      IEnumerable<string> enumerable;
      if (!string.IsNullOrWhiteSpace(source))
        enumerable = Enumerable.Where<string>(Enumerable.Select<string, string>((IEnumerable<string>) source.Split(new char[2]
        {
          ' ',
          ','
        }), (Func<string, string>) (x => x.Trim())), (Func<string, bool>) (x => x != ""));
      else
        enumerable = Enumerable.Empty<string>();
      return enumerable;
    }

    private class RegexReplacement
    {
      public Regex Regex { get; private set; }

      public string Replacement { get; private set; }

      public RegexReplacement(Regex regex, string replacement)
      {
        this.Regex = regex;
        this.Replacement = replacement;
      }
    }
  }
}
