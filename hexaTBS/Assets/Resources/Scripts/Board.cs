using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Resources.Scripts
{
    public class Board
    {
        private float startXOffset;
        private float startYOffset;
        
        private Dictionary<CubePoint, Tile> cubeCoordinates;
        private Dictionary<HexaPoint, Tile> hexaCoordinates;
        
        private GameObject canvasObj;
        private Object tilePrefab;

        public void Initialize(float startXOffset, float startYOffset, GameObject canvasObj)
        {
            cubeCoordinates = new Dictionary<CubePoint, Tile>();
            hexaCoordinates = new Dictionary<HexaPoint, Tile>();
            this.startXOffset = startXOffset;
            this.startYOffset = startYOffset;
            this.canvasObj = canvasObj;
            
            LoadTiles();
        }

        private void LoadTiles()
        {
            tilePrefab = UnityEngine.Resources.Load("Prefabs/Tile"); // note: not .prefab!
            // load from map file later made by kinda map tool
            
            float length = 1f; // one side length of hexagon
            float xOffset = length * Mathf.Sqrt(3);
            float yOffset = length * 3 / 2;
            
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i % 3 == 0 && (j % 7 == 0 || j % 6 == 0)) continue;
                    
                    float xPos = xOffset * j + xOffset / 2 * (i & 1);
                    float yPos = - yOffset * i;
                    
                    LoadTile(startXOffset + xPos, startYOffset + yPos, i, j, length);
                }
            }

//            foreach (var cubePoint in cubeCoordinates.Keys)
//            {
//                if (GetTile(cubePoint) != null)
//                    GetTile(cubePoint).SetHighlightPoints(GetPointsWithinRadius(cubePoint, 2));
//            }
        }
        
        private void LoadTile(float x, float y, int row, int col, float length)
        {   
            GameObject tileObj = (GameObject) GameObject.Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
            tileObj.transform.position = new Vector3(x, y);
            tileObj.transform.SetParent(canvasObj.transform);
            
            HexaPoint hexaPoint = new HexaPoint(row, col);
            CubePoint cubePoint = new CubePoint(row, col);
            
            var tile = tileObj.GetComponent<Tile>();
            tile.Initialize(hexaPoint, cubePoint, length, tileObj.transform.Find("Select").gameObject);
            hexaCoordinates.Add(hexaPoint, tile);
            cubeCoordinates.Add(cubePoint, tile);
        }

        public void AddToCoordinate(CubePoint cubePoint, Tile tile)
        {
            cubeCoordinates.Add(cubePoint, tile);
        }
        
        public void AddToCoordinate(HexaPoint cubePoint, Tile tile)
        {
            hexaCoordinates.Add(cubePoint, tile);
        }

        public Tile GetTile(CubePoint cubePoint)
        {
            if (!cubeCoordinates.ContainsKey(cubePoint))
                return null;
            return cubeCoordinates[cubePoint];
        }
        
        public Tile GetTile(HexaPoint hexaPoint)
        {
            if (!hexaCoordinates.ContainsKey(hexaPoint))
                return null;
            
            return hexaCoordinates[hexaPoint];
        }

        public List<CubePoint> GetCubePoints()
        {
            return cubeCoordinates.Keys.ToList();
        }

        public List<HexaPoint> GetHexaPoints()
        {
            return hexaCoordinates.Keys.ToList();
        }

        public bool ContainsPoint(CubePoint cubePoint)
        {
            return cubeCoordinates.ContainsKey(cubePoint);
        }
        
        public bool ContainsPoint(HexaPoint hexaPoint)
        {
            return hexaCoordinates.ContainsKey(hexaPoint);
        }
    }
}