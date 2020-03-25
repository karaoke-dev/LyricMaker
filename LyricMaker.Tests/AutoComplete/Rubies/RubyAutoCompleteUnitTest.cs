using System.Linq;
using LyricMaker.AutoComplete;
using LyricMaker.AutoComplete.Rubies;
using NUnit.Framework;

namespace LyricMaker.Tests.AutoComplete.Rubies
{
    [TestFixture]
    public class RubyAutoCompleteUnitTest : BaseUnitTest
    {
        [TestCase("帰", "キ")]
        [TestCase("り", "リ")]
        [TestCase("道", "ミチ")]
        [TestCase("は", "ハ")]
        public void TestRubyAutoComplate(string text, string expect)
        {
            var lyric = GenerateLyric(text);

            var complete = new RubyAutoComplete();
            complete.Complete(lyric, AutoCompleteStrategy.ReplaceIfConflict);

            var firstRubyTag = lyric.RubyTags.FirstOrDefault();
            
            // TODO : the expect result is not right
            Assert.AreEqual(firstRubyTag.Ruby, expect);
        }
    }
}