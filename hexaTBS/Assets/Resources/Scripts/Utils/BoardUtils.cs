using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Resources.Scripts.Utils.DataStructure;
using UnityEngine;

namespace Resources.Scripts
{
    public class BoardUtils
    {
        public static Board board = null;
        public static readonly HexPoint[] directions =
        {
            new HexPoint(1, -1, 0),
            new HexPoint(0, -1, 1),
            new HexPoint(-1, 0, 1),
            new HexPoint(-1, 1, 0),
            new HexPoint(0, 1, -1),
            new HexPoint(1, 0, -1),
        };

        public static readonly Color[] colors =
        {
            new Color(0.784f, 0.874f, 0.317f), 
            new Color(0.501f, 0.654f, 0.486f),
            new Color(0.631f, 0.768f, 0.329f),
            new Color(0.447f, 0.898f, 0.721f),
            new Color(0.360f, 0.545f, 0.819f),
            new Color(0.368f, 0.443f, 0.690f),
            new Color(0.925f, 0.670f, 0.235f),
            new Color(0.956f, 0.780f, 0.247f),
            new Color(0.956f, 0.866f, 0.474f),
        };
        
        public const float outerRadius = 10f;

        public const float innerRadius = outerRadius * 0.866025404f; // sqrt(3)/2

        public static Vector3 zOffset = new Vector3(0, -3f, 0);
	
        public static readonly Vector3[] corners = {
            new Vector3(0f, outerRadius),
            new Vector3(innerRadius, 0.5f * outerRadius),
            new Vector3(innerRadius, -0.5f * outerRadius),
            new Vector3(0f, -outerRadius),
            new Vector3(-innerRadius, -0.5f * outerRadius),
            new Vector3(-innerRadius, 0.5f * outerRadius),
            new Vector3(0f, outerRadius)
        };
        
        public static HexTile GetTileAtDirection(HexPoint point, int direction)
        {
            if (board == null)
                return null;
            
            HexPoint targetPoint = point + directions[direction];
            return board.GetTile(targetPoint) != null ? board.GetTile(targetPoint) : null;
        }
        
        public static List<HexPoint> GetPointsWithinRadius(HexPoint point, int radius)
        {
            if (board == null)
                return null;
            
            List<HexPoint> resultPoints = new List<HexPoint>();
            
            for (int i = 1; i <= radius; i++)
            {
                resultPoints.InsertRange(resultPoints.Count == 0 ? 0 : resultPoints.Count - 1, GetPointsAtRange(point, i));
            }

            return resultPoints;
        }
        
        public static List<HexPoint> GetPointsAtRange(HexPoint point, int range)
        {
            if (board == null)
                return null;
            
            List<HexPoint> resultPoints = new List<HexPoint>();
            HexPoint targetPoint = point + directions[4] * range;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    targetPoint = targetPoint + directions[i];
                    if (!board.ContainsPoint(targetPoint))
                        continue;
                    
                    resultPoints.Add(targetPoint);
                }
            }
    
            return resultPoints;
        }

        public static List<HexPoint> GetReachablePoints(HexTile tile)
        {
            if (board == null)
                return null;

            if (tile.GetUnit() == null)
                return null;
            
            List<HexPoint> resultPoints = new List<HexPoint>();

            Dictionary<HexPoint, int> distances = new Dictionary<HexPoint, int>();
            int maxDistance = 1000;
            List<HexPoint> existPoints = board.GetHexPoints();
            for (int i = 0; i < existPoints.Count; i++)
            {
                distances.Add(existPoints[i], maxDistance);
            }
            
            HexPoint currentPoint = tile.GetHexPoint();
            distances[currentPoint] = 0;
            
            PriorityQueue<HexPoint> toVisitPoints = new PriorityQueue<HexPoint>(distances);
            HashSet<HexPoint> visitedPoints = new HashSet<HexPoint>();
            
            Dijkstra(currentPoint, currentPoint, toVisitPoints, visitedPoints);
            
            
            return resultPoints;
        }

        private static void Dijkstra(HexPoint startPoint, HexPoint currentPoint, PriorityQueue<HexPoint> toVisitPoints, HashSet<HexPoint> visitedPoints)
        {
            if (board == null)
                return;
            
            visitedPoints.Add(currentPoint);

            List<HexPoint> adjacentPoints = GetPointsWithinRadius(currentPoint, 1);
            HexTile startTile = board.GetTile(startPoint);
                
            for (int i = 0; i < adjacentPoints.Count; i++)
            {
                HexPoint adjacentPoint = adjacentPoints[i];

//                if (toVisitPoints.Contains(adjacentPoint))
//                {
//                    
//                } else if (board.ContainsPoint(adjacentPoint) && !visitedPoints.Contains(adjacentPoint))
//                {
//                    int adjacentTileType = (int) board.GetTile(adjacentPoint).GetTileType();
//                    int movableTileType = (int) startTile.GetUnit().GetMovableTileType();
//
//                    if ((movableTileType & adjacentTileType) == adjacentTileType)
//                    { 
//                        toVisitPoints.Enqueue(adjacentPoint);
//                    }
//                }
            }
        }
    }
}