using System;

namespace Resources.Scripts
{
    public class CubePoint : IEquatable<CubePoint>
    {
        private readonly int x;
        private readonly int y;
        private readonly int z;
        
        public bool Equals(CubePoint point)
        {
            if (point == null) return false;
            return x == point.GetX() && y == point.GetY() && z == point.GetZ();
        }

        public override int GetHashCode()
        {
            string hash = x + "|" + y + "|" + z;
            return hash.GetHashCode();
        }

        public CubePoint(int row, int column)
        {
            x = column - (row - (row & 1)) / 2;
            y = row;
            z = -x - y;
        }

        public CubePoint(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public static CubePoint operator+ (CubePoint p1, CubePoint p2) {
            return new CubePoint(p1.GetX() + p2.GetX(), p1.GetY() + p2.GetY(), p1.GetZ() + p2.GetZ());
        }
        
        public static CubePoint operator* (CubePoint p, int a) {
            return new CubePoint(p.GetX() * a, p.GetY() * a, p.GetZ() * a);
        }

        public int GetX()
        {
            return x;
        }
        
        public int GetY()
        {
            return y;
        }
        
        public int GetZ()
        {
            return z;
        }
    }
}