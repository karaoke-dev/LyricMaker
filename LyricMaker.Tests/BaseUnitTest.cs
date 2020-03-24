using LyricMaker.Extensions;
using System.Globalization;
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

        protected void IsLyricEqual(string expected, string actual)
        {
            var expectedLyric = expected.Replace("\n", "");
            var actualLyric = expected.Replace("\n", "");

            Assert.AreEqual(expectedLyric, actualLyric);
        }
    }
}
