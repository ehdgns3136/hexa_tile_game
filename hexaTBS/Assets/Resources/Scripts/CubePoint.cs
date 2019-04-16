using System;

namespace Resources.Scripts
{
    public class CubePoint : IEquatable<CubePoint>
    {
        private int x;
        private int y;
        private int z;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int Z
        {
            get { return z; }
            set { z = value; }
        }
        
        public bool Equals(CubePoint point)
        {
            if (point == null) return false;
            return X == point.X && Y == point.Y && Z == point.Z;
        }

        public override int GetHashCode()
        {
            string hash = X + "|" + Y + "|" + Z;
            return hash.GetHashCode();
        }

        public CubePoint(int row, int col)
        {
            X = col - (row - (row & 1)) / 2;
            Y = row;
            Z = -X - Y;
        }

        public CubePoint(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public static CubePoint operator+ (CubePoint p1, CubePoint p2) {
            return new CubePoint(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }
    }
}