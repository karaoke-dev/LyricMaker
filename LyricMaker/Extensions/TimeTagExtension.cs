using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LyricMaker.Extensions
{
    /// <summary>
    /// Extension to process time tag format like [MM:ss.xx] or [MM:ss:xx]
    /// </summary>
    public class TimeTagExtension
    {
        public static void SetDecimalPointPeriod() { decimalPoint = '.'; }
        public static void SetDecimalPointColon() { decimalPoint = ':'; }

        private static char decimalPoint = '.';
        public static char DecimalPoint => decimalPoint;

        private static Regex timeTagRegex = new Regex(@"\[\d\d:\d\d[:.]\d\d\]");
        private static Regex headTimeTagRegex = new Regex(@"^\[\d\d:\d\d([:.]\d\d)?\]");

        public static Regex TimeTagRegex => timeTagRegex;
        public static Regex HeadTimeTagRegex => headTimeTagRegex;

        /// <summary>
        /// Convert milliseconds to format [mm:ss.ss]
        /// </summary>
        /// <example>
        /// Input : 17970
        /// Output : [00:17:97]
        /// </example>
        /// <param name="millionSecond"></param>
        /// <returns></returns>
        public static string MillionSecondToTimeTag(int millionSecond)
        {
            return millionSecond < 0 ? "" : string.Format("[{0:D2}:{1:D2}" + DecimalPoint + "{2:D2}]", millionSecond / 1000 / 60, millionSecond / 1000 % 60, millionSecond / 10 % 100);
        }

        public static int TimeTagToMillionSecond(string timeTag)
        {
            if (timeTag.Length < 10 || timeTag[0] != '[' || !char.IsDigit(timeTag[1]))
                return -1;
            var minute = int.Parse(timeTag.Substring(1, 2));
            var second = int.Parse(timeTag.Substring(4, 2));
            var million = int.Parse(timeTag.Substring(7, 2));

            return (minute * 60 + second) * 1000 + million * 10;
        }

        /// <summary>
        /// Remove time string like [mm:ss.ss]
        /// </summary>
        /// <example>
        /// Input : [00:17:97]帰[00:18:37]り[00:18:55]道[00:18:94]は[00:19:22]
        /// Output : 帰り道は
        /// </example>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveTimeTag(string text)
        {
            return TimeTagRegex.Replace(text, "");
        }

        /// <summary>
        /// String/Value pair
        /// </summary>
        public class Pair
        {
            public int MillionSecond;

            public string Word;

            public Pair(int millionSecond, string word)
            {
                MillionSecond = millionSecond;
                Word = word;
            }
        }

        /// <summary>
        /// Separate string into Pair array.
        /// </summary>
        /// <example>
        /// Input : [00:17:97]帰[00:18:37]り[00:18:55]道[00:18:94]は[00:19:22]
        /// Output : [new Pair(17970,"帰"),new Pair(18370,"り"),new Pair(18550,"道"),new Pair(18940,"は"),new Pair(19220,"")]
        /// </example>
        /// <param name="line"></param>
        /// <returns></returns>
        public static Pair[] SeparateKaraokeLine(string line)
        {
            var offset = 0;
            var tt = "";
            var h = HeadTimeTagRegex.Match(line);
            if (h.Success)
            {
                tt = h.Value;
                offset = h.Length;
            }
            var ret = new List<Pair>();
            var mc = TimeTagRegex.Matches(line, offset);
            if (mc.Count == 0)
            {
                ret.Add(ConvertPair(tt, line.Substring(offset)));
                return ret.ToArray();
            }

            ret.Add(ConvertPair(tt, line.Substring(offset, mc[0].Index - offset)));

            int i;
            for (i = 1; i < mc.Count; i++)
            {
                offset = mc[i - 1].Index + mc[i - 1].Length;
                ret.Add(ConvertPair(mc[i - 1].Value, line.Substring(offset, mc[i].Index - offset)));
            }
            offset = mc[i - 1].Index + mc[i - 1].Length;
            ret.Add(ConvertPair(mc[i - 1].Value, line.Substring(offset)));

            return ret.ToArray();

            //Add pair, convert [mm:ss.ss] into millisecond
            Pair ConvertPair(string timeTag, string text)
            {
                return new Pair(TimeTagToMillionSecond(timeTag), text);
            }
        }
    }
}
