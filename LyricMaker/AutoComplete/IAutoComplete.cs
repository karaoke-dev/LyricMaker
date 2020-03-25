using LyricMaker.Model;

namespace LyricMaker.AutoComplete
{
    public interface IAutoComplete<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lyric"></param>
        /// <param name="setting"></param>
        void Complete(Lyric lyric, T setting);
    }
}