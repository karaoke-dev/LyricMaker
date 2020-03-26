using osu.Framework;
using osu.Framework.Platform;
using System;
using System.Linq;

namespace LyricMaker.Tests
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            bool portable = args.Contains(@"--portable");

            using (GameHost host = Host.GetSuitableHost(@"lyric-maker-visual-tests", portableInstallation: portable))
            {
                host.Run(new VisualTestGame());
            }
        }
    }
}
