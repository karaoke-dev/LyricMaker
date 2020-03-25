using LyricMaker.UI.Screens;
using LyricMaker.UI.Tests.Visual;
using NUnit.Framework;

namespace LyricMaker.Tests.Visual.Screens
{
    [TestFixture]
    public class TestSceneLyricMakerScreen : LyricMakerTestScene
    {
        public TestSceneLyricMakerScreen()
        {
            Child = new LyricMakerScreen();
        }
    }
}
