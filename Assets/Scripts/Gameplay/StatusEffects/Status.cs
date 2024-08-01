namespace CardBuildingGame.Gameplay.Statuses
{
    public class Status
    {
        public StatusDirection Direction;
        public StatusType statusType;
        public bool isStackable;
        public bool isPerpetual;
        public bool isFullDecimate;

        public enum StatusDirection
        { 
            Positive = 0,
            Negative = 1,
            Neitral = 2,
        }
    }
}