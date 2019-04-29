using System.Collections.Generic;
using Resources.Scripts.Utils;
using UnityEngine;

namespace Resources.Scripts.InGame
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class TilesMesh : MonoBehaviour
    {
        Mesh _mesh;
        List<Vector3> _vertices;
        List<int> _triangles;
        List<Color> _colors;

        public BoxCollider2D boxCollider;
        
        private void Awake ()
        {
            GetComponent<MeshFilter>().mesh = _mesh = new Mesh();
    
            _mesh.name = "Hex Mesh";
            _vertices = new List<Vector3>();
            _triangles = new List<int>();
            _colors = new List<Color>();
        }
    
        public void Triangulate (List<HexTile> tiles, bool first)
        {
            _mesh.Clear();
            _vertices.Clear();
            _triangles.Clear();
            _colors.Clear();
            for (int i = 0; i < tiles.Count; i++) {
                Triangulate(tiles[i]);
            }
            _mesh.vertices = _vertices.ToArray();
            _mesh.triangles = _triangles.ToArray();
            _mesh.colors = _colors.ToArray();
            _mesh.RecalculateNormals();

            if (first)
            {
                Vector3 boundSize = _mesh.bounds.size;
                boxCollider.size = boundSize;
                boxCollider.offset = boundSize / 2 - (new Vector3(BoardUtils.innerRadius, BoardUtils.outerRadius) + BoardUtils.heightOffset);
            }
        }
            
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
    
            // side quad
            AddQuad(
                center + BoardUtils.corners[2],
                center + BoardUtils.corners[3],
                center + BoardUtils.corners[2] + BoardUtils.heightOffset * -(height + 1),
                center + BoardUtils.corners[3] + BoardUtils.heightOffset * -(height + 1)
            );
            AddQuadColor(Color.Lerp(Color.black, hexTile.GetColor(), 0.8f));
            
            AddQuad(
                center + BoardUtils.corners[3],
                center + BoardUtils.corners[4],
                center + BoardUtils.corners[3] + BoardUtils.heightOffset * -(height + 1),
                center + BoardUtils.corners[4] + BoardUtils.heightOffset * -(height + 1)
            );
            AddQuadColor(Color.Lerp(Color.black, hexTile.GetColor(), 0.65f));
            
            
            // lower ground
            AddQuad(
                center + BoardUtils.corners[2] + BoardUtils.heightOffset * -(height + 1),
                center + BoardUtils.corners[3] + BoardUtils.heightOffset * -(height + 1),
                center + BoardUtils.corners[2] + BoardUtils.heightOffset * -(height + 3),
                center + BoardUtils.corners[3] + BoardUtils.heightOffset * -(height + 3)
            );
            AddQuadColor(new Color(0.5f, 0.5f, 0.5f));
            
            AddQuad(
                center + BoardUtils.corners[3] + BoardUtils.heightOffset * -(height + 1),
                center + BoardUtils.corners[4] + BoardUtils.heightOffset * -(height + 1),
                center + BoardUtils.corners[3] + BoardUtils.heightOffset * -(height + 3),
                center + BoardUtils.corners[4] + BoardUtils.heightOffset * -(height + 3)
            );
            AddQuadColor(new Color(0.5f, 0.5f, 0.5f));
        }
        
        private void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3)
        {
            int vertexIndex = _vertices.Count;
            _vertices.Add(v1);
            _vertices.Add(v2);
            _vertices.Add(v3);
            _triangles.Add(vertexIndex);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 2);
        }
        
        private void AddTriangleColor (Color color)
        {
            _colors.Add(color);
            _colors.Add(color);
            _colors.Add(color);
        }
    
        private void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4)
        {
            int vertexIndex = _vertices.Count;
            _vertices.Add(v1);
            _vertices.Add(v2);
            _vertices.Add(v3);
            _vertices.Add(v4);
            _triangles.Add(vertexIndex);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 2);
            _triangles.Add(vertexIndex + 1);
            _triangles.Add(vertexIndex + 2);
            _triangles.Add(vertexIndex + 3);
        }
    
        private void AddQuadColor(Color color)
        {
            _colors.Add(color);
            _colors.Add(color);
            _colors.Add(color);
            _colors.Add(color);
        }
    }
}