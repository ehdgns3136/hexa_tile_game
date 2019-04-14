using System.Collections;
using System.Collections.Generic;
using Resources.Scripts;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private Dictionary<Point, Tile> _tiles;

    private float startXOffset = -10f;
    private float startYOffset = 8f;
    
    // Start is called before the first frame update
    void Start()
    {
        _tiles = new Dictionary<Point, Tile>();
        
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

        Point point = new Point(row, col);
        var tile = tileObj.GetComponent<Tile>();
        tile.Point = point;
        tile.Length = length;
        _tiles.Add(point, tile);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }    
}
