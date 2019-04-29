using System.Collections.Generic;
using System.Data;
using System.Linq;
using Resources.Scripts.InGame;
using UnityEngine;
using UnityEngine.UI;
using Resources.Scripts.Utils;

namespace Resources.Scripts.InGame
{
    public class Board : MonoBehaviour
    {
        private Tiles _tiles;

        private void Awake()
        {
            BoardUtils.board = this;
            _tiles = GetComponentInChildren<Tiles>();
        }

        private void Start()
        {
            _tiles.CreateTiles(BoardUtils.width, BoardUtils.height);
            _tiles.Triangulate(true);
        }
	
        public void UpdateTile(Vector3 position, Color color, HexTile.TileHeight height, bool add, bool remove) {
            position = transform.InverseTransformPoint(position);
            HexPoint point = HexPoint.FromPosition(position);

            if (add && !_tiles.ContainsKey(point))
            {
                int x = point.X + point.Z / 2;
                int y = point.Z;
                
                Vector3 newPos;
                newPos.x = (x + (y & 1) / 2f) * (BoardUtils.innerRadius * 2f);
                newPos.y = y * BoardUtils.outerRadius * 1.5f;
                newPos.z = 0f;
                
                HexTile tile = new HexTile(point, newPos, color, height);
                _tiles.Add(point.Z, point, tile);
            }
            else if (remove && _tiles.ContainsKey(point))
            {
                _tiles.Remove(point);
            }
            else if (_tiles.ContainsKey(point))
            {
                HexTile hexTile = _tiles.Get(point);
                hexTile.SetColor(color);
                hexTile.SetHeight(height);
            }

            _tiles.Triangulate();
        }

        public void InitializeTiles()
        {
            _tiles.Initialize();
            _tiles.CreateTiles(BoardUtils.width, BoardUtils.height);
            _tiles.Triangulate();
        }

        public HexTile GetTile(HexPoint point)
        {
            return _tiles.Get(point);
        }
        
        public List<HexPoint> GetHexPoints()
        {
            return _tiles.Keys();
        }
        
        public bool ContainsPoint(HexPoint hexPoint)
        {
            return _tiles.ContainsKey(hexPoint);
        } 
    }
}