using System.Collections.Generic;

namespace Resources.Scripts
{
    public class BoardUtils
    {
        public static readonly CubePoint[] directionOffsets =
        {
            new CubePoint(1, -1, 0),
            new CubePoint(1, 0, -1),
            new CubePoint(0, 1, -1),
            new CubePoint(-1, 1, 0),
            new CubePoint(-1, 0, 1),
            new CubePoint(0, -1, 1)
        };
        
        public static Tile GetTileAtDirection(Board board, CubePoint point, int direction)
        {
            CubePoint targetPoint = point + directionOffsets[direction];
            return board.GetTile(targetPoint) != null ? board.GetTile(targetPoint) : null;
        }
        
        public static List<CubePoint> GetPointsWithinRadius(Board board, CubePoint point, int radius)
        {
            List<CubePoint> points = new List<CubePoint>();
            for (int i = 1; i <= radius; i++)
            {
                points.InsertRange(points.Count == 0 ? 0 : points.Count - 1, GetPointsAtRange(board, point, i));
            }

            return points;
        }
        
        public static List<CubePoint> GetPointsAtRange(Board board, CubePoint point, int range)
        {
            List<CubePoint> points = new List<CubePoint>();
            CubePoint targetPoint = point + directionOffsets[4] * range;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    targetPoint = targetPoint + directionOffsets[i];
                    if (!board.ContainsPoint(targetPoint))
                        continue;
                    
                    points.Add(targetPoint);
                }
            }
    
            return points;
        }
    }
}