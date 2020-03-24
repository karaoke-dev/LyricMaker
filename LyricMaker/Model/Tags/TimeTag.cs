namespace LyricMaker.Model.Tags
{
    /// <summary>
    /// Time tag
    /// </summary>
    /// <example>
    /// [10:13.12] or [10:13:12]
    /// </example>
    public struct TimeTag
    {
        /// <summary>
        /// Time
        /// </summary>
        public int Time;

        /// <summary>
        /// Is checked
        /// </summary>
        public bool Check;

        /// <summary>
        /// Is keyUp
        /// </summary>
        public bool KeyUp;

        /// <summary>
        /// Checked time
        /// </summary>
        public int CheckedTime => Check ? Time : -1;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <param name="up"></param>
        public TimeTag(int t, bool c = true, bool up = false)
        {
            Time = t;
            Check = c;
            KeyUp = up;
        }
    }
}
