using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts
{
    public class Board : MonoBehaviour
    {
        public readonly int widthCount = 6;
        public readonly int heightCount = 6;
        
        private Dictionary<HexPoint, HexTile> _hexTiles;
        private BoardMesh _mesh;
        
        private HexPoint _currentPoint;
        private List<HexPoint> _currentHighlightPoints;
        
        private Canvas _gridCanvas;
        
        public Text cellLabelPrefab;

        private void Awake()
        {
            _gridCanvas = GetComponentInChildren<Canvas>();
            
            _mesh = GetComponentInChildren<BoardMesh>();
            _hexTiles = new Dictionary<HexPoint, HexTile>();
            CreateTiles();
        }

        private void Start()
        {
            _mesh.Triangulate(_hexTiles.Values.ToList());
        }
        
        void Update () {
            if (Input.GetMouseButton(0)) {
                HandleInput();
            }
        }

        void HandleInput () {
            Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(inputRay, out hit)) {
                TouchCell(hit.point);
            }
        }
	
        void TouchCell (Vector3 position) {
            position = transform.InverseTransformPoint(position);
            HexPoint hexPoint = HexPoint.FromPosition(position);
            
            HexTile hexTile = _hexTiles[hexPoint];
            hexTile.SetColor(Color.black);
		    _mesh.Triangulate(_hexTiles.Values.ToList());	
		
            Debug.Log("touched at " + hexPoint.ToString());
        }

        private void CreateTiles()
        {
            for (int y = 0; y < heightCount; y++)
            {
                for (int x = 0; x < widthCount; x++)
                {
                    CreateTile(x, y);
                }
            }
        }

        private void CreateTile(int x, int y)
        {
            Vector3 position;
            position.x = (x + (y & 1) / 2f) * (BoardUtils.innerRadius * 2f);
            position.y = y * BoardUtils.outerRadius * 1.5f;
            position.z = 0f;

            HexPoint hexPoint = HexPoint.FromOffsetPoint(x, y);
            HexTile hexTile = new HexTile(hexPoint, position, Color.red);
            _hexTiles.Add(hexPoint, hexTile);
		
            Text label = Instantiate<Text>(cellLabelPrefab);
            label.rectTransform.SetParent(_gridCanvas.transform, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
            label.text = hexPoint.ToStringOnSeparateLines();
            label.color = Color.black;
        }

        public void AddToTiles(HexPoint hexPoint, HexTile hexTile)
        {
            _hexTiles.Add(hexPoint, hexTile);
        }

        public HexTile GetTile(HexPoint hexPoint)
        {
            if (!_hexTiles.ContainsKey(hexPoint))
                return null;
            return _hexTiles[hexPoint];
        }
        
        public List<HexPoint> GetHexPoints()
        {
            return _hexTiles.Keys.ToList();
        }
        
        public bool ContainsPoint(HexPoint hexPoint)
        {
            return _hexTiles.ContainsKey(hexPoint);
        }
        
        private void LoadTiles()
        {
//            _tilePrefab = UnityEngine.Resources.Load("Prefabs/Tile"); // note: not .prefab!
            // load from map file later made by kinda map tool
            
//            float length = 1f; // one side length of hexagon
//            float xOffset = length * Mathf.Sqrt(3);
//            float yOffset = length * 3 / 2;
//            
//            for (int i = 0; i < 10; i++)
//            {
//                for (int j = 0; j < 10; j++)
//                {
//                    if (i % 3 == 0 && (j % 7 == 0 || j % 6 == 0)) continue;
//                    
//                    float xPos = xOffset * j + xOffset / 2 * (i & 1);
//                    float yPos = - yOffset * i;
//                    
//                    LoadTile(_startXOffset + xPos, _startYOffset + yPos, i, j, length);
//                }
//            }
//
////            foreach (var cubePoint in cubeCoordinates.Keys)
////            {
////                if (GetTile(cubePoint) != null)
////                    GetTile(cubePoint).SetHighlightPoints(GetPointsWithinRadius(cubePoint, 2));
////            }
        }
//        
//        private void LoadTile(float x, float y, int row, int column, float length)
//        {   
//            GameObject tileObj = (GameObject) GameObject.Instantiate(_tilePrefab, Vector3.zero, Quaternion.identity);
//            tileObj.transform.position = new Vector3(x, y);
//            tileObj.transform.SetParent(_canvasObj.transform);
//            
//            HexaPoint hexaPoint = new HexaPoint(row, column);
//            CubePoint cubePoint = new CubePoint(row, column);
//            
//            Tile tile = tileObj.GetComponent<Tile>();
//            tile.Initialize(hexaPoint, cubePoint, length);
//            LoadUnit(x, y, row, column, tile);
//            
//            _hexaCoordinates.Add(hexaPoint, tile);
//            _cubeCoordinates.Add(cubePoint, tile);
//        }
//
//        private void LoadUnit(float x, float y, int row, int column, Tile tile)
//        {
//            if (row == 1 && column == 1)
//            {
//                Object unitPrefab = UnityEngine.Resources.Load("Prefabs/Unit"); // note: not .prefab!
//                
//                GameObject unitObj = (GameObject) GameObject.Instantiate(unitPrefab, Vector3.zero, Quaternion.identity);
//                unitObj.transform.position = new Vector3(x, y);
//                unitObj.transform.SetParent(_canvasObj.transform);
//                
//                Unit unit = unitObj.GetComponent<Unit>();
//                unit.Initialize(10, 3, 1, 3, 2, Unit.UnitType.HUMAN);
//                tile.SetUnit(unit);
//            }
//        }
    }
}