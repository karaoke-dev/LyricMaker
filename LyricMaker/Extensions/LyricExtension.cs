using LyricMaker.Model;
using LyricMaker.Model.Tags;
using System.Collections.Generic;
using System.Linq;
using LyricMaker.Model.Common;

namespace LyricMaker.Extensions
{
    public static class LyricExtension
    {
        public static List<RubyQueryResult> QueryRubies(this Lyric lyric,string text,int line = 0)
        {
            var result = new List<RubyQueryResult>();
            for (var i = 0; i < text.Length; i++)
            {
                for (var take = text.Length - i; take > 0; take--)
                {
                    var queryText = text.Substring(i,take);

                    // Query result
                    var results = lyric.RubyTags.Where(x => x.Parent == queryText).OrderByDescending(x => x.StartPosition.HasValue || x.EndPosition.HasValue);
                    foreach (var queryResult in results)
                    {
                        if (string.IsNullOrEmpty(queryResult.Ruby))
                            continue;

                        var startPosition = new Position(line, i);
                        var endPosition = new Position(line, i + take);

                        // Check start position is in the range
                        var resultStartPosition = queryResult.StartPosition;
                        if (resultStartPosition.HasValue && resultStartPosition.Value > startPosition)
                            continue;

                        // Check end position is in the range
                        var resultEndPosition = queryResult.EndPosition;
                        if (resultEndPosition.HasValue && resultEndPosition.Value < endPosition)
                            continue;

                        result.Add(new RubyQueryResult
                        {
                            StartIndex = i,
                            EndIndex = i + 1,
                            Ruby = queryResult
                        });

                        i = i + take - 1;
                        break;
                    }
                }
            }
            return result;
        }
    }

    public struct RubyQueryResult
    {
        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public RubyTag Ruby { get; set; }
    }
}
