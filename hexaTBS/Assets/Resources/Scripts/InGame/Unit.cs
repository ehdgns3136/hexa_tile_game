using UnityEngine;

namespace Resources.Scripts.InGame
{
    public class Unit
    {
        private int _hp;
        private int _offense;
        private int _defense;
        private int _movement;
        private int _sight;
        private UnitType _type;
        
        private HexTile.TileHeight[] _movableTileType;
        private int _leftMovement;

        public Sprite sprite;
        
        public enum UnitType
        {
            NONE, // for default
            HUMAN,
            BUILDING
        }

        public void Initialize(int hp, int offense, int defense, int movement, int sight, UnitType type)
        {
            _hp = hp;
            _offense = offense;
            _defense = defense;
            _movement = movement;
            _sight = sight;
            _type = type;

            _movableTileType = new[] {HexTile.TileHeight.DEFAULT};
            _leftMovement = _movement;
        }

        public int GetLeftMovement()
        {
            return _leftMovement;
        }

        public int GetMovement()
        {
            return _movement;
        }

//        public HexTile.TileType GetMovableTileType()
//        {
//            return _movableTileType;
//        }
    }
}