using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AnalysisTrend.Functions;
class WordFunction
{
    public static string RemoveUnnecessaryChar(string text)
    {
        List<string> UnnecessaryCharList = MakeUnnecessaryChar();
        string str = text;

        str = Regex.Replace(str, @"\(.*?\)", "");
        str = Regex.Replace(str, @"（.*?）", "");
        str = Regex.Replace(str, @"【.*?】", "");
        str = Regex.Replace(str, @"［.*?］", "");
        str = Regex.Replace(str, @"＜.*?＞", "");
        str = Regex.Replace(str, @"＝.*?＝", "");
        str = Regex.Replace(str, @"《.*?》", "");
        str = Regex.Replace(str, @"≪.*?≫", "");
        str = Regex.Replace(str, @"〔.*〕", "");
        str = Regex.Replace(str, @"〈.*〉", "");

        foreach(string unnecessaryChar in UnnecessaryCharList)
        {
            str = str.Replace(unnecessaryChar, " ");
        }

        return str;
    }

    public static string ConvertFullwidthSpace(string text)
    {
        string str = text;

        Regex reg = new Regex("(?<endChar>[^A-Za-z]) (?<startChar>[^A-Za-z])");
        for (Match m = reg.Match(str); m.Success; m = m.NextMatch())
        {
            str = str.Replace(m.Value, m.Groups["endChar"] + "" + m.Groups["startChar"]);
        }

        reg = new Regex("(?<endChar>[A-Za-z]) (?<startChar>[^A-Za-z])");
        for (Match m = reg.Match(str); m.Success; m = m.NextMatch())
        {
            str = str.Replace(m.Value, m.Groups["endChar"] + "" + m.Groups["startChar"]);
        }

        reg = new Regex("(?<endChar>[^A-Za-z]) (?<startChar>[A-Za-z])");
        for (Match m = reg.Match(str); m.Success; m = m.NextMatch())
        {
            str = str.Replace(m.Value, m.Groups["endChar"] + "" + m.Groups["startChar"]);
        }

        return str;
    }

    public static string ConvertFullCharToHalfChar(string text)
    {
        string str = text;

        str = ConvertNumChar(str);
        str = ConvertAlphabetChar(str);
        str = ConvertOtherChar(str);

        return str;
    }

    public static string ConvertUnnecessaryDoubleByteHyphen(string text)
    {
        string str = text;
        Regex reg = new Regex("(?<endChar>[A-Za-z])ー(?<startChar>[A-Za-z])");
        for (Match m = reg.Match(str); m.Success; m = m.NextMatch())
        {
            str = str.Replace(m.Value, m.Groups["endChar"] + "-" + m.Groups["startChar"]);
        }
        return str;
    }

    private static string ConvertAlphabetChar(string text)
    {
        string str = text;
        str = str.Replace("Ａ", "A");
        str = str.Replace("Ｂ", "B");
        str = str.Replace("Ｃ", "C");
        str = str.Replace("Ｄ", "D");
        str = str.Replace("Ｅ", "E");
        str = str.Replace("Ｆ", "F");
        str = str.Replace("Ｇ", "G");
        str = str.Replace("Ｈ", "H");
        str = str.Replace("Ｉ", "I");
        str = str.Replace("Ｊ", "J");
        str = str.Replace("Ｋ", "K");
        str = str.Replace("Ｌ", "L");
        str = str.Replace("Ｍ", "M");
        str = str.Replace("Ｎ", "N");
        str = str.Replace("Ｏ", "O");
        str = str.Replace("Ｐ", "P");
        str = str.Replace("Ｑ", "Q");
        str = str.Replace("Ｒ", "R");
        str = str.Replace("Ｓ", "S");
        str = str.Replace("Ｔ", "T");
        str = str.Replace("Ｕ", "U");
        str = str.Replace("Ｖ", "V");
        str = str.Replace("Ｗ", "W");
        str = str.Replace("Ｘ", "X");
        str = str.Replace("Ｙ", "Y");
        str = str.Replace("Ｚ", "Z");
        str = str.Replace("ａ", "a");
        str = str.Replace("ｂ", "b");
        str = str.Replace("ｃ", "c");
        str = str.Replace("ｄ", "d");
        str = str.Replace("ｅ", "e");
        str = str.Replace("ｆ", "f");
        str = str.Replace("ｇ", "g");
        str = str.Replace("ｈ", "h");
        str = str.Replace("ｉ", "i");
        str = str.Replace("ｊ", "j");
        str = str.Replace("ｋ", "k");
        str = str.Replace("ｌ", "l");
        str = str.Replace("ｍ", "m");
        str = str.Replace("ｎ", "n");
        str = str.Replace("ｏ", "o");
        str = str.Replace("ｐ", "p");
        str = str.Replace("ｑ", "q");
        str = str.Replace("ｒ", "r");
        str = str.Replace("ｓ", "s");
        str = str.Replace("ｔ", "t");
        str = str.Replace("ｕ", "u");
        str = str.Replace("ｖ", "v");
        str = str.Replace("ｗ", "w");
        str = str.Replace("ｘ", "x");
        str = str.Replace("ｙ", "y");
        str = str.Replace("ｚ", "z");
        return str;
    }

    private static string ConvertNumChar(string text)
    {
        string str = text;
        str = str.Replace("０", "0");
        str = str.Replace("１", "1");
        str = str.Replace("２", "2");
        str = str.Replace("３", "3");
        str = str.Replace("４", "4");
        str = str.Replace("５", "5");
        str = str.Replace("６", "6");
        str = str.Replace("７", "7");
        str = str.Replace("８", "8");
        str = str.Replace("９", "9");
        return str;
    }

    private static string ConvertOtherChar(string text)
    {
        string str = text;
        // full-width char to half-width char
        str = str.Replace("！", "!");
        str = str.Replace("？", "?");
        str = str.Replace("．", ".");
        str = str.Replace("，", ",");
        str = str.Replace("～", "~");
        str = str.Replace("＆", "&");
        str = str.Replace("＠", "@");
        str = str.Replace("／", "/");
        str = str.Replace("／", "/");
        str = str.Replace("：", ":");
        str = str.Replace("；", ";");
        str = str.Replace("＞", ">");
        str = str.Replace("＜", "<");
        str = str.Replace("＋", "+");
        str = str.Replace("＃", "#");
        str = str.Replace("％", "%");

        // other
        str = str.Replace("   ", " ");
        str = str.Replace("  ", " ");

        return str;
    }

    public static List<string> MakeUnnecessaryChar()
    {
        List<string> list = new()
        {
            "・",
            "「",
            "」",
            "[",
            "]",
            "◇",
            "□",
            "■",
            "◆",
            "★",
            "☆",
            "▼",
            "▽",
            "●",
            "♡",
            "♪",
            "♯",
            "“",
            "”",
            "〝",
            "〟",
            "　"
        };
        return list;
    }
}