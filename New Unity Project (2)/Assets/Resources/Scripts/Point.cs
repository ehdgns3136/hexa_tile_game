using System;

namespace Resources.Scripts
{
    public class Point : IEquatable<Point>
    {
        private int _row;
        private int _col;
        private int _x;
        private int _y;
        private int _z;

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

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int Z
        {
            get { return _z; }
            set { _z = value; }
        }

        public bool Equals(Point point)
        {
            if (point == null) return false;
            return _x == point.X && _y == point.Y && _z == point.Z;
        }

        public override int GetHashCode()
        {
            string hash = Row + "|" + Col;
            return hash.GetHashCode();
        }

        public Point(int row, int col)
        {
            int x = col - (row - (row & 1)) / 2;
            int z = row;
            int y = -x - z;

            X = x;
            Y = y;
            Z = z;
            Row = row;
            Col = col;
        }
    }
}