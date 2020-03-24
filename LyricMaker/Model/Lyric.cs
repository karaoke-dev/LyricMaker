using LyricMaker.Model.Tags;

namespace LyricMaker.Model
{
    /// <summary>
    /// Lyric
    /// </summary>
    public class Lyric
    {
        /// <summary>
        /// Lines
        /// </summary>
        public LyricLine[] Lines;

        /// <summary>
        /// Ruby tags
        /// </summary>
        public RubyTag[] RubyTags;

        /// <summary>
        /// Ctor
        /// </summary>
        public Lyric()
        {
            Lines = new LyricLine[0];
            RubyTags = new RubyTag[0];
        }
    }
}
