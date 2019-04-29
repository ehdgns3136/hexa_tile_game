using System.Collections.Generic;
using System.Linq;
using Resources.Scripts.Utils;
using UnityEngine;

namespace Resources.Scripts.InGame
{
    public class Tiles : MonoBehaviour
    {
        private Dictionary<HexPoint, HexTile> _tilesDict;
        private List<List<HexTile>> _tilesList;

        private TilesMesh _tilesMesh;

        private void Awake()
        {
            Initialize();
            _tilesMesh = GetComponentInChildren<TilesMesh>();
        }

        public List<HexPoint> Keys()
        {
            return _tilesDict.Keys.ToList();
        }

        public void Add(int y, HexPoint point, HexTile tile)
        {
            _tilesDict.Add(point, tile);
            
            if (_tilesList.Count <= y)
            {
                for (int i = 0; i <= y - _tilesList.Count; i++)
                {
                    _tilesList.Add(new List<HexTile>());
                }
            }
            
            _tilesList[y].Add(tile);
        }

        public List<HexTile> ToList()
        {
            List<HexTile> result = new List<HexTile>();

            for (int i = _tilesList.Count - 1; i >= 0; i--)
            {
                result.AddRange(_tilesList[i]);
            }

            return result;
        }

        public bool ContainsKey(HexPoint point)
        {
            return _tilesDict.ContainsKey(point);
        }

        public HexTile Get(HexPoint point)
        {
            if (!_tilesDict.ContainsKey(point))
                return null;

            return _tilesDict[point];
        }

        public void Remove(HexPoint point)
        {
            HexTile tile = Get(point);

            for (int i = 0; i < _tilesList.Count; i++)
            {
                if (_tilesList[i].Exists(item => item == tile))
                {
                    _tilesList[i].Remove(tile);
                    break;
                }
            }

            for (int i = _tilesList.Count - 1; i >= 0; i--)
            {
                if (_tilesList[i].Count == 0)
                {
                    _tilesList.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
            
            _tilesDict.Remove(point);
        }

        public void CreateTiles(int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
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

            HexPoint point = HexPoint.FromOffsetPoint(x, y);
            HexTile.TileHeight height = (Random.Range(0, 2) & 1) == 0 ? HexTile.TileHeight.DEFAULT : HexTile.TileHeight.LOW;
            Color color = BoardUtils.colors[Random.Range(0, BoardUtils.colors.Length)];
            
            HexTile tile = new HexTile(point, position, color, height);
            Add(y, point, tile);
        }

        public void Triangulate()
        {
            _tilesMesh.Triangulate(ToList());
        }

        public void Initialize()
        {
            _tilesDict = new Dictionary<HexPoint, HexTile>();
            _tilesList = new List<List<HexTile>>();
        }
    }
}