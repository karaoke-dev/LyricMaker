using LyricMaker.Model;

namespace LyricMaker.Parser
{
    /// <summary>
    /// Base abstract class for encode/decode lyric file
    /// </summary>
    public abstract class LyricParser
    {
        /// <summary>
        /// Decode 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public abstract Lyric Decode(string text);

        /// <summary>
        /// Encode 
        /// </summary>
        /// <param name="lyric"></param>
        /// <returns></returns>
        public abstract string Encode(Lyric lyric);
    }
}
