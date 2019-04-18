using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
                    if (i % 3 == 0 && (j % 7 == 0 || j % 6 == 0)) continue;
                    
                    float xPos = xOffset * j + xOffset / 2 * (i & 1);
                    float yPos = - yOffset * i;
                    
                    LoadTile(startXOffset + xPos, startYOffset + yPos, i, j, length, canvasObj);
                }
            }
            
            foreach (var cubePoint in cubeTiles.Keys)
            {
                if (GetTile(cubePoint) != null)
                    GetTile(cubePoint).SetHighlightPoints(GetPointsBetweenRange(cubePoint, 2));
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
    
        public List<CubePoint> GetPointsAtRange(CubePoint point, int range)
        {
            List<CubePoint> points = new List<CubePoint>();
            CubePoint targetPoint = point + directionOffsets[4] * range;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < range; j++)
                {
                    targetPoint = targetPoint + directionOffsets[i];
                    if (!cubeTiles.ContainsKey(targetPoint))
                        continue;
                    
                    points.Add(targetPoint);
                }
            }
    
            return points;
        }

        public List<CubePoint> GetPointsBetweenRange(CubePoint point, int range)
        {
            List<CubePoint> points = new List<CubePoint>();
            for (int i = 1; i <= range; i++)
            {
                points.InsertRange(points.Count == 0 ? 0 : points.Count - 1, GetPointsAtRange(point, i));
            }

            return points;
        }

        public void HighlightTiles(List<CubePoint> points, bool active)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (GetTile(points[i]) != null)
                {
                    GetTile(points[i]).HighlightTile.SetActive(active);
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

