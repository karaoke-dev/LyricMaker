using LyricMaker.UI.Screens;
using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osu.Framework.Testing;

namespace LyricMaker.Tests.Visual.Screens
{
    [TestFixture]
    public class TestSceneLyricMakerScreen : TestScene
    {
        private LyricMakerScreen baseScreen;
        private ScreenStack stack;

        [SetUp]
        public void SetupTest() => Schedule(() =>
        {
            Clear();
            Add(stack = new ScreenStack(baseScreen = new LyricMakerScreen())
            {
                RelativeSizeAxes = Axes.Both
            });
        });
    }
}
