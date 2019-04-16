using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Resources.Scripts
{
    public class TileManager : MonoWeakSingleton<TileManager>
    {
        private Dictionary<CubePoint, Tile> cubeTiles;
        private Dictionary<HexaPoint, Tile> hexaTiles;
    
        private static readonly float startXOffset = -10f;
        private static readonly float startYOffset = 8f;
    
        private static readonly CubePoint[] directionOffsets =
        {
            new CubePoint(1, -1, 0),
            new CubePoint(1, 0, -1),
            new CubePoint(0, 1, -1),
            new CubePoint(-1, 1, 0),
            new CubePoint(-1, 0, 1),
            new CubePoint(0, -1, 1)
        };
    
        private CubePoint currentPoint;
        private List<CubePoint> currentHighlightPoints;

        private void Initialize()
        {
            cubeTiles = new Dictionary<CubePoint, Tile>();
            hexaTiles = new Dictionary<HexaPoint, Tile>();
            currentPoint = null;
        }
        
        protected void Start()
        {
            Initialize();
            
            GameObject canvasObj = GameObject.Find("Canvas");
            
            float length = 1f;
            float xOffset = length * Mathf.Sqrt(3);
            float yOffset = length * 3 / 2;
            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    float xPos = xOffset * j + xOffset / 2 * (i & 1);
                    float yPos = - yOffset * i;
                    
                    LoadTile(startXOffset + xPos, startYOffset + yPos, i, j, length, canvasObj);
                }
            }
            
            foreach (var cubePoint in cubeTiles.Keys)
            {
                if (GetTile(cubePoint) != null)
                    GetTile(cubePoint).SetHighlightPoints(GetNeighborsPoint(cubePoint));
            }
        }
    
        private void LoadTile(float x, float y, int row, int col, float length, GameObject canvas)
        {
            Object tilePrefab = UnityEngine.Resources.Load("Prefabs/Tile"); // note: not .prefab!
            GameObject tileObj = (GameObject) GameObject.Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
            tileObj.transform.position = new Vector3(x, y);
            tileObj.transform.SetParent(canvas.transform);
            
            HexaPoint hexaPoint = new HexaPoint(row, col);
            CubePoint cubePoint = new CubePoint(row, col);
            
            var tile = tileObj.GetComponent<Tile>();
            tile.Initialize(hexaPoint, cubePoint, length, tileObj.transform.Find("Select").gameObject);
            hexaTiles.Add(hexaPoint, tile);
            cubeTiles.Add(cubePoint, tile);
        }
    
        public Tile GetTile(CubePoint point, int direction)
        {
            CubePoint targetPoint = point + directionOffsets[direction];
            return GetTile(targetPoint) != null ? GetTile(targetPoint) : null;
        }

        public Tile GetTile(CubePoint point)
        {
            if (cubeTiles.ContainsKey(point))
                return cubeTiles[point];
            return null;
        }
    
        
        // TODO : Experimental logic, need to remove after a while from here
        public List<Tile> GetNeighbors(CubePoint point)
        {
            List<Tile> neighbors = new List<Tile>();
            for (int i = 0; i < 6; i++)
            {
                Tile neighbor = GetTile(point, i);
                if (neighbor != null)
                    neighbors.Add(neighbor);
            }
    
            return neighbors;
        }
    
        public List<CubePoint> GetNeighborsPoint(CubePoint point)
        {
            List<CubePoint> neighborsPoint = new List<CubePoint>();
            for (int i = 0; i < 6; i++)
            {
                CubePoint targetPoint = point + directionOffsets[i];
                if (!cubeTiles.ContainsKey(targetPoint))
                    continue;
                
                neighborsPoint.Add(targetPoint);
            }
    
            return neighborsPoint;
        }
        // TODO : To here

        public void HighlightTiles(List<CubePoint> points, bool activate)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (GetTile(points[i]) != null)
                {
                    GetTile(points[i]).HighlightTile.SetActive(activate);
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
            currentHighlightPoints = GetTile(currentPoint).HighlightPoints;
            HighlightTiles(currentHighlightPoints, true);
        }
    }
}

