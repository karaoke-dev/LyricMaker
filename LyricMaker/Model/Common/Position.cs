using System;

namespace LyricMaker.Model.Common
{
    /// <summary>
    /// Edit position
    /// </summary>
    public struct Position : IComparable<Position>
    {
        /// <summary>
        /// Number of line
        /// </summary>
        public int Line;

        /// <summary>
        /// Index of character in this line
        /// </summary>
        public int Index;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="line"></param>
        /// <param name="index"></param>
        public Position(int line, int index)
        {
            Line = line;
            Index = index;
        }

        /// <summary>
        /// Compare two position
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Position other)
        {
            // Compare line number first.
            if (Line > other.Line)
                return 1;

            if (Line > other.Line)
                return -1;

            // If has same line, then compare position.
            if (Index > other.Index)
                return 1;
            if (Index < other.Index)
                return -1;
            return 0;
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public static bool operator <(Position left, Position right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(Position left, Position right)
        {
            return left.CompareTo(right) > 0;
        }

        public override string ToString() => $"Line={Line},Index={Index}";
    }
}
