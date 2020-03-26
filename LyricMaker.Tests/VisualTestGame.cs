using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace LyricMaker.Tests
{
    internal class VisualTestGame : TestGame
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Child = new SafeAreaContainer
            {
                RelativeSizeAxes = Axes.Both,
                Child = new DrawSizePreservingFillContainer
                {
                    Children = new Drawable[]
                    {
                        new TestBrowser("LyricMaker.Tests"),
                        new CursorContainer(),
                    },
                }
            };
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}
