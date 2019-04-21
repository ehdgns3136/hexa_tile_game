using System.Collections.Generic;
using UnityEngine;

namespace Resources.Scripts
{
    public class BoardManager :MonoWeakSingleton<BoardManager>
    {
        private static readonly float startXOffset = -10f;
        private static readonly float startYOffset = 8f;

        private static GameObject _canvasObj;
    
        private Board _board;
        
        private CubePoint _currentPoint;
        private List<CubePoint> _currentHighlightPoints;

        protected void Start()
        {
            _currentPoint = null;
            _canvasObj = GameObject.Find("Canvas");
            
            _board = new Board();
            _board.Initialize(startXOffset, startYOffset, _canvasObj);

            BoardUtils.board = _board;
        }

        public void UpdateReachablePoints(Tile tile)
        {
            List<CubePoint> reachablePoints = BoardUtils.GetReachablePoints(tile);
            tile.SetReachablePoints(reachablePoints);
        }

//        public void HighlightTiles(List<CubePoint> points, bool active)
//        {
//            for (int i = 0; i < points.Count; i++)
//            {
//                if (board.GetTile(points[i]) != null)
//                {
//                    board.GetTile(points[i]).HighlightTile.SetActive(active);
//                }
//            }
//        }

//        public void SelectNewPoint(CubePoint point)
//        {
//            if (point == currentPoint)
//                return;
//
//            if (currentHighlightPoints != null && currentHighlightPoints.Count > 0)
//                HighlightTiles(currentHighlightPoints, false);
//
//            currentPoint = point;
//            currentHighlightPoints = board.GetTile(currentPoint).HighlightPoints;
//            HighlightTiles(currentHighlightPoints, true);
//        }
    }
}