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

        public enum TileHeight
        {
            LOW = 0x0001,
            DEFAULT = 0x0010,
            HIGH = 0x0100
        }

        private TileHeight _tileHeight;
        private int _height;

        public TileHeight Height
        {
            get { return _tileHeight; }
            set
            {
                _tileHeight = value;

                int heightAsDecimal = (int) _tileHeight;
                _height = 0;
                
                while (heightAsDecimal >= 16)
                {
                    heightAsDecimal = heightAsDecimal / 16;
                    _height += 1;
                }
            }
        }

//        private Sprite _sprite;


        public HexTile(HexPoint hexPoint, Vector3 position, Color color, TileHeight height)
        {
            _hexPoint = hexPoint;
            _position = position;
            _color = color;
            Height = height;
            
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

        public int GetHeight()
        {
            return _height;
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

        public void SetHeight(TileHeight height)
        {
            Height = height;
        }

    }
}
