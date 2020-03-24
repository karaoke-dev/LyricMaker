using LyricMaker.Model.Tags;

namespace LyricMaker.Model
{
    /// <summary>
    /// One line of lyric
    /// </summary>
    public class LyricLine
    {
        /// <summary>
        /// Text
        /// </summary>
        public string Text;

        /// <summary>
        /// Time tags
        /// </summary>
        public TimeTag[] TimeTags;
    }
}
