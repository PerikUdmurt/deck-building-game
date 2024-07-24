namespace CardBuildingGame.Infrastructure
{
    public interface IPooledObject
    {
        public void OnCreated();
        public void OnReceipt();
        public void OnReleased();
    }
}