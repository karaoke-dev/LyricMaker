using System;
using System.Linq;
using LyricMaker.AutoComplete.TimeTags;
using LyricMaker.Model;
using LyricMaker.Model.Tags;
using NUnit.Framework;

namespace LyricMaker.Tests.AutoComplete.TimeTags
{
    [TestFixture]
    public class TimeTagAutoCompleteUnitTest
    {
        [Ignore("This feature has not been implemented")]
        public void TestLyricWithCheckLineEnd(string lyric, string[] actualTimeTags, bool applyConfig)
        {
            var config = generatorConfig(applyConfig ? nameof(TimeTagAutoCompleteParameter.CheckLineEnd) : null);
            runTimeTagCheckTest(lyric, actualTimeTags, config);
        }

        [TestCase("か", new[] { "[0,start]:" }, false)]
        [TestCase("か", new[] { "[0,start]:", "[0,end]:" }, true)]
        public void TestLyricWithCheckLineEndKeyUp(string lyric, string[] actualTimeTags, bool applyConfig)
        {
            var config = generatorConfig(applyConfig ? nameof(TimeTagAutoCompleteParameter.CheckLineEndKeyUp) : null);
            runTimeTagCheckTest(lyric, actualTimeTags, config);
        }

        [Ignore("This feature has not been implemented")]
        public void TestLyricWithCheckBlankLine(string lyric, string[] actualTimeTags, bool applyConfig)
        {
            var config = generatorConfig(applyConfig ? nameof(TimeTagAutoCompleteParameter.CheckBlankLine) : null);
            runTimeTagCheckTest(lyric, actualTimeTags, config);
        }

        [TestCase("     ", new[] { "[0,start]:", "[1,start]:", "[2,start]:", "[3,start]:", "[4,start]:" }, false)]
        [TestCase("     ", new[] { "[0,start]:" }, true)]
        public void TestLyricWithCheckWhiteSpace(string lyric, string[] actualTimeTags, bool applyConfig)
        {
            var config = generatorConfig(applyConfig ? nameof(TimeTagAutoCompleteParameter.CheckWhiteSpace) : null);
            runTimeTagCheckTest(lyric, actualTimeTags, config);
        }

        [Ignore("This feature has not been implemented")]
        public void TestLyricWithCheckWhiteSpaceKeyUp(string lyric, string[] actualTimeTags, bool applyConfig)
        {
            var config = generatorConfig(applyConfig ? nameof(TimeTagAutoCompleteParameter.CheckWhiteSpaceKeyUp) : null);
            runTimeTagCheckTest(lyric, actualTimeTags, config);
        }

        [TestCase("a　b　c　d　e", new[] { "[0,start]:", "[2,start]:", "[4,start]:", "[6,start]:", "[8,start]:" }, false)]
        [TestCase("a　b　c　d　e", new[] { "[0,start]:", "[1,start]:", "[2,start]:", "[3,start]:", "[4,start]:", "[5,start]:", "[6,start]:", "[7,start]:", "[8,start]:" }, true)]
        [TestCase("Ａ　Ｂ　Ｃ　Ｄ　Ｅ", new[] { "[0,start]:", "[2,start]:", "[4,start]:", "[6,start]:", "[8,start]:" }, false)]
        [TestCase("Ａ　Ｂ　Ｃ　Ｄ　Ｅ", new[] { "[0,start]:", "[1,start]:", "[2,start]:", "[3,start]:", "[4,start]:", "[5,start]:", "[6,start]:", "[7,start]:", "[8,start]:" }, true)]
        public void TestLyricWithCheckWhiteSpaceAlphabet(string lyric, string[] actualTimeTags, bool applyConfig)
        {
            var config = generatorConfig(nameof(TimeTagAutoCompleteParameter.CheckWhiteSpace),
                applyConfig ? nameof(TimeTagAutoCompleteParameter.CheckWhiteSpaceAlphabet) : null);
            runTimeTagCheckTest(lyric, actualTimeTags, config);
        }

        [TestCase("0　1　2　3　4", new[] { "[0,start]:", "[2,start]:", "[4,start]:", "[6,start]:", "[8,start]:" }, false)]
        [TestCase("0　1　2　3　4", new[] { "[0,start]:", "[1,start]:", "[2,start]:", "[3,start]:", "[4,start]:", "[5,start]:", "[6,start]:", "[7,start]:", "[8,start]:" }, true)]
        [TestCase("０　１　２　３　４", new[] { "[0,start]:", "[2,start]:", "[4,start]:", "[6,start]:", "[8,start]:" }, false)]
        [TestCase("０　１　２　３　４", new[] { "[0,start]:", "[1,start]:", "[2,start]:", "[3,start]:", "[4,start]:", "[5,start]:", "[6,start]:", "[7,start]:", "[8,start]:" }, true)]
        public void TestLyricWithCheckWhiteSpaceDigit(string lyric, string[] actualTimeTags, bool applyConfig)
        {
            var config = generatorConfig(nameof(TimeTagAutoCompleteParameter.CheckWhiteSpace),
                applyConfig ? nameof(TimeTagAutoCompleteParameter.CheckWhiteSpaceDigit) : null);
            runTimeTagCheckTest(lyric, actualTimeTags, config);
        }

