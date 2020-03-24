namespace LyricMaker.Model.Tags
{
    /// <summary>
    /// At tag
    /// </summary>
    /// <example>
    /// @Name=Value
    /// </example>
    public struct AtTag
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name;

        /// <summary>
        /// Value
        /// </summary>
        public string Value;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public AtTag(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
