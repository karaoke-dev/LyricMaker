using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.IO.Stores;

namespace LyricMaker.Tests
{
    internal class TestGame : Game
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new NamespacedResourceStore<byte[]>(new DllResourceStore(typeof(TestGame).Assembly), "Resources"));
        }
    }
}
