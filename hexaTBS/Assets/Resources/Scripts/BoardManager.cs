using System.Collections.Generic;
using UnityEngine;

namespace Resources.Scripts
{
    public class BoardManager :MonoWeakSingleton<BoardManager>
    {
        private static readonly float startXOffset = -10f;
        private static readonly float startYOffset = 8f;

        private static GameObject canvasObj;
    
        private static readonly CubePoint[] directionOffsets =
        {
            new CubePoint(1, -1, 0),
            new CubePoint(1, 0, -1),
            new CubePoint(0, 1, -1),
            new CubePoint(-1, 1, 0),
            new CubePoint(-1, 0, 1),
            new CubePoint(0, -1, 1)
        };
    
        private Board board;
        
        private CubePoint currentPoint;
        private List<CubePoint> currentHighlightPoints;

        private void Initialize()
        {
            currentPoint = null;
            board = new Board();
            board.Initialize(startXOffset, startYOffset, canvasObj);
            
        }
        
        protected void Start()
        {
            Initialize();
            
            canvasObj = GameObject.Find("Canvas");
        }
    
        public Tile GetTileAtDirection(CubePoint point, int direction)
        {
            CubePoint targetPoint = point + directionOffsets[direction];
            return board.GetTile(targetPoint) != null ? board.GetTile(targetPoint) : null;
        }
    
        public List<CubePoint> GetPointsAtRange(CubePoint point, int range)
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

        public List<CubePoint> GetPointsWithinRadius(CubePoint point, int radius)
        {
            List<CubePoint> points = new List<CubePoint>();
            for (int i = 1; i <= radius; i++)
            {
                points.InsertRange(points.Count == 0 ? 0 : points.Count - 1, GetPointsAtRange(point, i));
            }

            return points;
        }

        public void HighlightTiles(List<CubePoint> points, bool active)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (board.GetTile(points[i]) != null)
                {
                    board.GetTile(points[i]).HighlightTile.SetActive(active);
                }
            }
        }

        public void SelectNewPoint(CubePoint point)
        {
            if (point == currentPoint)
                return;

            if (currentHighlightPoints != null && currentHighlightPoints.Count > 0)
                HighlightTiles(currentHighlightPoints, false);

            currentPoint = point;
            currentHighlightPoints = board.GetTile(currentPoint).HighlightPoints;
            HighlightTiles(currentHighlightPoints, true);
        }
    }
}