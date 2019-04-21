using System.Collections.Generic;
using Resources.Scripts.Utils.DataStructure;

namespace Resources.Scripts
{
    public class BoardUtils
    {
        public static Board board = null;
        public static readonly CubePoint[] directionOffsets =
        {
            new CubePoint(1, -1, 0),
            new CubePoint(1, 0, -1),
            new CubePoint(0, 1, -1),
            new CubePoint(-1, 1, 0),
            new CubePoint(-1, 0, 1),
            new CubePoint(0, -1, 1)
        };
        
        public static Tile GetTileAtDirection(CubePoint point, int direction)
        {
            if (board == null)
                return null;
            
            CubePoint targetPoint = point + directionOffsets[direction];
            return board.GetTile(targetPoint) != null ? board.GetTile(targetPoint) : null;
        }
        
        public static List<CubePoint> GetPointsWithinRadius(CubePoint point, int radius)
        {
            if (board == null)
                return null;
            
            List<CubePoint> resultPoints = new List<CubePoint>();
            
            for (int i = 1; i <= radius; i++)
            {
                resultPoints.InsertRange(resultPoints.Count == 0 ? 0 : resultPoints.Count - 1, GetPointsAtRange(point, i));
            }

            return resultPoints;
        }
        
        public static List<CubePoint> GetPointsAtRange(CubePoint point, int range)
        {
            if (board == null)
                return null;
            
            List<CubePoint> resultPoints = new List<CubePoint>();
            CubePoint targetPoint = point + directionOffsets[4] * range;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    targetPoint = targetPoint + directionOffsets[i];
                    if (!board.ContainsPoint(targetPoint))
                        continue;
                    
                    resultPoints.Add(targetPoint);
                }
            }
    
            return resultPoints;
        }

        public static List<CubePoint> GetReachablePoints(Tile tile)
        {
            if (board == null)
                return null;

            if (tile.GetUnit() == null)
                return null;
            
            List<CubePoint> resultPoints = new List<CubePoint>();

            Dictionary<CubePoint, int> distances = new Dictionary<CubePoint, int>();
            int maxDistance = 1000;
            List<CubePoint> existPoints = board.GetCubePoints();
            for (int i = 0; i < existPoints.Count; i++)
            {
                distances.Add(existPoints[i], maxDistance);
            }
            
            CubePoint currentPoint = tile.GetCubePoint();
            PriorityQueue<CubePoint> toVisitPoints = new PriorityQueue<CubePoint>(distances);
            HashSet<CubePoint> visitedPoints = new HashSet<CubePoint>();
            
            Dijkstra(currentPoint, currentPoint, toVisitPoints, visitedPoints);
            
            
            return resultPoints;
        }

        private static void Dijkstra(CubePoint startPoint, CubePoint currentPoint, PriorityQueue<CubePoint> toVisitPoints, HashSet<CubePoint> visitedPoints)
        {
            if (board == null)
                return;
            
            visitedPoints.Add(currentPoint);

            List<CubePoint> adjacentPoints = GetPointsWithinRadius(currentPoint, 1);
            Tile startTile = board.GetTile(startPoint);
                
            for (int i = 0; i < adjacentPoints.Count; i++)
            {
                CubePoint adjacentPoint = adjacentPoints[i];

                
                
                if (board.ContainsPoint(adjacentPoint))
                {
//                    if (toVisitPoints.)
                    int adjacentTileType = (int) board.GetTile(adjacentPoint).GetTileType();
                    int movableTileType = (int) startTile.GetUnit().GetMovableTileType();

                    if ((movableTileType & adjacentTileType) == adjacentTileType)
                    {
                        toVisitPoints.Enqueue(adjacentPoint);
                    }
                }
            }

            
            
        }
    }
}