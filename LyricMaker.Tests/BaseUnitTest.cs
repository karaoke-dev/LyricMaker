using LyricMaker.Extensions;
using LyricMaker.Model;
using LyricMaker.Parser;
using NUnit.Framework;

namespace LyricMaker.Tests
{
    public class BaseUnitTest
    {
        [SetUp]
        public void Initialize()
        {
            // Use [mm:ss:ss]
            TimeTagExtension.SetDecimalPointColon();
        }

        protected Lyric GenerateLyric(string lyric)
        {
            var parser = new LrcParser();
            return parser.Decode(lyric);
        }

        protected void IsLyricEqual(string expected, string actual)
        {
            var expectedLyric = expected.Replace("\n", "");
            var actualLyric = expected.Replace("\n", "");

            Assert.AreEqual(expectedLyric, actualLyric);
        }
    }
}
