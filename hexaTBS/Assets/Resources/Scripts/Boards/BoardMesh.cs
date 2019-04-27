using System;
using UnityEngine;
using System.Collections.Generic;
using Resources.Scripts.Utils;
using Resources.Scripts;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BoardMesh : MonoBehaviour
{
    Mesh hexMesh;
    List<Vector3> vertices;
    List<int> triangles;
    List<Color> colors;
    
    public MeshCollider meshCollider;

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

//        Mesh colliderMesh = new Mesh();
//        
//        List<Vector3> vertices2 = new List<Vector3>();
//        List<int> triangles2 = new List<int>();
//
//        Vector3 boundSize = hexMesh.bounds.size;
//        
//        Vector3 v1 = new Vector3(-boundSize.x, boundSize.y, 0);
//        Vector3 v2 = new Vector3(boundSize.x, boundSize.y, 0);
//        Vector3 v3 = new Vector3(-boundSize.x, -boundSize.y, 0);
//        Vector3 v4 = new Vector3(boundSize.x, -boundSize.y);
//        
//        int vertexIndex = vertices2.Count;
//        vertices2.Add(v1);
//        vertices2.Add(v2);
//        vertices2.Add(v3);
//        vertices2.Add(v4);
//        triangles2.Add(vertexIndex);
//        triangles2.Add(vertexIndex + 1);
//        triangles2.Add(vertexIndex + 2);
//        triangles2.Add(vertexIndex + 1);
//        triangles2.Add(vertexIndex + 2);
//        triangles2.Add(vertexIndex + 3);
//
//        colliderMesh.vertices = vertices2.ToArray();
//        colliderMesh.triangles = triangles2.ToArray();
//        colliderMesh.RecalculateNormals();
//
//        meshCollider.sharedMesh = colliderMesh;

    }
    
//    Mesh SpriteToMesh(Sprite sprite)
//    {
//        Mesh mesh = new Mesh();
//        mesh.vertices = Array.ConvertAll(sprite.vertices, i => (Vector3)i);
//        mesh.uv = sprite.uv;
//        mesh.triangles = Array.ConvertAll(sprite.triangles, i => (int)i);
// 
//        return mesh;
//    }
    
    private void Triangulate (HexTile hexTile)
    {
        Vector3 center = hexTile.GetPosition();
        
        int height = hexTile.GetHeight();

        center += BoardUtils.heightOffset * (height - 1);

        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
                center,
                center + BoardUtils.corners[i],
                center + BoardUtils.corners[i + 1]
            );
            AddTriangleColor(hexTile.GetColor());
        }

        AddQuad(
            center + BoardUtils.corners[2],
            center + BoardUtils.corners[3],
            center + BoardUtils.corners[2] + BoardUtils.heightOffset * -(height + 1),
            center + BoardUtils.corners[3] + BoardUtils.heightOffset * -(height + 1)
        );
        AddQuadColor(Color.Lerp(Color.black, hexTile.GetColor(), 0.9f));
        
//        AddQuad(
//            center + BoardUtils.corners[2] + BoardUtils.heightOffset * -(height + 1),
//            center + BoardUtils.corners[3] + BoardUtils.heightOffset * -(height + 1),
//            center + BoardUtils.corners[2] + BoardUtils.heightOffset * -(height + 3),
//            center + BoardUtils.corners[3] + BoardUtils.heightOffset * -(height + 3)
//        );
//        AddQuadColor(new Color(0.8f, 0.8f, 0.8f));
        
        AddQuad(
            center + BoardUtils.corners[3],
            center + BoardUtils.corners[4],
            center + BoardUtils.corners[3] + BoardUtils.heightOffset * -(height + 1),
            center + BoardUtils.corners[4] + BoardUtils.heightOffset * -(height + 1)
        );
        AddQuadColor(Color.Lerp(Color.black, hexTile.GetColor(), 0.75f));
        
//        AddQuad(
//            center + BoardUtils.corners[3] + BoardUtils.heightOffset * -(height + 1),
//            center + BoardUtils.corners[4] + BoardUtils.heightOffset * -(height + 1),
//            center + BoardUtils.corners[3] + BoardUtils.heightOffset * -(height + 3),
//            center + BoardUtils.corners[4] + BoardUtils.heightOffset * -(height + 3)
//        );
//        AddQuadColor(new Color(0.8f, 0.8f, 0.8f));
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