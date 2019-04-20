namespace Resources.Scripts
{
    public class Unit
    {
        private int hp;
        private int offense;
        private int defense;
        private int movement;
        private int sight;
        private UnitType type;
        
        public enum UnitType
        {
            NONE,
            HUMAN,
            BUILDING
        }

        public Unit(int hp, int offense, int defense, int movement, int sight, UnitType type)
        {
            this.hp = hp;
            this.offense = offense;
            this.defense = defense;
            this.movement = movement;
            this.sight = sight;
            this.type = type;
        }
    }
}