namespace Resources.Scripts
{
    public class Building
    {
        private int hp;
        private int offense;
        private int defense;
        private int movement;
        private int sight;

        public Building(int hp, int offense, int defense, int movement, int sight)
        {
            this.hp = hp;
            this.offense = offense;
            this.defense = defense;
            this.movement = movement;
            this.sight = sight;
        }
    }
}