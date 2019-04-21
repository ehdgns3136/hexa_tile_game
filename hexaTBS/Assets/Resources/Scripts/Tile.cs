using System.Collections.Generic;
using UnityEngine;
using Resources.Scripts;

namespace Resources.Scripts
{
    
    public class Tile : MonoBehaviour
    {
        private float _length;
        private HexaPoint _hexaPoint;
        private CubePoint _cubePoint;
    
        private GameObject _highlightTile;
    
        private List<CubePoint> _reachablePoints;

        private Unit _unit;
        private Resource _resource;

        private Color _color;

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


        public void Initialize(HexaPoint hexaPoint, CubePoint cubePoint, float length)
        {
            _hexaPoint = hexaPoint;
            _cubePoint = cubePoint;
            _length = length;
            _highlightTile = transform.Find("Select").gameObject;
            
            _unit = null;
            _resource = null;
            
            BoardManager.Instance.UpdateReachablePoints(this);
        }
        
    
        public void OnClick()
        {
            Debug.Log(_unit);

//            Debug.Log("Row, Col : " + hexaPoint.GetRow() + ", " + hexaPoint.GetColumn());
//            Debug.Log("X, Y, Z : " + cubePoint.GetX() + ", " + cubePoint.GetY() + ", " + cubePoint.GetZ());

//            BoardManager.Instance.SelectNewPoint(cubePoint);

        }

        public void SetReachablePoints(List<CubePoint> points)
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

        public CubePoint GetCubePoint()
        {
            return _cubePoint;
        }
        
        public HexaPoint GetHexaPoint()
        {
            return _hexaPoint;
        }

        public TileType GetTileType()
        {
            return _type;
        }
    }
}
