using LyricMaker.Model;
using LyricMaker.Model.Tags;

namespace LyricMaker.AutoComplete.TimeTags
{
    /// <summary>
    /// Checker that can auto check which <see cref="TimeTag"/> is clickable
    /// </summary>
    public class TimeTagAutoComplete
    {
        /// <summary>
        /// Mark time tag as checked by parameter
        /// </summary>
        /// <param name="lyric"></param>
        /// <param name="setting"></param>
        public void AutoCheck(Lyric lyric, TimeTagAutoCompleteParameter setting)
        {
            foreach (var line in lyric.Lines)
            {
                if (line.Text.Length != 0)
                {
                    // 空行でなければ行頭文字は問答無用でチェック
                    line.TimeTags[1] = new TimeTag(line.TimeTags[1].Time, true);

                    // 行末文字チェック
                    if (setting.CheckLineEnd)
                        line.TimeTags[line.TimeTags.Length - 2] = new TimeTag(line.TimeTags[line.TimeTags.Length - 2].Time, true, setting.CheckLineEndKeyUp);
                }
                else
                {
                    // 空行
                    if (setting.CheckBlankLine)
                        line.TimeTags[0] = new TimeTag(line.TimeTags[0].Time, true);

                    continue;
                }
                for (var i = 1; i < line.Text.Length; i++)
                {
                    var timeTag = line.TimeTags[i * 2 + 1];
                    timeTag.Check = true;
                    var c = line.Text[i];
                    var pc = line.Text[i - 1];
                    if (char.IsWhiteSpace(c) && setting.CheckWhiteSpace)
                    {
                        // 空白文字の連続は無条件で無視
                        if (char.IsWhiteSpace(pc))
                            continue;

                        timeTag.KeyUp = setting.CheckWhiteSpaceKeyUp;
                        if (IsLatin(pc))
                        {
                            if (setting.CheckWhiteSpaceAlphabet)
                                line.TimeTags[i * 2 + 1] = timeTag;
                        }
                        else if (char.IsDigit(pc))
                        {
                            if (setting.CheckWhiteSpaceDigit)
                                line.TimeTags[i * 2 + 1] = timeTag;
                        }
                        else if (IsASCIISymbol(pc))
                        {
                            if (setting.CheckWhiteSpaceAsciiSymbol)
                                line.TimeTags[i * 2 + 1] = timeTag;
                        }
                        else
                            line.TimeTags[i * 2 + 1] = timeTag;
                    }
                    else if (IsLatin(c) || char.IsNumber(c) || IsASCIISymbol(c))
                    {
                        if (char.IsWhiteSpace(pc) || (!IsLatin(pc) && !char.IsNumber(pc) && !IsASCIISymbol(pc)))
                        {
                            line.TimeTags[i * 2 + 1] = timeTag;
                        }
                    }
                    else if (char.IsWhiteSpace(pc))
                        line.TimeTags[i * 2 + 1] = timeTag;
                    else
                    {
                        switch (c)
                        {
                            case 'ゃ':
                            case 'ゅ':
                            case 'ょ':
                            case 'ャ':
                            case 'ュ':
                            case 'ョ':
                            case 'ぁ':
                            case 'ぃ':
                            case 'ぅ':
                            case 'ぇ':
                            case 'ぉ':
                            case 'ァ':
                            case 'ィ':
                            case 'ゥ':
                            case 'ェ':
                            case 'ォ':
                            case 'ー':
                            case '～':
                                break;

                            case 'ん':
                                if (setting.Checkん)
                                    line.TimeTags[i * 2 + 1] = timeTag;
                                break;

                            case 'っ':
                                if (setting.Checkっ)
                                    line.TimeTags[i * 2 + 1] = timeTag;
                                break;

                            default:
                                line.TimeTags[i * 2 + 1] = timeTag;
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check this char is kana
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsKana(char c)
        {
            return (c >= '\u3041' && c <= '\u309F') |  // ひらがなwith゛゜
                   (c >= '\u30A0' && c <= '\u30FF') |  // カタカナwith゠・ー
                   (c >= '\u31F0' && c <= '\u31FF') |  // Katakana Phonetic Extensions
                   (c >= '\uFF65' && c <= '\uFF9F');
        }

        /// <summary>
        /// Check this character is english
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsLatin(char c)
        {
            return c >= 'A' && c <= 'Z' ||
                   c >= 'a' && c <= 'z' ||
                   c >= 'Ａ' && c <= 'Ｚ' ||
                   c >= 'ａ' && c <= 'ｚ';
        }

        /// <summary>
        /// Check this char is symbol
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsASCIISymbol(char c)
        {
            return c >= ' ' && c <= '/' ||
                   c >= ':' && c <= '@' ||
                   c >= '[' && c <= '`' ||
                   c >= '{' && c <= '~';
        }
    }
}