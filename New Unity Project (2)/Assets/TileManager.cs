using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float size = 0.6f;
        float xSize = size * Mathf.Sqrt(3);
        float ySize = size * 3 / 2;
        
        
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                float xPos = xSize * j + xSize / 2 * (i & 1);
                float yPos = ySize * i;
                
                LoadTile(xPos, yPos);
            }
        }
        
    }

    private void LoadTile(float x, float y)
    {
        Object tilePrefab = Resources.Load("Tile"); // note: not .prefab!
        GameObject tileObj = (GameObject) GameObject.Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
        tileObj.transform.position = new Vector3(x, y);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
