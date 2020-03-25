using System;
using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Ja;
using LyricMaker.Model;
using LyricMaker.Model.Tags;

namespace LyricMaker.AutoComplete.Rubies
{
    public class RubyAutoComplete : PositionTagAutoComplete<RubyTag>, IAutoComplete<AutoCompleteStrategy>
    {
        protected override Analyzer Analyzer => Analyzer.NewAnonymous(createComponents: (fieldName, reader) =>
        {
            Tokenizer tokenizer = new JapaneseTokenizer(reader, null, true, JapaneseTokenizerMode.SEARCH);
            return new TokenStreamComponents(tokenizer, new JapaneseReadingFormFilter(tokenizer, false));
        });

        public void Complete(Lyric lyric, AutoCompleteStrategy strategy)
        {
            var rubyTags = new List<RubyTag>();
            for (var i = 0; i < lyric.Lines.Length; i++)
            {
                var line = lyric.Lines[i];
                rubyTags.AddRange(GenerateTagByLine(line.Text, i));
            }

            switch (strategy)
            {
                case AutoCompleteStrategy.ReplaceAll:
                    rubyTags = CleanUpTags(rubyTags, null).ToList();
                    break;
                case AutoCompleteStrategy.ReplaceIfConflict:
                    rubyTags = CleanUpTags(rubyTags, lyric.RubyTags.ToList()).ToList();
                    break;
                case AutoCompleteStrategy.NotReplaceIfConflict:
                    rubyTags = CleanUpTags(lyric.RubyTags.ToList(),rubyTags).ToList();
                    break;
            }

            // Apply rubies
            lyric.RubyTags = rubyTags.ToArray();
        }

        /// <summary>
        /// Remove same ruby if appear many times, even with different time
        /// </summary>
        /// <param name="tags">Old tags.</param>
        /// <param name="newTags">New tags, paste only not conflict.</param>
        /// <returns></returns>
        internal override IList<RubyTag> CleanUpTags(IList<RubyTag> tags,IList<RubyTag> newTags = null)
        {
            var result = new List<RubyTag>();

            // Combine two list
            var combinedTags = new List<RubyTag>();
            combinedTags.AddRange(tags);
            combinedTags.AddRange(newTags ?? throw new ArgumentNullException(nameof(newTags)));

            // Group by parents
            var groupedTags = combinedTags.GroupBy(x => x.Parent);

            // Remove start time and end time while parent and ruby are the same
            foreach (var group in groupedTags)
            {
                // Remove start time and end time if appear many times
                var rubyGroups = group.GroupBy(x => x.Ruby).OrderBy(x => x.Count());

                // Get max size of group
                var maxRubyGroup = rubyGroups.FirstOrDefault();
                var maxRuby = maxRubyGroup.FirstOrDefault();

                // Save max ruby into list without position
                result.Add(new RubyTag
                {
                    Parent = maxRuby.Parent,
                    Ruby = maxRuby.Ruby
                });

                // Add remain ruby into list
                if (rubyGroups.Count() <= 1)
                    continue;

                foreach (var rubyGroup in rubyGroups)
                {
                    if (rubyGroup == maxRubyGroup)
                        continue;

                    result.AddRange(rubyGroup);
                }
            }

            // TODO : Process same parents with different ruby
            // But can be deal with later.

            return result;
        }

        protected override RubyTag GenerateTag(string parentText, string text)
        {
            return new RubyTag
            {
                Parent = parentText,
                Ruby = text
            };
        }
    }
}
