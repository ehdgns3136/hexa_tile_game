
using System;
using Resources.Scripts;
using UnityEngine;

public class HexPoint : IEquatable<HexPoint>
{
    public int X { get; private set; }
    
    public int Y { get; private set; }

    public int Z { get; private set; }

    public HexPoint (int x, int z) {
        X = x;
        Y = -x - z;
        Z = z;
    }
    
    public HexPoint (int x, int y, int z) {
        X = x;
        Y = y;
        Z = z;
    }
    
    public static HexPoint FromOffsetPoint(int x, int y) {
        return new HexPoint(x - y/2, y);
    }
    
    public bool Equals(HexPoint hexPoint)
    {
        if (hexPoint == null) return false;
        return X == hexPoint.X && Y == hexPoint.Y && Z == hexPoint.Z;
    }

    public override int GetHashCode()
    {
        string hash = X + "|" + Y + "|" + Z;
        return hash.GetHashCode();
    }
    
    public static HexPoint operator+ (HexPoint p1, HexPoint p2) {
        return new HexPoint(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
    }
        
    public static HexPoint operator* (HexPoint p, int a) {
        return new HexPoint(p.X * a, p.Y * a, p.Z * a);
    }
    
    public override string ToString () {
        return "(" + X.ToString() + ", " + Y.ToString() + ", " + Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines ()
    {
        return X.ToString() + "\n" + Y.ToString() + "\n" + Z.ToString();
    }
    
    public static HexPoint FromPosition (Vector3 position) {
        float x = position.x / (BoardUtils.innerRadius * 2f);
        float y = -x;
        
        float offset = position.y / (BoardUtils.outerRadius * 3f);
        x -= offset;
        y -= offset;
        
        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x -y);
        
        if (iX + iY + iZ != 0) {
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x -y - iZ);

            if (dX > dY && dX > dZ) {
                iX = -iY - iZ;
            }
            else if (dZ > dY) {
                iZ = -iX - iY;
            }
        }
        
        return new HexPoint(iX, iZ);
    }
}