using System.Collections.Generic;
using UnityEngine;

namespace Resources.Scripts
{
    public class BoardManager :MonoWeakSingleton<BoardManager>
    {
        private static readonly float startXOffset = -10f;
        private static readonly float startYOffset = 8f;

        private static GameObject canvasObj;
    
        private Board board;
        
        private CubePoint currentPoint;
        private List<CubePoint> currentHighlightPoints;

        protected void Start()
        {
            currentPoint = null;
            canvasObj = GameObject.Find("Canvas");
            
            board = new Board();
            board.Initialize(startXOffset, startYOffset, canvasObj);
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