using LyricMaker.Model.Tags;

namespace LyricMaker.Parser.Component
{
    /// <summary>
    /// Components for process <see cref="AtTag"/>
    /// </summary>
    public class AtTagParserComponent : ParserComponent<AtTag?>
    {
        public override AtTag? Decode(string line)
        {
            if (!line.StartsWith("@"))
                return null;

            // 三文字以下(最短構成"@n=") 行頭@ではない 行頭@@は除外
            if (line.Length < 3 || line[0] != '@' || line[1] == '@')
                return new AtTag("", "");

            var equal = line.IndexOf('=', 1);
            if (equal == -1)
                return new AtTag("", "");

            var end = equal;
            while (line[end - 1] == ' ' || line[end - 1] == '　' || line[end - 1] == '\t')
                end--;

            var name = line.Substring(1, end - 1);
            end = line.IndexOfAny(new[] { '\r', '\n' });
            if (end == -1)
                end = line.Length;

            var value = equal + 1 < end ? line.Substring(equal + 1, end - (equal + 1)) : "";

            return new AtTag(name, value);
        }

        public override string Encode(AtTag? component)
        {
            return component == null ? null : $"@{component.Value.Name}={component.Value.Value}";
        }
    }
}
