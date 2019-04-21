using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Resources.Scripts
{
    public class Board
    {
        private float _startXOffset;
        private float _startYOffset;
        
        private Dictionary<CubePoint, Tile> _cubeCoordinates;
        private Dictionary<HexaPoint, Tile> _hexaCoordinates;
        
        private GameObject _canvasObj;
        private Object _tilePrefab;

        public void Initialize(float startXOffset, float startYOffset, GameObject canvasObj)
        {
            _cubeCoordinates = new Dictionary<CubePoint, Tile>();
            _hexaCoordinates = new Dictionary<HexaPoint, Tile>();
            _startXOffset = startXOffset;
            _startYOffset = startYOffset;
            _canvasObj = canvasObj;
            
            LoadTiles();
        }

        private void LoadTiles()
        {
            _tilePrefab = UnityEngine.Resources.Load("Prefabs/Tile"); // note: not .prefab!
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
                    
                    LoadTile(_startXOffset + xPos, _startYOffset + yPos, i, j, length);
                }
            }

//            foreach (var cubePoint in cubeCoordinates.Keys)
//            {
//                if (GetTile(cubePoint) != null)
//                    GetTile(cubePoint).SetHighlightPoints(GetPointsWithinRadius(cubePoint, 2));
//            }
        }
        
        private void LoadTile(float x, float y, int row, int column, float length)
        {   
            GameObject tileObj = (GameObject) GameObject.Instantiate(_tilePrefab, Vector3.zero, Quaternion.identity);
            tileObj.transform.position = new Vector3(x, y);
            tileObj.transform.SetParent(_canvasObj.transform);
            
            HexaPoint hexaPoint = new HexaPoint(row, column);
            CubePoint cubePoint = new CubePoint(row, column);
            
            Tile tile = tileObj.GetComponent<Tile>();
            tile.Initialize(hexaPoint, cubePoint, length);
            LoadUnit(x, y, row, column, tile);
            
            _hexaCoordinates.Add(hexaPoint, tile);
            _cubeCoordinates.Add(cubePoint, tile);
        }

        private void LoadUnit(float x, float y, int row, int column, Tile tile)
        {
            if (row == 1 && column == 1)
            {
                Object unitPrefab = UnityEngine.Resources.Load("Prefabs/Unit"); // note: not .prefab!
                
                GameObject unitObj = (GameObject) GameObject.Instantiate(unitPrefab, Vector3.zero, Quaternion.identity);
                unitObj.transform.position = new Vector3(x, y);
                unitObj.transform.SetParent(_canvasObj.transform);
                
                Unit unit = unitObj.GetComponent<Unit>();
                unit.Initialize(10, 3, 1, 3, 2, Unit.UnitType.HUMAN);
                tile.SetUnit(unit);
            }
        }

        public void AddToCoordinate(CubePoint cubePoint, Tile tile)
        {
            _cubeCoordinates.Add(cubePoint, tile);
        }
        
        public void AddToCoordinate(HexaPoint cubePoint, Tile tile)
        {
            _hexaCoordinates.Add(cubePoint, tile);
        }

        public Tile GetTile(CubePoint cubePoint)
        {
            if (!_cubeCoordinates.ContainsKey(cubePoint))
                return null;
            return _cubeCoordinates[cubePoint];
        }
        
        public Tile GetTile(HexaPoint hexaPoint)
        {
            if (!_hexaCoordinates.ContainsKey(hexaPoint))
                return null;
            
            return _hexaCoordinates[hexaPoint];
        }

        public List<CubePoint> GetCubePoints()
        {
            return _cubeCoordinates.Keys.ToList();
        }

        public List<HexaPoint> GetHexaPoints()
        {
            return _hexaCoordinates.Keys.ToList();
        }

        public bool ContainsPoint(CubePoint cubePoint)
        {
            return _cubeCoordinates.ContainsKey(cubePoint);
        }
        
        public bool ContainsPoint(HexaPoint hexaPoint)
        {
            return _hexaCoordinates.ContainsKey(hexaPoint);
        }
    }
}