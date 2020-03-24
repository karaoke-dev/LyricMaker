using LyricMaker.Model.Common;

namespace LyricMaker.Model.Tags
{
    public interface IPositionTag
    {
        /// <summary>
        /// Start position
        /// </summary>
        Position? StartPosition { get; set; }

        /// <summary>
        /// End position
        /// </summary>
        Position? EndPosition { get; set; }
    }
}
