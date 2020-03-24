using System.Linq;
using LyricMaker.Extensions;
using LyricMaker.Model;
using LyricMaker.Model.Tags;
using System.Text;

namespace LyricMaker.Parser.Component
{
    /// <summary>
    /// Components for process <see cref="LyricLine"/>
    /// </summary>
    public class TimeTagParserComponent : ParserComponent<LyricLine>
    {
        public override LyricLine Decode(string t)
        {
            var pairs = TimeTagExtension.SeparateKaraokeLine(t);

            // Combine texts
            var text = pairs.Aggregate("", (current, p) => current + p.word);

            // Create and make sure all check is false
            var timeTags = new TimeTag[text.Length * 2 + 2];
            for (var i = 0; i < timeTags.Length; i++)
            {
                timeTags[i] = new TimeTag(-1, false);
            }

            // TODO : what is ti
            int ti;
            if (pairs[0].word == "")
            {
                timeTags[0] = new TimeTag(pairs[0].millisec, pairs[0].millisec > 0);
                ti = 1;
            }
            else
            {
                timeTags[1] = new TimeTag(pairs[0].millisec, pairs[0].millisec > 0);
                ti = pairs[0].word.Length * 2;
            }

            // TODO : what are this doing
            for (var pi = 1; pi < pairs.Length; pi++)
            {
                if (pairs[pi].word.Length == 0)
                {
                    if (ti % 2 == 0)
                    {
                        timeTags[ti] = new TimeTag(pairs[pi].millisec);
                        ti++;
                    }
                }
                else
                {
                    if (ti % 2 == 0)
                        ti++;
                    timeTags[ti] = new TimeTag(pairs[pi].millisec);
                    ti += pairs[pi].word.Length * 2 - 1;
                }
            }

            // 隣接した同時間タイムタグがあれば、手前（文字後）を無効化
            for (var i = 0; i < timeTags.Length; i += 2)
            {
                if (timeTags[i].Time == timeTags[i + 1].Time && timeTags[i].Check && timeTags[i + 1].Check)
                {
                    var tag = timeTags[i];
                    tag.Check = false;
                    timeTags[i] = tag;
                }
            }

            return new LyricLine
            {
                Text = text,
                TimeTags = timeTags
            };
        }

        public override string Encode(LyricLine line)
        {
            var sb = new StringBuilder();

            //Add head time tag
            sb.Append(HeadTimeTag(line));

            //Get time tag and chat for each char
            for (var i = 0; i < line.Text.Length; i++)
                sb.Append(TimeTaggedChar(line, i));

            //Add tail time tag
            sb.Append(TailTimeTag(line));

            return sb.ToString();
            
            string TimeTaggedChar(LyricLine l, int index)
            {
                var t1 = l.TimeTags[1 + index * 2];
                var t2 = l.TimeTags[1 + index * 2 + 1];
                return TimeTagExtension.millisec2timetag(t1.CheckedTime) +
                        l.Text[index] +
                        TimeTagExtension.millisec2timetag(t2.CheckedTime);
            }
            
            string HeadTimeTag(LyricLine l)
            {
                return TimeTagExtension.millisec2timetag(l.TimeTags[0].CheckedTime);
            }
            
            string TailTimeTag(LyricLine l)
            {
                return TimeTagExtension.millisec2timetag(l.TimeTags[l.TimeTags.Length - 1].CheckedTime);
            }
        }
    }
}
