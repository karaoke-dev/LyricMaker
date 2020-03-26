using LyricMaker.UI.Screens;
using NUnit.Framework;
using osu.Framework.Testing;

namespace LyricMaker.Tests.Visual.Screens
{
    [TestFixture]
    public class TestSceneLyricMakerScreen : TestScene
    {
        public TestSceneLyricMakerScreen()
        {
            Child = new LyricMakerScreen();
        }
    }
}
