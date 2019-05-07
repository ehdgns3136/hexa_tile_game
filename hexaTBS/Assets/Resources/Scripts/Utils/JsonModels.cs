using Resources.Scripts.InGame;

namespace Resources.Scripts.Utils
{
    public class ColorJson
    {
        public float r;
        public float g;
        public float b;
    }

    public class PointJson
    {
        public float x;
        public float y;
        public float z;
    }

    public class HexPointJson
    {
        public int x;
        public int y;
        public int z;
    }

    public class TileHeightJson
    {
        public HexTile.TileHeight height;
    }

    public class HexTileJson
    {
        public HexPointJson hexPoint;
        public PointJson point;
        public ColorJson color;
        public TileHeightJson height;
    }
}