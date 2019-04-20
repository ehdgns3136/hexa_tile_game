using System.Collections.Generic;
using UnityEngine;
using Resources.Scripts;

namespace Resources.Scripts
{
    
    public class Tile : MonoBehaviour
    {
        private float length;
        private HexaPoint hexaPoint;
        private CubePoint cubePoint;
    
        private GameObject highlightTile;
    
        private List<CubePoint> highlightPoints;

        private Unit unit;
        private Building building;
        private Resource resource;

        public void Initialize(HexaPoint hexaPoint, CubePoint cubePoint, float length, GameObject tileObject)
        {
            this.hexaPoint = hexaPoint;
            this.cubePoint = cubePoint;
            this.length = length;
            this.highlightTile = tileObject;
        }
    
        public void OnClick()
        {
            Debug.Log("Row, Col : " + hexaPoint.GetRow() + ", " + hexaPoint.GetColumn());
            Debug.Log("X, Y, Z : " + cubePoint.GetX() + ", " + cubePoint.GetY() + ", " + cubePoint.GetZ());

//            BoardManager.Instance.SelectNewPoint(cubePoint);

        }

        public void SetHighlightPoints(List<CubePoint> points)
        {
            highlightPoints = points;
        }
    }
}
