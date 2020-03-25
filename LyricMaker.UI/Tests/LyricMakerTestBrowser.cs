using LyricMaker.UI.Screens.Backgrounds;
using osu.Framework;
using osu.Framework.Graphics;
using osu.Framework.Platform;
using osu.Framework.Screens;
using osu.Framework.Testing;
using osuTK.Graphics;

namespace LyricMaker.UI.Tests
{
    public class LyricMakerTestBrowser : Game
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();

            LoadComponentAsync(new ScreenStack(new BackgroundScreenDefault { Colour = new Color4(0.5f, 0.5f, 0.5f, 1) })
            {
                Depth = 10,
                RelativeSizeAxes = Axes.Both,
            }, AddInternal);

            // Have to construct this here, rather than in the constructor, because
            // we depend on some dependencies to be loaded within OsuGameBase.load().
            Add(new TestBrowser());
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}
