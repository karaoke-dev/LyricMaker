namespace LyricMaker.AutoComplete
{
    /// <summary>
    /// Strategy for <see cref="PositionTagAutoComplete{T}"/>
    /// </summary>
    public enum AutoCompleteStrategy
    {
        /// <summary>
        /// Clean-up all the old tag and replace new one
        /// </summary>
        ReplaceAll,

        /// <summary>
        /// Replace if conflict
        /// </summary>
        ReplaceIfConflict,

        /// <summary>
        /// Not replace if conflict
        /// </summary>
        NotReplaceIfConflict
    }
}
