using LyricMaker.Model.Tags;
using LyricMaker.Parser.Component;
using NUnit.Framework;

namespace LyricMaker.Tests.Parser.Component
{
    [TestFixture]
    public class RubyTagUnitTest : BaseUnitTest
    {
        [TestCase("@Ruby1=帰,かえ", "帰", "かえ")]
        [TestCase("@Ruby1=り,り", "り", "り")]
        [TestCase("@Ruby1=道,みち", "道", "みち")]
        [TestCase("@Ruby1=は,は", "は", "は")]
        public void TestAtTagEncode(string rubyTag,string parent,string ruby)
        {
            // At tag component
            var atTagComponent = new AtTagParserComponent();
            var result = atTagComponent.Decode(rubyTag);

            // Ruby tag component(without filter duplicated)
            var atRubyTagComponent = new RubyTagParserComponent(null,false);
            var rubyResult = atRubyTagComponent.Decode(result.Value);

            // Check is not null;
            Assert.AreEqual(rubyResult.HasValue, true);

            var rubyValue = rubyResult.Value;
            Assert.AreEqual(rubyValue.Parent, parent);
            Assert.AreEqual(rubyValue.Ruby, ruby);
        }
        
        [TestCase("@Ruby1=帰,かえ", "帰", "かえ")]
        [TestCase("@Ruby1=り,り", "り", "り")]
        public void TestAtTagDecode(string rubyTagText, string parent, string ruby)
        {
            // Ruby tag
            var rubyTag = new RubyTag
            {
                Parent = parent,
                Ruby = ruby
            };

            // Ruby tag component
            var atRubyTagComponent = new RubyTagParserComponent(null);
            var atTag = atRubyTagComponent.Encode(rubyTag);

            // At tag component
            var atTagComponent = new AtTagParserComponent();
            var result = atTagComponent.Encode(atTag);

            Assert.AreEqual(result, rubyTagText);
        }
    }
}
