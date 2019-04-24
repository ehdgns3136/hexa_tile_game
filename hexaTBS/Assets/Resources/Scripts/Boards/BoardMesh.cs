using UnityEngine;
using System.Collections.Generic;
using Resources.Scripts;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BoardMesh : MonoBehaviour
{
    Mesh hexMesh;
    List<Vector3> vertices;
    List<int> triangles;
    List<Color> colors;
    
    MeshCollider meshCollider;

    private void Awake ()
    {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();

        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        triangles = new List<int>();
        colors = new List<Color>();
    }
    
    public void Triangulate (List<HexTile> tiles)
    {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        colors.Clear();
        for (int i = 0; i < tiles.Count; i++) {
            Triangulate(tiles[i]);
        }
        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.colors = colors.ToArray();
        hexMesh.RecalculateNormals();
        
        meshCollider.sharedMesh = hexMesh;
    }
	
    private void Triangulate (HexTile hexTile)
    {
        Vector3 center = hexTile.GetPosition();
        HexPoint hexPoint = hexTile.GetHexPoint();

        if (hexTile.GetTileType() == HexTile.TileType.DEFAULT)
        {
            for (int i = 0; i < 6; i++)
            {
                AddTriangle(
                    center,
                    center + BoardUtils.corners[i],
                    center + BoardUtils.corners[i+1]
                );
                AddTriangleColor(hexTile.GetColor());

                HexTile rightDownTile = BoardUtils.GetTileAtDirection(hexPoint, 5);
                if (rightDownTile != null && rightDownTile.GetTileType() == HexTile.TileType.LOW)
                {
                    AddQuad(
                        center + BoardUtils.corners[2],
                        center + BoardUtils.corners[3],
                        center + BoardUtils.corners[2] + BoardUtils.zOffset,
                        center + BoardUtils.corners[3] + BoardUtils.zOffset
                    );
                    AddQuadColor(Color.Lerp(Color.black, hexTile.GetColor(), 0.9f));
                }

                HexTile leftDownTile = BoardUtils.GetTileAtDirection(hexPoint, 4);
                if (leftDownTile != null && leftDownTile.GetTileType() == HexTile.TileType.LOW)
                {
                    AddQuad(
                        center + BoardUtils.corners[3],
                        center + BoardUtils.corners[4],
                        center + BoardUtils.corners[3] + BoardUtils.zOffset,
                        center + BoardUtils.corners[4] + BoardUtils.zOffset
                    );
                    AddQuadColor(Color.Lerp(Color.black, hexTile.GetColor(), 0.8f));
                }
            }
        } 
        else if (hexTile.GetTileType() == HexTile.TileType.LOW)
        {
            for (int i = 0; i < 6; i++)
            {
                if (i == 0 || i == 5)
                {
                    AddTriangle(
                        center + BoardUtils.zOffset,
                        center + BoardUtils.corners[i] + BoardUtils.zOffset,
                        center + BoardUtils.corners[i+1] + BoardUtils.zOffset
                    );
                }
                else if (i == 1 || i == 2)
                {
                    HexTile rightDownTile = BoardUtils.GetTileAtDirection(hexPoint, 5);
                    if (rightDownTile != null && rightDownTile.GetTileType() == HexTile.TileType.DEFAULT)
                    {
                        if (i == 1)
                        {
                            AddTriangle(
                                center + BoardUtils.zOffset,
                                center + BoardUtils.corners[i] + BoardUtils.zOffset,
                                center + BoardUtils.corners[i+1]
                            );
                        }
                        else
                        {
                            AddTriangle(
                                center + BoardUtils.zOffset,
                                center + BoardUtils.corners[i],
                                center + BoardUtils.corners[i+1]
                            );
                        }
                    }
                    else
                    {
                        AddTriangle(
                            center + BoardUtils.zOffset,
                            center + BoardUtils.corners[i] + BoardUtils.zOffset,
                            center + BoardUtils.corners[i+1] + BoardUtils.zOffset
                        );
                    }
                } else if (i == 3 || i == 4)
                {
                    HexTile leftDownTile = BoardUtils.GetTileAtDirection(hexPoint, 4);
                    if (leftDownTile != null && leftDownTile.GetTileType() == HexTile.TileType.DEFAULT)
                    {
                        if (i == 4)
                        {
                            AddTriangle(
                                center + BoardUtils.zOffset,
                                center + BoardUtils.corners[i],
                                center + BoardUtils.corners[i+1] + BoardUtils.zOffset
                            );
                        }
                        else
                        {
                            AddTriangle(
                                center + BoardUtils.zOffset,
                                center + BoardUtils.corners[i],
                                center + BoardUtils.corners[i+1]
                            );
                        }
                    }
                    else
                    {
                        AddTriangle(
                            center + BoardUtils.zOffset,
                            center + BoardUtils.corners[i] + BoardUtils.zOffset,
                            center + BoardUtils.corners[i+1] + BoardUtils.zOffset
                        );
                    }
                }
                
                AddTriangleColor(hexTile.GetColor());
            }
        }
    }
    
    private void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }
    
    private void AddTriangleColor (Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }

    private void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
    {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        vertices.Add(v4);
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
        triangles.Add(vertexIndex + 3);
    }

    private void AddQuadColor(Color color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }
    
}