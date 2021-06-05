using LyricMaker.Extensions;
using LyricMaker.Parser.Component;
using NUnit.Framework;

namespace LyricMaker.Tests.Parser.Component
{
    [TestFixture]
    public class TimeTagUnitTest
    {
        [SetUp]
        public void Initialize()
        {
            // Use [mm:ss:ss]
            TimeTagExtension.SetDecimalPointColon();
        }
        
        [Test]
        public void TestKaraokeLineEncodeAndDecode()
        {
            const string lyricText = "[00:17:97]帰[00:18:37]り[00:18:55]道[00:18:94]は[00:19:22]";
            var timeTagComponent = new TimeTagParserComponent();
            var lyric = timeTagComponent.Decode(lyricText);

            // Test lyric
            Assert.AreEqual(lyric.Text, "帰り道は");

            // Test time tag
            Assert.AreEqual(lyric.TimeTags.Length, 10);
            Assert.AreEqual(lyric.TimeTags[0].Time, -1 );
            Assert.AreEqual(lyric.TimeTags[1].Time, 17970);
            Assert.AreEqual(lyric.TimeTags[2].Time, -1);
            Assert.AreEqual(lyric.TimeTags[3].Time, 18370);

            // Then encode
            var encodeResult = timeTagComponent.Encode(lyric);

            // Test encode result
            Assert.AreEqual(encodeResult, encodeResult);
        }
    }
}
