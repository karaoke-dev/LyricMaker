using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.TokenAttributes;
using LyricMaker.Model.Common;
using LyricMaker.Model.Tags;

namespace LyricMaker.AutoComplete
{
    public abstract class PositionTagAutoComplete<T> where T : IPositionTag
    {
        /// <summary>
        /// Generate analyzer
        /// </summary>
        protected abstract Analyzer Analyzer { get; }
        
        /// <summary>
        /// Clean-up tags
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="newTags"></param>
        /// <returns></returns>
        internal abstract IList<T> CleanUpTags(IList<T> tags, IList<T> newTags = null);

        /// <summary>
        /// Generate list of tag by one lyric line
        /// </summary>
        /// <param name="text"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        internal virtual IList<T> GenerateTagByLine(string text, int line = 0)
        {
            var tags = new List<T>();

            // Tokenize the text
            var tokenStream = Analyzer.GetTokenStream("dummy", new StringReader(text));

            // Get result and offset
            var result = tokenStream.GetAttribute<ICharTermAttribute>();
            var offsetAtt = tokenStream.GetAttribute<IOffsetAttribute>();

            // Reset the stream and convert all result
            tokenStream.Reset();
            while (true)
            {
                // Read next token
                tokenStream.ClearAttributes();
                tokenStream.IncrementToken();

                // Get parsed result
                var parsedResult = result.ToString();
                if (string.IsNullOrEmpty(parsedResult))
                    break;

                // Make tag
                var startIndex = offsetAtt.StartOffset;
                var endIndex = offsetAtt.EndOffset;
                var parent = text.Substring(startIndex, endIndex - startIndex);


                var tag = GenerateTag(parent, parsedResult);
                tag.StartPosition = new Position(line, startIndex);
                tag.EndPosition = new Position(line, endIndex);
                tags.Add(tag);
            }

            // Dispose
            tokenStream.End();
            tokenStream.Dispose();

            return tags;
        }

        /// <summary>
        /// Generate tag by property
        /// </summary>
        /// <param name="parentText"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        protected abstract T GenerateTag(string parentText, string text);
    }
}
