using System;

namespace Resources.Scripts
{
    public class HexaPoint : IEquatable<HexaPoint>
    {
        private int _row;
        private int _col;

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

        public int Col
        {
            get { return _col; }
            set { _col = value; }
        }

        public bool Equals(HexaPoint point)
        {
            if (point == null) return false;
            return Row == point.Row && Col == point.Col;
        }

        public override int GetHashCode()
        {
            string hash = Row + "|" + Col;
            return hash.GetHashCode();
        }

        public HexaPoint(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}