namespace LyricMaker.Parser.Component
{
    /// <summary>
    /// Base component pass target type of object and get expect return type
    /// </summary>
    /// <typeparam name="T">Encode and decode object type.</typeparam>
    /// <typeparam name="TR">Encode and decode source type.</typeparam>
    public abstract class ParserComponent<T,TR>
    {
        /// <summary>
        /// Decode to target class and leave remain text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public abstract T Decode(TR text);

        /// <summary>
        /// Encode target component
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public abstract TR Encode(T component);
    }

    /// <inheritdoc>
    /// Base component pass string
    /// </inheritdoc>
    /// <typeparam name="T">Encode and decode object type</typeparam>
    public abstract class ParserComponent<T> : ParserComponent<T,string>
    {

    }
}
