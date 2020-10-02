namespace TjuvOchPolisMattias
{
    abstract class Person : Inventory
    {
        public int MovementYAxis { get; set; }
        public int MoveMentXAxis { get; set; }
        public int Direction { get; set; }
        public char PlayerIcon { get; set; }
        public Person(int movementYAxis, int movementXAxis, int direction, char playerIcon)
        {
            MovementYAxis = movementYAxis;
            MoveMentXAxis = movementXAxis;
            Direction = direction;
            PlayerIcon = playerIcon;
        }
        public virtual void PrisonCheck()
        {
        }
        public virtual void PrisonIdUpdate(int input)
        {
        }
    }
}
