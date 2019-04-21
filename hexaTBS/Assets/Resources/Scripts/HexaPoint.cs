using System;

namespace Resources.Scripts
{
    public class HexaPoint : IEquatable<HexaPoint>
    {
        private readonly int _row;
        private readonly int _column;

        public bool Equals(HexaPoint point)
        {
            if (point == null) return false;
            return _row == point.GetRow() && _column == point.GetColumn();
        }

        public override int GetHashCode()
        {
            string hash = _row + "|" + _column;
            return hash.GetHashCode();
        }

        public HexaPoint(int row, int column)
        {
            _row = row;
            _column = column;
        }

        public int GetRow()
        {
            return _row;
        }

        public int GetColumn()
        {
            return _column;
        }
    }
}