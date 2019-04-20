using System;

namespace Resources.Scripts
{
    public class HexaPoint : IEquatable<HexaPoint>
    {
        private readonly int row;
        private readonly int column;

        public bool Equals(HexaPoint point)
        {
            if (point == null) return false;
            return row == point.GetRow() && column == point.GetColumn();
        }

        public override int GetHashCode()
        {
            string hash = row + "|" + column;
            return hash.GetHashCode();
        }

        public HexaPoint(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public int GetRow()
        {
            return row;
        }

        public int GetColumn()
        {
            return column;
        }
    }
}