        [TestCase("!　!　!　!　！", new[] { "[0,start]:", "[2,start]:", "[4,start]:", "[6,start]:", "[8,start]:" }, false)]
        [TestCase("!　!　!　!　！", new[] { "[0,start]:", "[1,start]:", "[2,start]:", "[3,start]:", "[4,start]:", "[5,start]:", "[6,start]:", "[7,start]:", "[8,start]:" }, true)]
        public void TestLyricWitCheckWhiteSpaceAsciiSymbol(string lyric, string[] actualTimeTags, bool applyConfig)
        {
            var config = generatorConfig(nameof(TimeTagAutoCompleteParameter.CheckWhiteSpace),
                applyConfig ? nameof(TimeTagAutoCompleteParameter.CheckWhiteSpaceAsciiSymbol) : null);
            runTimeTagCheckTest(lyric, actualTimeTags, config);
        }

        [TestCase("がんばって", new[] { "[0,start]:", "[2,start]:", "[4,start]:" }, false)]
        [TestCase("がんばって", new[] { "[0,start]:", "[1,start]:", "[2,start]:", "[4,start]:" }, true)]
        public void TestLyricWithCheckWhiteCheckん(string lyric, string[] actualTimeTags, bool applyConfig)
        {
            var config = generatorConfig(applyConfig ? nameof(TimeTagAutoCompleteParameter.Checkん) : null);
            runTimeTagCheckTest(lyric, actualTimeTags, config);
        }

        [TestCase("買って", new[] { "[0,start]:", "[2,start]:" }, false)]
        [TestCase("買って", new[] { "[0,start]:", "[1,start]:", "[2,start]:" }, true)]
        public void TestLyricWithCheckっ(string lyric, string[] actualTimeTags, bool applyConfig)
        {
            var config = generatorConfig(applyConfig ? nameof(TimeTagAutoCompleteParameter.Checkっ) : null);
            runTimeTagCheckTest(lyric, actualTimeTags, config);
        }

        [Test]
        public void TestTagWithRubyLyric()
        {
            var config = generatorConfig(null);
            var lyric = new LyricLine
            {
                Text = "明日いっしょに遊びましょう！",
                RubyTags = new[]
                {
                    new RubyTag
                    {
                        StartIndex = 0,
                        EndIndex = 2,
                        Text = "あした"
                    },
                    new RubyTag
                    {
                        StartIndex = 7,
                        EndIndex = 8,
                        Text = "あそ"
                    }
                }
            };

            var actualTimeTags = new[]
            {
                "[0,start]:",
                "[0,start]:",
                "[0,start]:",
                "[2,start]:",
                "[4,start]:",
                "[6,start]:",
                "[7,start]:",
                "[7,start]:",
                "[8,start]:",
                "[9,start]:",
                "[10,start]:",
                "[12,start]:",
                "[13,start]:"
            };
            runTimeTagCheckTest(lyric, actualTimeTags, config);
        }

        #region Tools

        private static void runTimeTagCheckTest(string text, string[] actualTimeTags, TimeTagAutoCompleteParameter config)
        {
            var lyric = new LyricLine { Text = text };
            runTimeTagCheckTest(lyric, actualTimeTags, config);
        }

        private static void runTimeTagCheckTest(LyricLine lyric, string[] actualTimeTags, TimeTagAutoCompleteParameter config)
        {
            var generator = new TimeTagAutoComplete();

            // create time tag and actually time tag.
            var timeTags = generator?.Complete(lyric, config);
            var actualIndexed = TestCaseTagHelper.ParseTimeTags(actualTimeTags);

            // check should be equal
            TimeTagAssert.ArePropertyEqual(timeTags, actualIndexed);
        }

        private static TimeTagAutoCompleteParameter generatorConfig(params string[] properties)
        {
            var config = new TimeTagAutoCompleteParameter();
            if (properties == null)
                return config;

            foreach (var propertyName in properties)
            {
                if (propertyName == null)
                    continue;

                var theMethod = config.GetType().GetProperty(propertyName);
                if (theMethod == null)
                    throw new MissingMethodException("Config is not exist.");

                theMethod.SetValue(config, true);
            }

            return config;
        }

        #endregion
    }
}
