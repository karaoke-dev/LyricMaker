using LyricMaker.Model;
using LyricMaker.Model.Tags;
using LyricMaker.Parser.Component;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LyricMaker.Parser
{
    /// <summary>
    /// Parser for encode and decode .lrc lyric format
    /// </summary>
    public class LrcParser : LyricParser
    {
        /// <summary>
        /// Decode lrc format file
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override Lyric Decode(string text)
        {
            // All lyric lines
            var lines = new List<string>();

            // Components
            var atTags = new List<AtTag>();
            var rubyTags = new List<RubyTag>();
            var lyricLines = new List<LyricLine>();

            // Get all lines
            var sr = new StringReader(text);
            for (var s = sr.ReadLine(); s != null; s = sr.ReadLine())
            {
                lines.Add(s);
            }

            // Get all atTag(@) in text
            var atTagComponent = new AtTagParserComponent();
            foreach (var line in lines.ToList())
            {
                var atTag = atTagComponent.Decode(line);
                if (atTag == null)
                    continue;

                atTags.Add(atTag.Value);
                lines.Remove(line);
            }

            // Get all lyric from remain text
            var timeTagComponent = new TimeTagParserComponent();
            foreach (var line in lines)
            {
                var lyricLine = timeTagComponent.Decode(line);
                lyricLines.Add(lyricLine);
            }

            var lyric = new Lyric
            {
                Lines = lyricLines.ToArray(),
            };

            // Process ruby tags
            var rubyTagComponent = new RubyTagParserComponent(lyric);
            foreach (var atTag in atTags.ToList())
            {
                var rubyTag = rubyTagComponent.Decode(atTag);
                if (rubyTag == null)
                    continue;

                rubyTags.Add(rubyTag.Value);
                atTags.Remove(atTag);
            }

            lyric.RubyTags = rubyTags.ToArray();
            return lyric;
        }

        /// <summary>
        /// Encode lrc format file
        /// </summary>
        /// <param name="lyric"></param>
        /// <returns></returns>
        public override string Encode(Lyric lyric)
        {
            if (lyric == null)
                return null;

            StringBuilder sb = new StringBuilder();

            // Get all lyric in remain text
            var timeTagComponent = new TimeTagParserComponent();
            foreach (var line in lyric.Lines)
            {
                var lyricResult = timeTagComponent.Encode(line);
                //Copy result
                sb.AppendLine(lyricResult);
            }

            // Change new line
            sb.AppendLine("");

            var atTags = new List<AtTag>();

            // Convert ruby into ast tag
            var rubyTagComponent = new RubyTagParserComponent(lyric);
            foreach (var rubyTag in lyric.RubyTags)
            {
                var rubyResult = rubyTagComponent.Encode(rubyTag);
                atTags.Add(rubyResult);
            }

            // Convert at tag into string
            var atTagComponent = new AtTagParserComponent();
            foreach (var atTag in atTags)
            {
                var atTagResult = atTagComponent.Encode(atTag);
                //Copy result
                sb.AppendLine(atTagResult);
            }

            //return result
            return sb.ToString();
        }
    }
}
