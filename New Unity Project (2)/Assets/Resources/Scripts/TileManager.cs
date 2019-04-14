using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using UnityEngine;

public class TileManager : MonoSingleton<TileManager>
{
    private Dictionary<CubePoint, Tile> _cubeTiles;
    private Dictionary<HexaPoint, Tile> _hexaTiles;

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

    private CubePoint _currentPoint;

    private void Initialize()
    {
        _cubeTiles = new Dictionary<CubePoint, Tile>();
        _hexaTiles = new Dictionary<HexaPoint, Tile>();
        _currentPoint = null;
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
        tile.HexaPoint = hexaPoint;
        tile.CubePoint = cubePoint;
        _hexaTiles.Add(hexaPoint, tile);
        _cubeTiles.Add(cubePoint, tile);
        tile.Length = length;
        tile.RedTile = tileObj.transform.Find("Select").gameObject;
    }

    public Tile GetTile(CubePoint point, int direction)
    {
        CubePoint targetPoint = point + directionOffsets[direction];
        if (!_cubeTiles.ContainsKey(targetPoint))
            return null;

        return _cubeTiles[targetPoint];
    }

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

    public void HighlightNeighbor(CubePoint point)
    {
        if (point == _currentPoint)
            return;
        
        List<Tile> neighbors;
        if (_currentPoint != null)
        {
            neighbors = GetNeighbors(_currentPoint);
            for (int i = 0; i < neighbors.Count; i++)
            {
                neighbors[i].RedTile.SetActive(false);
            }
        }

        neighbors = GetNeighbors(point);
        for (int i = 0; i < neighbors.Count; i++)
        {
            neighbors[i].RedTile.SetActive(true);
        }

        _currentPoint = point;
    }
}
