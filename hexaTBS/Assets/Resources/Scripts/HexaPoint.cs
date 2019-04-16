using System;

namespace Resources.Scripts
{
    public class HexaPoint : IEquatable<HexaPoint>
    {
        private int row;
        private int col;

        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        public int Col
        {
            get { return col; }
            set { col = value; }
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