using LyricMaker.UI.Tests;
using osu.Framework;
using osu.Framework.Platform;
using System;

namespace LyricMaker.Tests
{
    public static class VisualTestRunner
    {
        [STAThread]
        public static int Main(string[] args)
        {
            using (DesktopGameHost host = Host.GetSuitableHost(@"LyricMaker", true))
            {
                host.Run(new LyricMakerTestBrowser());
                return 0;
            }
        }
    }
}
