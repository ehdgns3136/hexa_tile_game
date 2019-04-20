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
        private bool empty;
    
        private GameObject highlightTile;
    
        private List<CubePoint> highlightPoints;
        // object unit which on current tile
    
        public float Length
        {
            get { return length; }
            set { length = value; }
        }
    
        public HexaPoint HexaPoint
        {
            get { return hexaPoint; }
            set { hexaPoint = value; }
        }
    
        public CubePoint CubePoint
        {
            get { return cubePoint; }
            set { cubePoint = value; }
        }
    
        public GameObject HighlightTile
        {
            get { return highlightTile; }
            set { highlightTile = value; }
        }

        public List<CubePoint> HighlightPoints
        {
            get { return highlightPoints; }
            set { highlightPoints = value; }
        }

        public bool Empty
        {
            get { return empty; }
            set { empty = value; }
        }

        public void Initialize(HexaPoint hexaPoint, CubePoint cubePoint, float length, GameObject tileObject)
        {
            HexaPoint = hexaPoint;
            CubePoint = cubePoint;
            Length = length;
            HighlightTile = tileObject;
        }
    
        public void OnClick()
        {
            Debug.Log("Row, Col : " + HexaPoint.Row + ", " + HexaPoint.Col);
            Debug.Log("X, Y, Z : " + CubePoint.X + ", " + CubePoint.Y + ", " + CubePoint.Z);

            BoardManager.Instance.SelectNewPoint(cubePoint);

        }

        public void SetHighlightPoints(List<CubePoint> points)
        {
            highlightPoints = points;
        }
    }
}
