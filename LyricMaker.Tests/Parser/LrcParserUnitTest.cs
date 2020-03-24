using System.Collections.Generic;
using System.IO;
using System.Linq;
using LyricMaker.Parser;
using LyricMaker.Tests.Resources;
using NUnit.Framework;

namespace LyricMaker.Tests.Parser
{
    [TestFixture]
    public class LrcParserUnitTest : BaseUnitTest
    {
        private static IEnumerable<string> allLrcFileNames => TestResources.GetStore().GetAvailableResources()
            .Where(res => res.EndsWith(".lrc")).Select(Path.GetFileNameWithoutExtension);

        [TestCaseSource(nameof(allLrcFileNames))]
        public void TestDecodeEncodedBeatmap(string fileName)
        {
            using (var stream = TestResources.OpenLrcResource(fileName))
            using (var sr = new StreamReader(stream))
            {
                var text = sr.ReadToEnd();
                // Parser song
                var parser = new LrcParser();
                var song = parser.Decode(text);

                // Encode to lyric
                var newLyricText = parser.Encode(song);

                // Encode and decode result should be same
                IsLyricEqual(newLyricText, text);
            }
        }
    }
}
