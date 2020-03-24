using LyricMaker.Model.Common;

namespace LyricMaker.Model.Tags
{
    /// <summary>
    /// Ruby tag
    /// </summary>
    /// <example>
    /// @Ruby1=帰,かえ
    /// @Ruby25=時,じか,,[00:38:45]
    /// @Ruby49=時,とき,[00:38:45],[01:04:49]
    /// </example>
    public struct RubyTag : IPositionTag
    {
        /// <summary>
        /// Parent kanji
        /// </summary>
        public string Parent { get; set; }

        /// <summary>
        /// Ruby
        /// </summary>
        public string Ruby { get; set; }

        /// <summary>
        /// Start position
        /// </summary>
        public Position? StartPosition { get; set; }

        /// <summary>
        /// End position
        /// </summary>
        public Position? EndPosition { get; set; }
    }
}
