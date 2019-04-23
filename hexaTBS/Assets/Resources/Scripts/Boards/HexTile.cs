using System.Collections.Generic;
using UnityEngine;
using Resources.Scripts;

namespace Resources.Scripts
{
    
    public class HexTile
    {
        private HexPoint _hexPoint;
        private Vector3 _position;
        private Color _color;
    
        private GameObject _highlightTile;
    
        private List<HexPoint> _reachablePoints;

        private Unit _unit;
        private Resource _resource;


        public enum TileType
        {
            DEFAULT = 0x0001,
            LOW = 0x0010,
            LOWABLE = 0x0011,
            HIGH = 0x0100,
            HIGHABLE = 0x0101,
            EVERYWHERE = 0x0111
        }

        private TileType _type;
//        private Sprite _sprite;


        public HexTile(HexPoint hexPoint, Vector3 position, Color color)
        {
            _hexPoint = hexPoint;
            _position = position;
            _color = color;
            
            _unit = null;
            _resource = null;
            
//            BoardManager.Instance.UpdateReachablePoints(this);
        }

        public void SetReachablePoints(List<HexPoint> points)
        {
            _reachablePoints = points;
        }

        public void SetUnit(Unit unit)
        {
            _unit = unit;
        }

        public Unit GetUnit()
        {
            return _unit;
        }
        
        public HexPoint GetHexPoint()
        {
            return _hexPoint;
        }

        public TileType GetTileType()
        {
            return _type;
        }

        public Vector3 GetPosition()
        {
            return _position;
        }

        public Color GetColor()
        {
            return _color;
        }

        public void SetColor(Color color)
        {
            _color = color;
        }

    }
}
