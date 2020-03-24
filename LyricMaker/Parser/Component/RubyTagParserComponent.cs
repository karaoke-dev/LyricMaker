using LyricMaker.Extensions;
using LyricMaker.Model;
using LyricMaker.Model.Common;
using LyricMaker.Model.Tags;
using System;
using System.Linq;

namespace LyricMaker.Parser.Component
{
    public class RubyTagParserComponent : ParserComponent<RubyTag?, AtTag>
    {
        private readonly Lyric _lyric;
        private readonly bool _filterDuplicated;

        public RubyTagParserComponent(Lyric lyric,bool filterDuplicated = true)
        {
            _lyric = lyric;
            _filterDuplicated = filterDuplicated;
        }

        public override RubyTag? Decode(AtTag tag)
        {
            if (!tag.Name.ToLower().StartsWith("ruby"))
                return null;

            var values = tag.Value.Split(',');
            var parent = values[0];
            var ruby = values[1];
            Position? startPosition = null;
            Position? endPosition = null;

            // Filter duplicated if ruby == parent's text
            if (_filterDuplicated && ruby == parent)
                return null;

            // TODO : have a better way to deal timetag in ruby.
            ruby = string.Join("", TimeTagExtension.SeparateKaraokeLine(ruby).Select(x => x.word));

            // Has start time and end time
            if (values.Length >= 3)
                startPosition = FindPositionByTimeTag(values[2]);
            if (values.Length > 3)
                endPosition = FindPositionByTimeTag(values[3]);

            return new RubyTag
            {
                Parent = parent,
                Ruby = ruby,
                StartPosition = startPosition,
                EndPosition = endPosition,
            };

            Position? FindPositionByTimeTag(string timeTagText)
            {
                if (string.IsNullOrEmpty(timeTagText))
                    return null;

                var milliSecond = TimeTagExtension.timetag2millisec(timeTagText);

                for (int i = 0; i < _lyric.Lines.Length; i++)
                {
                    var line = _lyric.Lines[i];
                    for (var j = 0; j < line.TimeTags.Length; j++)
                    {
                        var timeTag = line.TimeTags[j];
                        if (timeTag.Time == milliSecond)
                        {
                            return new Position
                            {
                                Line = i,
                                Index = (int)Math.Ceiling((float)(j - 1) / 2)
                            };
                        }
                    }
                }

                return null;
            }
        }

        public override AtTag Encode(RubyTag? component)
        {
            var rubyTag = component.Value;

            var rubyIndex = Array.IndexOf(_lyric?.RubyTags ?? new [] { rubyTag }, rubyTag);

            var name = "Ruby" + (rubyIndex + 1);
            var value = $"{rubyTag.Parent},{rubyTag.Ruby}";

            if (rubyTag.StartPosition == null && rubyTag.EndPosition == null)
                return new AtTag
                {
                    Name = name,
                    Value = value
                };

            var startTimeTag = GenerateTimeTagByPosition(rubyTag.StartPosition);
            var endTimeTag = GenerateTimeTagByPosition(rubyTag.EndPosition);
            value = value + $",{startTimeTag},{endTimeTag}";

            // TODO : need to implement time range
            return new AtTag
            {
                Name = name,
                Value = value
            };

            string GenerateTimeTagByPosition(Position? position)
            {
                if (position == null)
                    return null;

                // Get position value
                var p = position.Value;

                //Get time
                var time = _lyric?.Lines[p.Line]?.TimeTags[p.Index].Time;

                return time == null ? null : TimeTagExtension.millisec2timetag(time.Value);
            }
        }
    }
}
