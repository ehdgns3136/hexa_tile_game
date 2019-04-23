using System.Collections.Generic;
using UnityEngine;

namespace Resources.Scripts
{
    public class BoardManager :MonoWeakSingleton<BoardManager>
    {
//        private static GameObject _canvasObj;
    
        private Board _board;
        
        private HexPoint _currentPoint;
        private List<HexPoint> _currentHighlightPoints;

        public void UpdateReachablePoints(HexTile hexTile)
        {
            List<HexPoint> reachablePoints = BoardUtils.GetReachablePoints(hexTile);
            hexTile.SetReachablePoints(reachablePoints);
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