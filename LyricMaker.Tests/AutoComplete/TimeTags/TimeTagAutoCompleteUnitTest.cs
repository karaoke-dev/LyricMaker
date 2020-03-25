using System.Linq;
using LyricMaker.AutoComplete.TimeTags;
using NUnit.Framework;

namespace LyricMaker.Tests.AutoComplete.TimeTags
{
    [TestFixture]
    public class TimeTagAutoCompleteUnitTest : BaseUnitTest
    {
        [TestCase("[01:02:14]何[01:02:61][01:02:84]も[01:03:36]", new int[]{1,2,3,4})]
        [TestCase("[01:02:14]何[01:02:61]度[01:02:84][01:03:36]", new int[]{1,3,4})]
        [TestCase("[01:02:14]何[01:02:61]度[01:02:84]も[01:03:36]", new int[]{1,3,5,6})]
        [TestCase("[01:02:14]何[01:02:61]度[01:02:84]も[01:03:36]目[01:03:96]", new int[]{1,3,5,7,8})]
        public void TestTimeTagAutoComplete(string text, int[] expect)
        {
            var lyric = GenerateLyric(text);

            var complete = new TimeTagAutoComplete();
            complete.Complete(lyric, new TimeTagAutoCompleteParameter
            {
                CheckLineEnd = true
            });

            var firstLine = lyric.Lines.FirstOrDefault();
            Assert.NotNull(firstLine);

            var checkedIndex = firstLine.TimeTags.Select((v, i) => v.Check ? i : -1).Where(i=>i != -1).ToArray();
            Assert.AreEqual(checkedIndex, expect);
        }
    }
}
